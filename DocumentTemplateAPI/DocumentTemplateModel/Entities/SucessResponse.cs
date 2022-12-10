using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities
{
    public class SucessResponse
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }
    }
}
