using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities.Templates
{
    public class TemplateUpdate
    {

        public int Id { get; set; }
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; }
    }
}
