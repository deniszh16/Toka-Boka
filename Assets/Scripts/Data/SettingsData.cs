using System;
using System.Collections.Generic;
using YG;

namespace Data
{
    [Serializable]
    public class SettingsData
    {
        public int Locale;
        public bool Sound = true;
        
        private readonly List<string> _languages = new() { "ru", "be", "kk", "uk", "uz" };

        public SettingsData() =>
            Locale = _languages.Contains(YandexGame.EnvironmentData.language) ? 0 : 1;
    }
}