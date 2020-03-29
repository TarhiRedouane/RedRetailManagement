using System.Configuration;

namespace RRMDataManager.Library.Helpers
{
    public class ConfigHelper
    {
        public static decimal GetTaxRate()
        {
            var isParsable = decimal.TryParse(ConfigurationManager.AppSettings["taxRate"], out var result);
            if (isParsable) return result;
            throw new ConfigurationErrorsException("Invalid Tax Rate");
        }
    }
}