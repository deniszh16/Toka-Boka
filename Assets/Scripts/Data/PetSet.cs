using System;

namespace Data
{
    [Serializable]
    public class PetSet
    {
        public bool[] Pets;
        public int OpenPets;

        public event Action OpenPetsChanged;

        public PetSet() =>
            Pets = new bool[3];

        public void IncreaseNumberOfOpenPets()
        {
            OpenPets++;
            OpenPetsChanged?.Invoke();
        }
    }
}