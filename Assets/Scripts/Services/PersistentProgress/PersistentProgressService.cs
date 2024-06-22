using DZGames.TokaBoka.Data;

namespace DZGames.TokaBoka.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; private set; }

        public void SetUserProgress(UserProgress progress) =>
            GetUserProgress = progress;
    }
}