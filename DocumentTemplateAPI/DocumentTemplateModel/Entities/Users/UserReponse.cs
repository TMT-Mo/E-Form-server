using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities.Users
{
    public class UserReponse
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }
    }
}
