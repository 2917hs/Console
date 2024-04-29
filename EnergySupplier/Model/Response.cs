namespace EnergySupplier.Model
{
    public class Response
    {
        public string SupplierName { get; set; }

        public string PlanName { get; set; }

        public decimal TotalCost { get; set; }

        public Response(string supplierName, string planName, decimal totalCost)
        {
            SupplierName = supplierName;
            PlanName = planName;
            TotalCost = totalCost;
        }

        public override string ToString()
        {
            return $"{SupplierName}, {PlanName}, {TotalCost:F2}";
        }
    }
}
