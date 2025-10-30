using System;
using UnityEngine;

namespace App.ResolutionProviding
{
    public interface IResolutionProviderRO
    {
        public Vector2Int Resolution { get; }
        
        public event Action OnResolutionChanged;   
    }
}