using System.Configuration;

namespace RRMDesktopShell.Helpers
{
    public interface IConfigHelper
    {
        decimal GetTaxRate();
    }

    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {
            var isParsable =decimal.TryParse(ConfigurationManager.AppSettings["taxRate"],out var result);
            if (isParsable) return result;
            throw new ConfigurationErrorsException("Invalid Tax Rate");
        }
    }
}