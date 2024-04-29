using Newtonsoft.Json;

namespace EnergySupplier.Model
{
    public class SupplierPlan
    {
        [JsonProperty("supplier_name")]
        public string? SupplierName { get; set; }

        [JsonProperty("plan_name")]
        public string? PlanName { get; set; }

        [JsonProperty("prices")]
        public List<Price> Prices { get; set; }

        [JsonProperty("standing_charge")]
        public decimal? StandingCharge { get; set; }
    }
}
