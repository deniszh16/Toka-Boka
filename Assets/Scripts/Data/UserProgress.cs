using System;
using System.Collections.Generic;

namespace DZGames.TokaBoka.Data
{
    [Serializable]
    public class UserProgress
    {
        public int Progress = 1;
        public int Hearts = 25;

        public List<int> Attempts;
        public List<int> Stars;
        public List<CharacterPets> CharacterPets;

        public event Action HeartsAdded;
        public event Action NotEnoughHearts;
        public event Action StarsChanged;

        public SettingsData SettingsData  = new();

        public UserProgress()
        {
            Attempts = new List<int>(capacity: 12);
            Stars = new List<int>(capacity: 12);
            CharacterPets = new List<CharacterPets>(capacity: 12);

            for (int i = 0; i < Stars.Capacity; i++)
            {
                Attempts.Add(item: 0);
                Stars.Add(item: 0);
                CharacterPets.Add(item: new CharacterPets());
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

        public int GetNumberOfOpenPets(int characterNumber) =>
            CharacterPets[characterNumber - 1].OpenPets;
    }
}