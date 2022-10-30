using DocumentTemplateModel.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateRepository.Interfaces
{
    public interface IUserRepository : ICrud<UserCreationRequest, UserUpdateRequest, DocumentTemplateModel.Models.User, int>
    {
        string Authenticate(string userName, string password);

    }
}
