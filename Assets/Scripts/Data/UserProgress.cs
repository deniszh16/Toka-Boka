using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress;
        public int Hearts;
        public int Locale;
        
        public UserProgress()
        {
            Progress = 1;
        }

        public void ChangeNumberOfHearts(int value)
        {
            Hearts += value;
            
            if (Hearts < 0)
                Hearts = 0;
        }
    }
}