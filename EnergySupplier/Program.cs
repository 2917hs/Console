using EnergySupplier.Process;

try
{
    new ExecuteCommand().Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}