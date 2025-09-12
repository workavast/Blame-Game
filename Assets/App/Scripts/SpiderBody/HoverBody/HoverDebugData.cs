using System;
using UnityEngine;

namespace App.SpiderBody.HoverBody
{
    [Serializable]
    public struct HoverDebugData
    {
        [Header("Body")]
        public bool drawSprings;
        public bool drawVelocity;
        [Space]
        [Header("Legs")]
        public bool drawTargetPosition;
        public bool drawStepSphere;
        public bool drawHolderPoint;
        public bool drawIsGroundCheck;
    }
}
