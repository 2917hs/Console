using EnergySupplier.BusinessLayer;
using EnergySupplier.Model;
using EnergySupplier.Utility;

namespace EnergySupplier.Process
{
    public class ExecuteCommand
    {
        public ExecuteCommand() { }

        private List<SupplierPlan> _supplierPlan;

        const decimal VAT = 0.05m;

        public void Run()
        {
            bool executeNextCommand = true;

            while (executeNextCommand)
            {
                Console.Write("> ");
                string? command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command) && !Validation.IsValidCommand(command))
                {
                    Console.WriteLine("Invalid arguments.");
                    continue;
                }

                string[] args = command.Split(' ');

                switch (args[0])
                {
                    case Constant.InputCommand:
                        HandleInputCommand(args[1]);
                        break;
                    case Constant.AnnualCostCommand:
                        HandleAnnualCostCommand(args[1]);
                        break;
                    case Constant.ExitCommand:
                        executeNextCommand = false;
                        break;
                    default:
                        Console.WriteLine("Invalid command.");
                        executeNextCommand = false;
                        break;
                }
            }
        }

        void HandleInputCommand(string filePath)
        {
            _supplierPlan = new Generic().ConvertJsonToCollection<SupplierPlan>(filePath);
            if (_supplierPlan is null || !_supplierPlan.Any())
            {
                Console.WriteLine("Please load a file with valid content using 'input <filename>' command.");
            }
        }

        void HandleAnnualCostCommand(string inputCost)
        {
            if (int.TryParse(inputCost, out int cost))
            {
                AnnualCostCalculation(_supplierPlan, cost);
            }
        }

        void AnnualCostCalculation(List<SupplierPlan> supplierPlans, int annualConsumption)
        {
            //_supplierPlan.Sort(new PlanPriceComparer());
            List<Response> response = new List<Response>();
            StandardCostCalculator standardCostCalculator = new StandardCostCalculator();

            foreach (SupplierPlan plan in supplierPlans)
            {
                decimal totalCost = standardCostCalculator.CalculateTotalCost(
                                                        plan,
                                                        annualConsumption,
                                                        VAT);
                response.Add(new Response(plan.SupplierName, plan.PlanName, totalCost));
            }

            response.Sort((x, y) => x.TotalCost.CompareTo(y.TotalCost));

            foreach (var result in response)
            {
                Console.WriteLine(result);
            }
        }
    }
}
