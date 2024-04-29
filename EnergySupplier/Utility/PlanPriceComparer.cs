using EnergySupplier.Model;

namespace EnergySupplier.Utility
{
    public class PlanPriceComparer : IComparer<SupplierPlan>
    {
        public int Compare(SupplierPlan? x, SupplierPlan? y)
        {
            if (x == null && y == null) return 0;

            decimal xPrice = x?.Prices?.FirstOrDefault()?.Rate ?? 0;
            decimal yPrice = y?.Prices?.FirstOrDefault()?.Rate ?? 0;

            return xPrice.CompareTo(yPrice);
        }
    }
}
