namespace Services.SceneLoader
{
    public interface ISceneLoaderService
    {
        public string CurrentScene { get; }
        public void LoadSceneAsync(Scenes scene, bool screensaver, float delay);
        public void LoadLevelAsync(int levelNumber);
    }
}