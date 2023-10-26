using System;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress;
        public int Hearts;
        public int Locale;

        public event Action HeartsAdded;
        public event Action NotEnoughHearts;

        public UserProgress()
        {
            Progress = 1;
            Hearts = 100;
        }

        public void AddHearts(int value)
        {
            Hearts += value;
            HeartsAdded?.Invoke();
        }

        public bool SubtractHearts(int value)
        {
            if (Hearts < value)
            {
                NotEnoughHearts?.Invoke();
                return false;
            }
            
            Hearts -= value;
            HeartsAdded?.Invoke();
            return true;
        }
    }
}