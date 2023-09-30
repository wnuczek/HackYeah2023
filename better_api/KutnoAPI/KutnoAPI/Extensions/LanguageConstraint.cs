namespace MesInternalApi.Extensions
{
    public class LanguageContraint
    {
        public static bool ValidCulture(string culture)
        {
            return culture == "en-US" || culture == "pl-PL" || culture == "en-EN";
        }
    }
}
