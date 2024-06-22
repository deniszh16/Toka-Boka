using DZGames.TokaBoka.Data;

namespace DZGames.TokaBoka.Services
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}