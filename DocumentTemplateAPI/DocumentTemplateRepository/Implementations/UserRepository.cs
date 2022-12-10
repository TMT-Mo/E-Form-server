using DocumentTemplateModel.Entities.Users;
using DocumentTemplateModel.Models;
using DocumentTemplateRepository.Interfaces;
using DocumentTemplateUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentTemplateRepository.Implementations
{
    public class UserRepository : BaseCRUDRepository<UserCreationRequest, UserUpdateRequest, User, int>, IUserRepository
    {
        public UserRepository(IUnitOfWork _UnitOfWork, IDBRepositoryBase<User> _repo)
             : base(_UnitOfWork, _repo)
        {
        }

        protected override User CreationTypeToEdmx(UserCreationRequest inp)
        {
            throw new NotImplementedException();
        }
        public string Authenticate(string userName, string password)
        {
            var user = GetUserByUserName(userName, password);
            if (user == null)
            {
                return Enums.ReponseUser.NOTFOUND.ToString();
            }
            if (user.Password != password)
            {
                return Enums.ReponseUser.INVALIDPASSWORD.ToString();
            }
            return user.Id.ToString();
            //if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            //{
            //    return $"{ user.Id }";
            //}
        }

        public User GetUserByUserName(string userName, string password)
        {
            try
            {
                return _repo.GetAll().FirstOrDefault(u => u.Username == userName && u.Status == (int)Enums.Status.ACTIVE);
            }
            catch (Exception ex)
            {
                //_logger.WriteError($"{ this.GetType().Name } - { System.Reflection.MethodBase.GetCurrentMethod().Name }: Method Failed. Error Details: { ex.Message }", ex);
                throw;
            }
        }
        protected override void UpdateEDMXFromUpdateReq(User edmx, UserUpdateRequest inp)
        {
            throw new NotImplementedException();
        }
    }
}
