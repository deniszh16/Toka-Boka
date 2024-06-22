using DZGames.TokaBoka.ListOfLevels;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DZGames.TokaBoka.LifetimeScopes
{
    public class LevelsLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelSelection _levelSelection;
        [SerializeField] private OpenPets _openPets;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindLevelSelection(builder);
            BindOpenPets(builder);
        }
        
        private void BindLevelSelection(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelSelection);
        
        private void BindOpenPets(IContainerBuilder builder) =>
            builder.RegisterComponent(_openPets);
    }
}