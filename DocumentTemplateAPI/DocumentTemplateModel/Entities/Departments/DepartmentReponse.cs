using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities.Departments
{
    public class DepartmentReponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updateAt")]
        public DateTime? UpdateAt { get; set; }

        [JsonProperty(PropertyName = "departmentName")]
        public string DepartmentName { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }
}
