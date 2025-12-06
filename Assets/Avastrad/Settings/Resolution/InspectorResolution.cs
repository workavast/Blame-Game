using System;
using UnityEngine;

namespace Avastrad.Settings.Resolution
{
    [Serializable]
    public struct InspectorResolution : IEquatable<InspectorResolution>
    {
        [field: SerializeField, Min(0)] public int Width { get; private set; }
        [field: SerializeField, Min(0)] public int Height { get; private set; }

        public InspectorResolution(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public override int GetHashCode() 
            => HashCode.Combine(Width, Height);

        public override string ToString() 
            => $"{Width}x{Height}";

        public static bool operator ==(InspectorResolution left, InspectorResolution right) 
            => left.Width == right.Width && left.Height == right.Height;

        public static bool operator !=(InspectorResolution left, InspectorResolution right) 
            => !(left == right);
        
        public bool Equals(InspectorResolution other) 
            => Width == other.Width && Height == other.Height;

        public override bool Equals(object obj) 
            => obj is InspectorResolution other && Equals(other);
    }
}