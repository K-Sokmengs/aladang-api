using System.Globalization;

namespace aladang_server_api.Utils;

public class AmountFormatCurrencyUtil
{
    public static string GetAmountFormatCurrency(decimal balance, string currencyCode)
    {
        string toBeFormat;
        var nfi = new CultureInfo("en-US", false).NumberFormat;

        // Keep two decimal places for other currencies
        toBeFormat = balance.ToString(currencyCode == "KHR" ?
            // Remove decimal places for KHR
            "#,##0" : "#,##0.00", nfi);

        return toBeFormat;
    }
}