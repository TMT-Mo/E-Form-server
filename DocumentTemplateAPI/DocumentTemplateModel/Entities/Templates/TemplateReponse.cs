using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities.Templates
{
    public class TemplateReponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updatedAt")]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "templateName")]
        public string TemplateName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "size")]
        public string Size { get; set; }

        [JsonProperty(PropertyName = "statusTemplate")]
        public string StatusTemplate { get; set; }

        [JsonProperty(PropertyName = "departmentName")]
        public string DepartmentName { get; set; }

    }
}
