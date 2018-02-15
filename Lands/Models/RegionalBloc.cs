namespace Lands.Models
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class RegionalBloc
    {
        [JsonProperty(PropertyName = "acronym")]
        public string acronym { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

    }
}
