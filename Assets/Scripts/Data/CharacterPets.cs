using System;

namespace Data
{
    [Serializable]
    public class CharacterPets
    {
        public bool[] Pets;
        public int OpenPets;

        public CharacterPets() =>
            Pets = new bool[3];

        public void IncreaseNumberOfOpenPets() =>
            OpenPets++;
    }
}