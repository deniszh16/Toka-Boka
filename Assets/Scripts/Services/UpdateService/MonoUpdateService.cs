﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace TokaBoka.Services
{
    public class MonoUpdateService : MonoBehaviour, IMonoUpdateService
    {
        private List<Action> _updateActions = new();
        
        public void AddToUpdate(Action action) =>
            _updateActions.Add(action);
        
        public void RemoveFromUpdate(Action action) =>
            _updateActions.Remove(action);
        
        private void Update()
        {
            foreach (Action action in _updateActions)
                action?.Invoke();
        }
    }
}