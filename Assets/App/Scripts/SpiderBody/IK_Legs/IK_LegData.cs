using System;
using App.SpiderBody.HoverBody;
using UnityEngine;

namespace App.SpiderBody.IK_Legs
{
    [Serializable]
    public class IK_LegData
    {
        [SerializeField] private IK_LegsData ikLegsData;
        [SerializeField] private IK_LegPoint legPointPair;
        [SerializeField] private Transform legPoint;
        [Space]
        [SerializeField] private Transform ikTarget;
        [SerializeField] private Transform legGroundPoint;
        [SerializeField] private Transform legAirPoint;

        [field: Space, Header("Debug")]
        [field: SerializeField] public bool HasGround_DB { get; set; }
        
        public bool IsGrounded { get; set; }
        public bool IsAnimate { get; set; }
        public float CurrentStepDistanceRandomOffset { get; set; }
        public Vector3 LastTargetPosition { get; set; }
        public Vector3 TargetPosition { get; set; }
        public Vector3 CurrentPosition { get; set; }
        public float LerpTime { get; set; }
        
        public readonly Vector3 RayVerticalOffset = Vector3.up * 2;
        
        //readonly SerializeField
        public IK_LegPoint LegPointPair => legPointPair;
        public Transform LegPoint => legPoint;
        public Transform IkTarget => ikTarget;
        public Transform LegGroundPoint => legGroundPoint;
        public Transform LegAirPoint => legAirPoint;
        
        //readonly ikLegsData
        // public float CurrentLinerVelocity => ikLegsData.Rb.linearVelocity.magnitude;
        public float CurrentLinerVelocity => ikLegsData.CurrentLinerVelocity;
        
        
        public Hover Hover => ikLegsData.Hover;
        public LayerMask GroundLayers => ikLegsData.GroundLayers;
        public float IsGroundCheckRadius => ikLegsData.IsGroundCheckRadius;
        public float StepHeight => ikLegsData.StepHeight;
        public float StepDistance => ikLegsData.StepDistance;
        public float Speed => ikLegsData.Speed;
        public float GroundSpeed => ikLegsData.GroundSpeed;
        public Vector2 StepDistanceRandomOffset => ikLegsData.StepDistanceRandomOffset;
        public AnimationCurve DistanceScalePerSpeed => ikLegsData.DistanceScalePerSpeed;
        public float DeepDistance => ikLegsData.DeepDistance;
    }
}