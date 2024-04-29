using System.Text.Json;
using System.Text.RegularExpressions;


namespace EnergySupplier.Utility
{
    public static class Validation
    {
        public static bool IsValidJsonFilepath(string filePath) =>
            Regex.IsMatch(filePath, @"^.*\.json$");

        public static bool IsValidJsonContent(string content)
        {
            try
            {
               JsonDocument.Parse(content);
            }
            catch (System.Text.Json.JsonException)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidCommand(string command)
        {
            CommandProcessor commandProcessor = CommandProcessor.Instance;

            if (!IsValidCommand(command, commandProcessor.Commands))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidCommand(string input, HashSet<string> commands)
        {
            if (input.Equals(Constant.ExitCommand))
            {
                return true;
            }

            string[] splitCommand = input.Split(' ');

            return splitCommand is not null &&
                    splitCommand.Length == 2 &&
                    commands.Contains(splitCommand[0]);
        }

        public static DirectoryInfo TryGetSolutionDirectoryInfo(string currentPath = null)
        {
            var directory = new DirectoryInfo(
                currentPath ?? Directory.GetCurrentDirectory());
            while (directory != null && !directory.GetFiles("*.sln").Any())
            {
                directory = directory.Parent;
            }
            return directory;
        }

    }
}
