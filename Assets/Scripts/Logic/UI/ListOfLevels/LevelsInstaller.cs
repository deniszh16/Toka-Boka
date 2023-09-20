using UnityEngine;
using Zenject;

namespace Logic.UI.ListOfLevels
{
    public class LevelsInstaller : MonoInstaller
    {
        [SerializeField] private LevelSelection _levelSelection;
        
        public override void InstallBindings() =>
            BindLevelSelection();

        private void BindLevelSelection() =>
            Container.BindInstance(_levelSelection).AsSingle();
    }
}