﻿using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress;
        public int Hearts;

        public List<int> Attempts;
        public List<int> Stars;
        public List<PetSet> PetSets;

        public int Locale;

        public event Action HeartsAdded;
        public event Action NotEnoughHearts;
        public event Action StarsChanged; 

        public UserProgress()
        {
            Progress = 1;
            Hearts = 100;

            Attempts = new List<int>(capacity: 12);
            Stars = new List<int>(capacity: 12);
            PetSets = new List<PetSet>(capacity: 12);

            for (int i = 0; i < Stars.Capacity; i++)
            {
                Attempts.Add(item: 0);
                Stars.Add(item: 0);
                PetSets.Add(item: new PetSet());
            }
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

        public void ChangeAttempts(int levelNumber) =>
            Attempts[levelNumber] += 1;

        public int GetNumberOfStars(int levelNumber) =>
            Stars[levelNumber];

        public void ChangeStars(int levelNumber, int value)
        {
            Stars[levelNumber] += value;
            StarsChanged?.Invoke();
        }
    }
}