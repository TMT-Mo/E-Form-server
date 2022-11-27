using DocumentTemplateModel.Entities.Templates;
using DocumentTemplateModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentTemplateUtilities
{
    public class ConvertEDMXToDetailShared
    {
        public static TemplateReponse TierEdmxToDetail(Template inp)
        {
            return new TemplateReponse
            {
                Id = inp.Id,
                CreatedAt = inp.CreatedAt,
                UpdatedAt = inp.UpdatedAt,
                StatusTemplate = inp.StatusTemplate == 1 ? "Active" : "Deactive",
                DepartmentName = inp.IdDepartmentNavigation?.DepartmentName,
                Size = inp.Size,
                Type = inp.Type,
                TemplateName = inp.TemplateName
            };
        }

        public static List<TemplateReponse> TierEdmxToListDetails(List<Template> inp)
        {
            return inp.Select(e => TierEdmxToDetail(e)).ToList();
        }
    }
}
