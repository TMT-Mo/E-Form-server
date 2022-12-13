using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace DocumentTemplateModel.Entities.Templates
{
    public class TemplateCreationRequest
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int IdTemplate { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public int IdUser { get; set; }

        [Required]
        [JsonProperty("name")]
        public string TemplateName { get; set; }

        [Required]
        [JsonProperty("signatoryList")]
        public List<int> SignatoryList { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        [JsonProperty("type")]
        public string Type { get; set; }

        [Required]
        [JsonProperty("Size")]
        public int Size { get; set; }

        [Required]
        [JsonProperty("createdBy")]
        public int CreatedBy { get; set; }

        [Required]
        [JsonProperty("description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty("link")]
        public string Link { get; set; }

        [Required]
        [JsonProperty("idTemplateType")]
        public int IdTemplateType { get; set; }
    }
}
