using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress;
        public int Hearts;

        public List<int> Stars;
        
        public int Locale;

        public event Action HeartsAdded;
        public event Action NotEnoughHearts;
        public event Action StarsChanged; 

        public UserProgress()
        {
            Progress = 1;
            Hearts = 100;

            Stars = new List<int>(capacity: 12);
            for (int i = 0; i < Stars.Capacity; i++)
                Stars.Add(item: 0);
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

        public void ChangeStars(int levelNumber, int value)
        {
            Stars[levelNumber] += value;
            StarsChanged?.Invoke();
        }
    }
}