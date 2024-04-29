namespace EnergySupplier.Tests.Service
{
    public class FileMock : IFileMock
    {
        public string TryGetSolutionDirectoryInfo(string currentDirectory = null)
        {
            var directory = new DirectoryInfo(currentDirectory ?? Directory.GetCurrentDirectory());

            while (directory != null && !directory.EnumerateFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }

            return directory.FullName;
        }
    }
}
