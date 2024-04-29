using Newtonsoft.Json;

namespace EnergySupplier.Utility
{
    public class Generic
    {
        public List<T> ConvertJsonToCollection<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath) && !Validation.IsValidJsonFilepath(filePath))
                {
                    throw new FileNotFoundException("File not found.", filePath);
                }

                var json = File.ReadAllText(filePath);

                if (!Validation.IsValidJsonContent(json))
                {
                    throw new InvalidDataException("Invalid json format.");
                }
                else
                {
                    return JsonConvert.DeserializeObject<List<T>>(json);
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
