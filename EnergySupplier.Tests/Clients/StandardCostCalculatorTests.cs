using EnergySupplier.BusinessLayer;
using EnergySupplier.Model;

namespace EnergySupplier.Tests.Clients
{
    public class StandardCostCalculatorTests
    {
        private readonly CostCalculatorBase costCalculatorBase;

        public StandardCostCalculatorTests()
        {
            costCalculatorBase = new StandardCostCalculator();
        }

        [Theory]
        [InlineData(1000, 0.05)]
        public void CalculateTotalCost_ReturnsTotalCost(int annualConsumption, decimal vat)
        {
            // Arrange
            var supplierPlan = new SupplierPlan
            {
                StandingCharge = null,
                SupplierName = "Test",
                PlanName = "Test",
                Prices = new List<Price>
                {
                    new Price { Threshold = 100, Rate = 13.5m },
                    new Price { Rate = 10m }
                }
            };

            // Act
            decimal totalCost = costCalculatorBase.CalculateTotalCost(supplierPlan, annualConsumption, vat);

            // Assert
            Assert.Equal(108.68m, decimal.Round(totalCost, 2, MidpointRounding.AwayFromZero));
        }

        [Theory]
        [InlineData(1000, 0.05)]
        public void CalculateTotalCost_ReturnsTotalCostWithStandingCharges(int annualConsumption, decimal vat)
        {
            // Arrange
            var supplierPlan = new SupplierPlan
            {
                StandingCharge = 7,
                SupplierName = "Test",
                PlanName = "Test",
                Prices = new List<Price>
                {
                    new Price { Rate = 9 }
                }
            };

            // Act
            decimal totalCost = costCalculatorBase.CalculateTotalCost(supplierPlan, annualConsumption, vat);

            // Assert
            Assert.Equal(121.33m, decimal.Round(totalCost, 2, MidpointRounding.AwayFromZero));
        }
    }
}
