using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities
{
    public class Response<T>
    {
        [JsonProperty(PropertyName = "errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty(PropertyName = "data")]
        public T Data { get; set; }
    }
}
