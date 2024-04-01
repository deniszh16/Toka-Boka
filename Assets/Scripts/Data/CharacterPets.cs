﻿using System;

namespace Data
{
    [Serializable]
    public class CharacterPets
    {
        public bool[] Pets = new bool[3];
        public int OpenPets;

        public void IncreaseNumberOfOpenPets() =>
            OpenPets++;
    }
}