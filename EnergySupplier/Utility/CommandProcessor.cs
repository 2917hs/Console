namespace EnergySupplier.Utility
{
    public sealed class CommandProcessor
    {
        private static CommandProcessor? _instance;

        private readonly HashSet<string> commands = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            Constant.InputCommand,
            Constant.AnnualCostCommand,
            Constant.ExitCommand
        };

        private CommandProcessor() { }

        public static CommandProcessor Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new CommandProcessor();
                }

                return _instance;
            }
        }

        public HashSet<string> Commands => commands;
    }
}
