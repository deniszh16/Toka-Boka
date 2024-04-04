using System;

namespace Services.UpdateService
{
    public interface IMonoUpdateService
    {
        public void AddToUpdate(Action action);
        public void RemoveFromUpdate(Action action);
    }
}