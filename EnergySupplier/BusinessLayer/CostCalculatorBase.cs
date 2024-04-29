using EnergySupplier.Model;

namespace EnergySupplier.BusinessLayer
{
    public abstract class CostCalculatorBase
    {
        public abstract decimal CalculateTotalCost(
                                SupplierPlan supplierPlan,
                                int annualConsumption,
                                decimal vat);

        protected decimal AddVAT(decimal cost, decimal vat) => cost * (1 + vat);
    }
}
