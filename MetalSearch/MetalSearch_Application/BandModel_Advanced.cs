using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MetalSearch_Application
{
    public class BandModel_Advanced
    {
        [JsonPropertyName("id")]
        public string? ID { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("location")]
        public string? Location { get; set; }

        [JsonPropertyName("formedIn")]
        public string? FormedIn { get; set; }

        [JsonPropertyName("yearsActive")]
        public string? YearsActive { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("themes")]
        public string? Themes { get; set; }

        [JsonPropertyName("label")]
        public string? Label {  get; set; }

        [JsonPropertyName("bandCover")]
        public string? BandCover { get; set; }

        [JsonPropertyName("albums")]
        public List<Album_Preview> Albums { get; set; }
        

        public BandModel_Advanced() { }


    }

    
}
