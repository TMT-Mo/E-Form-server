using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DocumentTemplateModel.Entities.Departments;
using DocumentTemplateModel.Entities.Users;

namespace DocumentTemplateUtilities
{
    public class ConvertEDMXToDetailShared
    {
        public static TemplateReponse TemplateEdmxToDetail(Template inp, IEnumerable<Category> categories = null, IEnumerable<UserTemplate> userTemplates = null, IEnumerable<User> users = null, IEnumerable<Department> departments = null, IEnumerable<UserRole> userRoles = null)
        {
            var typeName = categories?.SingleOrDefault(x => x.Id == inp.IdType)?.TypesName;
            var userTemplateIds = userTemplates?.Where(x => x.IdTemplate == inp.Id).Select( y => y.IdUser).ToList();
            var usersbyTemplateIds = users?.Where(x => userTemplateIds.Contains(x.Id)).ToList();
            var departmentByUserTemplateIds = userRoles?.Where(x => userTemplateIds.Contains(x.IdUser)).Select(y => y.IdDepartment).Distinct().ToList();
            var departmentName = departments?.Where(x => departmentByUserTemplateIds.Any(y => y == x.Id)).FirstOrDefault()?.DepartmentName;
            return new TemplateReponse
            {
                Id = inp.Id,
                CreatedAt = inp.CreatedAt,
                UpdateAt = inp.UpdateAt,
                Status = Helper.ConvertStatusTemplate((int)inp.Status),
                Size = (int)inp.Size,
                Type = inp.Type,
                Link = inp.Link,
                Description = inp.Description,
                IsEnable = Helper.ConvertIsEnableTemplate((bool)inp.IsEnable),
                TemplateName = inp.TemplateName,
                TypeName = typeName,
                SignatoryList = usersbyTemplateIds != null ? UserEdmxToListDetails(usersbyTemplateIds) : null,
                DepartmentName = departmentName
            };
        }
        public static List<TemplateReponse> TemplateEdmxToListDetails(List<Template> inp, IEnumerable<Category> categories = null, IEnumerable<UserTemplate> userTemplates = null, IEnumerable<User> users = null, IEnumerable<Department> departments = null, IEnumerable<UserRole> userRoles = null)
        {
            return inp.Select(e => TemplateEdmxToDetail(e, categories, userTemplates, users)).ToList();
        }

        public static List<DepartmentReponse> DepartmentEdmxToListDetails(List<Department> inp)
        {
            return inp.Select(e => DepartmentEdmxToListDetails(e)).ToList();
        }

        public static DepartmentReponse DepartmentEdmxToListDetails(Department inp)
        {
            return new DepartmentReponse
            {
                Id = inp.Id,
                CreatedAt = inp.CreatedAt,
                UpdateAt = inp.UpdateAt,
                Status = (int)inp.Status,
                DepartmentName = inp.DepartmentName
            };
        }

        public static List<CategoryReponse> CategoryEdmxToListDetails(List<Category> inp)
        {
            return inp.Select(e => CategoryEdmxToListDetails(e)).ToList();
        }

        public static CategoryReponse CategoryEdmxToListDetails(Category inp)
        {
            return new CategoryReponse
            {
                Id = inp.Id,
                CreatedAt = inp.CreatedAt,
                UpdateAt = inp.UpdateAt,
                Status = (int)inp.Status,
                typeName = inp.TypesName
            };
        }

        public static List<UserProfileReponse> UserEdmxToListDetails(List<User> inp)
        {
            return inp.Select(e => UserEdmxToListDetails(e)).ToList();
        }

        public static UserProfileReponse UserEdmxToListDetails(User inp)
        {
            return new UserProfileReponse
            {
                Id = inp.Id,
                CreatedAt = inp.CreatedAt,
                UpdateAt = inp.UpdateAt,
                Status = (int)inp.Status,
                Email = inp.Email,
                Firstname = inp.Firstname,
                Lastname = inp.Lastname,
                Signature = inp.Signature
            };
        }
        public static string GetErrorMessagesFromModalState(ModelStateDictionary modelState)
        {
            var errorMessages = "";

            foreach (var state in modelState.Values)
            {
                foreach (var error in state.Errors)
                {
                    if (!string.IsNullOrEmpty(error.ErrorMessage))
                    {
                        errorMessages += error.ErrorMessage + " ";
                    }
                    else if (!string.IsNullOrEmpty(error.Exception.Message))
                    {
                        errorMessages += error.Exception.Message + " ";
                    }

                }
            }

            return errorMessages;
        }
    }

}
