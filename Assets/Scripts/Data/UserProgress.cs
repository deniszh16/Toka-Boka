using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress;
        public int Locale;
        
        public UserProgress()
        {
            Progress = 1;
        }
    }
}