using EnergySupplier.Model;
using EnergySupplier.Tests.Service;
using EnergySupplier.Utility;
using Moq;

namespace EnergySupplier.Tests.Clients
{
    public class GenericTest
    {
        private readonly Generic generic;
        private readonly string  dataSetPath;
        private readonly Mock<IFileMock> _mockFileMock;

        public GenericTest()
        {
            generic = new Generic();

            var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent;
            _mockFileMock = new Mock<IFileMock>();
            _mockFileMock.Setup(x => x.TryGetSolutionDirectoryInfo(null)).Returns(solutionDirectory.FullName);
            dataSetPath = _mockFileMock.Object.TryGetSolutionDirectoryInfo(null);
        }

        [Theory]
        [InlineData(@"Dataset/FileNotFound.json")]
        public void ConvertJsonToCollection_FileNotFound_ThrowsFileNotFoundException(string fileName)
        {
            string filePath = Path.Combine(dataSetPath, fileName);
            
            Assert.Throws<FileNotFoundException>(() => generic.ConvertJsonToCollection<object>(filePath));
        }

        [Theory]
        [InlineData(@"Dataset/InvalidContent.json")]
        public void ConvertJsonToCollection_InvalidJsonFormat_ThrowsInvalidDataException(string fileName)
        {
            string filePath = Path.Combine(dataSetPath, fileName);
            
            Assert.Throws<InvalidDataException>(() => generic.ConvertJsonToCollection<object>(filePath));
        }

        [Theory]
        [InlineData(@"Dataset/EnergySupplierData.json")]
        public void ConvertJsonToCollection_ValidFileAndJson_ReturnsDeserializedObject(string fileName)
        {
            string filePath = Path.Combine(dataSetPath, fileName);

            var result = generic.ConvertJsonToCollection<SupplierPlan>(filePath);

            Assert.NotNull(result);
            Assert.IsType<List<SupplierPlan>>(result);
        }
    }
}
