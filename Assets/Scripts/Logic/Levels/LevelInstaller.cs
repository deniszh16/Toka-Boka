using Services.StateMachine;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelItems _levelItems;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindLevelItems();
        }

        private void BindGameStateMachine()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            Container.BindInstance(gameStateMachine).AsSingle();
        }

        private void BindLevelItems() =>
            Container.BindInstance(_levelItems).AsSingle();
    }
}