using DZGames.TokaBoka.Data;

namespace DZGames.TokaBoka.Services
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; }
        public void SetUserProgress(UserProgress progress);
    }
}