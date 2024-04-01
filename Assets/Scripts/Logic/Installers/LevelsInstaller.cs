using Logic.ListOfLevels;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class LevelsInstaller : MonoInstaller
    {
        [SerializeField] private LevelSelection _levelSelection;
        [SerializeField] private OpenPets _openPets;
        
        public override void InstallBindings()
        {
            BindLevelSelection();
            BindOpenPets();
        }

        private void BindLevelSelection() =>
            Container.BindInstance(_levelSelection).AsSingle();
        
        private void BindOpenPets() =>
            Container.BindInstance(_openPets).AsSingle();
    }
}