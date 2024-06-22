using System.Threading;
using Cysharp.Threading.Tasks;

namespace TokaBoka.Services
{
    public interface ISceneLoaderService
    {
        public UniTask LoadSceneAsync(int scene, bool screensaver, float delay, CancellationToken token);
        public UniTask LoadLevelAsync(int levelNumber, CancellationToken token);
    }
}