using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DocumentTemplateUtilities
{
    public class JwtHelpers
    {
        private static string SECRET_KEY = "this_is_secret_key_will_be_replaced_by_value_in_config_file";

        public static string GenerateJwtToken(string userId, string sessionId = null, bool enabled2FA = false, bool isChangePassword = false, string refreshToken = "", int type = 0, int expiryMinutes = 30)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET_KEY);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim("userId", userId.ToString()),
                }),

                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes), // .AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static JwtSecurityToken GetJwtToken(string token, int clockSkewMinutes = -1)
        {
            TimeSpan clockSkew;
            if (clockSkewMinutes == -1)
            {
                clockSkew = TimeSpan.Zero;
            }
            else
            {
                clockSkew = TimeSpan.FromMinutes(clockSkewMinutes);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = clockSkew
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
            }

            return null;
        }

        //public static ValidateUser ValidateJwtToken(string token)
        //{
        //    try
        //    {
        //        var container = new Container();

        //        container.Register<ILogger, InsightsLogger>();
        //        container.Register<IServiceAccess, ServiceBase>();
        //        container.Register<IConfigReader, ConfigReader>();
        //        container.Register<ICacheRepository, CacheRepository>();
        //        var _cache = container.GetInstance<ICacheRepository>();

        //        var jwtToken = GetJwtToken(token);
        //        if (jwtToken == null)
        //        {
        //            return null;
        //        }
        //        var userId = jwtToken.Claims.First(x => x.Type == "userId").Value;
        //        var sessionId = jwtToken.Claims.First(x => x.Type == "sessionId").Value;

        //        var isEnableSingleSession = ConfigurationManager.AppSettings["EnableSingleSession"];
        //        if (isEnableSingleSession != null && isEnableSingleSession == "true")
        //        {
        //            var data = _cache.GetItemFromCache($"Admin_{ userId }");
        //            if (data == null)
        //            {
        //                return null;
        //            }

        //            var uuid = data.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //            if (uuid == null)
        //            {
        //                return null;
        //            }

        //            uuid = uuid.Replace("\"", "");
        //            if (sessionId != uuid)
        //            {
        //                return null;
        //            }
        //        }

        //        string stepValue = jwtToken.Claims.First(x => x.Type == "stepValue").Value;
        //        bool is2FAStep = stepValue == Constants.ValidateJwtTokenStep2FA;
        //        bool isChangePasswordStep = stepValue == Constants.ValidateJwtTokenStepChangePassword;

        //        string enabled2FAValue = jwtToken.Claims.First(x => x.Type == "enabled2FA").Value;
        //        bool enabled2FA = false;
        //        if (enabled2FAValue != null)
        //        {
        //            bool.TryParse(enabled2FAValue, out enabled2FA);
        //        }

        //        Role role = (Role)Enum.Parse(typeof(Role), jwtToken.Claims.First(x => x.Type == "type").Value); // type = 1 => role = admin, type = 2 => role = merchant

        //        // return account id from JWT token if validation successful
        //        if ((enabled2FA && is2FAStep || !enabled2FA) && !isChangePasswordStep)
        //        {
        //            return new ValidateUser { UserId = userId, Role = role };
        //        }

        //        return null;
        //    }
        //    catch (Exception e)
        //    {
        //        // return null if validation fails
        //        return null;
        //    }
        //}

        //public static string ValidateJwtTokenByStep(string token, List<string> stepValues = null)
        //{
        //    try
        //    {
        //        var jwtToken = GetJwtToken(token);

        //        if (jwtToken == null)
        //        {
        //            return null;
        //        }

        //        var userId = jwtToken.Claims.First(x => x.Type == "userId").Value;

        //        string stepValue = jwtToken.Claims.First(x => x.Type == "stepValue").Value;

        //        // return account id from JWT token if validation successful
        //        if (stepValues != null && stepValues.Contains(stepValue))
        //        {
        //            return userId;
        //        }

        //        return null;
        //    }
        //    catch
        //    {
        //        // return null if validation fails
        //        return null;
        //    }
        //}

        //public static string GenerateRefreshToken(int size = 32)
        //{
        //    byte[] randomNumber = new byte[size];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        return Convert.ToBase64String(randomNumber);
        //    }
        //}

        //public static ClaimsPrincipal GetPrincipalFromToken(string token)
        //{
        //    var key = Encoding.ASCII.GetBytes(SECRET_KEY);
        //    var tokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
        //        ValidateIssuer = false,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = new SymmetricSecurityKey(key),
        //        ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    SecurityToken securityToken;
        //    var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        //    var jwtSecurityToken = securityToken as JwtSecurityToken;
        //    if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        //        throw new SecurityTokenException("Invalid token");
        //    return principal;
        //}
    }
}
