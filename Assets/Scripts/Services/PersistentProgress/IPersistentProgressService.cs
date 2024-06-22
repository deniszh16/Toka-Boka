using TokaBoka.Data;

namespace TokaBoka.Services
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; }
        public void SetUserProgress(UserProgress progress);
    }
}