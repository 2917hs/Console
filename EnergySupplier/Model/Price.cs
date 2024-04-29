using Newtonsoft.Json;

namespace EnergySupplier.Model
{
    public class Price
    {
        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("threshold")]
        public int? Threshold { get; set; }
    }
}
