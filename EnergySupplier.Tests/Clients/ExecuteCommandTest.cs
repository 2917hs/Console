using EnergySupplier.Process;
using EnergySupplier.Tests.Service;
using EnergySupplier.Utility;
using Moq;
using Constant = EnergySupplier.Utility.Constant;

namespace EnergySupplier.Tests.Clients
{
    public class ExecuteCommandTest
    {
        private readonly string dataSetPath;
        private readonly Mock<IFileMock> _mockFileMock;

        public ExecuteCommandTest()
        {
            var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            _mockFileMock = new Mock<IFileMock>();
            _mockFileMock.Setup(x => x.TryGetSolutionDirectoryInfo(null)).Returns(solutionDirectory.FullName);
            dataSetPath = _mockFileMock.Object.TryGetSolutionDirectoryInfo(null);
        }

        [Theory]
        [InlineData(Constant.InputCommand, @"Dataset/EnergySupplierData.json", Constant.AnnualCostCommand, "1000", Constant.ExitCommand)]
        public void Run_WithValidInput_ShouldHandleCommands(string input, string fileName, string annual_cost, string cost, string exit)
        {
            string filePath = Path.Combine(dataSetPath, fileName);
            string consoleReader = $"{input} {filePath}{Environment.NewLine}{annual_cost} {cost}{Environment.NewLine}{exit}";
            var command = new StringReader(consoleReader);
            Console.SetIn(command);
            var output = new StringWriter();
            Console.SetOut(output);

            new ExecuteCommand().Run();

            var outputLines = output.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            //1st line => input
            //2nd to 5th lines => each row for 4 suppliers
            //6th line => exit
            Assert.Equal(5, outputLines.Length);
        }

    }
}
