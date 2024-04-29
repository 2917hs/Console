using EnergySupplier.Model;

namespace EnergySupplier.BusinessLayer
{
    public class StandardCostCalculator : CostCalculatorBase
    {
        public override decimal CalculateTotalCost(SupplierPlan supplierPlan, int annualConsumption, decimal vat)
        {
            decimal totalCost = (supplierPlan.StandingCharge * 365 / 100) ?? 0;

            foreach (var price in supplierPlan.Prices)
            {
                if (price.Threshold.HasValue) 
                {
                    totalCost += Math.Min(price.Threshold.Value, annualConsumption) * price.Rate / 100;
                    annualConsumption -= price.Threshold.Value;
                }
                else
                {
                    totalCost += annualConsumption * price.Rate / 100;
                }
            }

            return AddVAT(totalCost, vat);
        }
    }
}
