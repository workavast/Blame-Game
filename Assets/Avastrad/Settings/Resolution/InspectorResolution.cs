using System;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    [Serializable]
    public struct InspectorResolution
    {
        [field: SerializeField, Min(0)] public int Width { get; private set; }
        [field: SerializeField, Min(0)] public int Height { get; private set; }

        public InspectorResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public static bool operator ==(InspectorResolution left, InspectorResolution right) 
            => left.Width == right.Width && left.Height == right.Height;

        public static bool operator !=(InspectorResolution left, InspectorResolution right) 
            => !(left == right);

        public override string ToString() => $"{Width}x{Height}";
    }
}