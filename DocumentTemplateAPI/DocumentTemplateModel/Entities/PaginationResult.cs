using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentTemplateModel.Entities
{
    public class PaginationResult<T>
    {
        [JsonProperty(PropertyName = "items")]
        public List<T> Items { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
    }
}
