using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities
{
    public class Response
    {
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }
    }
}
