namespace Services.Localization
{
    public interface ILocalizationService
    {
        public void SetLocale(int localeID);
        public void ChangeLocale();
    }
}