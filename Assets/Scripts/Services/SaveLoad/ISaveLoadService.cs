using TokaBoka.Data;

namespace TokaBoka.Services
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}