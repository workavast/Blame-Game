using App.FSM;
using App.SpiderBody.HoverBody;
using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public abstract class IK_LegState : StateBase
    {
        private readonly IK_LegData _ikLegData;
        
        protected bool HasGround_DB { get => _ikLegData.HasGround_DB; set => _ikLegData.HasGround_DB = value; }

        protected IK_LegPoint LegPointPair => _ikLegData.LegPointPair;
        protected Transform IKTarget => _ikLegData.IkTarget;
        protected Transform LegGroundPoint => _ikLegData.LegGroundPoint;
        protected Transform LegAirPoint => _ikLegData.LegAirPoint;

        protected bool IsGrounded => _ikLegData.IsGrounded;
        protected Vector3 RayVerticalOffset => _ikLegData.RayVerticalOffset;

        private float CurrentLinerVelocity => _ikLegData.CurrentLinerVelocity;
        protected Hover Hover => _ikLegData.Hover;
        protected LayerMask GroundLayers => _ikLegData.GroundLayers;
        protected float StepHeight => _ikLegData.StepHeight;
        protected float StepDistance => _ikLegData.StepDistance;
        protected float Speed => _ikLegData.Speed;
        protected float GroundSpeed => _ikLegData.GroundSpeed;
        protected Vector2 StepDistanceRandomOffset => _ikLegData.StepDistanceRandomOffset;
        protected AnimationCurve DistanceScalePerSpeed => _ikLegData.DistanceScalePerSpeed;
        protected float DeepDistance => _ikLegData.DeepDistance;
        
        protected bool IsAnimate { get => _ikLegData.IsAnimate; set => _ikLegData.IsAnimate = value; }
        protected float CurrentStepDistanceRandomOffset { get => _ikLegData.CurrentStepDistanceRandomOffset; set => _ikLegData.CurrentStepDistanceRandomOffset = value; }
        protected Vector3 LastTargetPosition { get => _ikLegData.LastTargetPosition; set => _ikLegData.LastTargetPosition = value; }
        protected Vector3 TargetPosition { get => _ikLegData.TargetPosition; set => _ikLegData.TargetPosition = value; }
        protected Vector3 CurrentPosition { get => _ikLegData.CurrentPosition; set => _ikLegData.CurrentPosition = value; }
        protected float LerpTime { get => _ikLegData.LerpTime; set => _ikLegData.LerpTime = value; }
        
        protected IK_LegState(IK_LegPoint ikLegPoint)
        {
            _ikLegData = ikLegPoint.ikLegData;
        }
        
        protected float GetStepDistance()
        {
            var moveSpeedPercentage = CurrentLinerVelocity / Hover.MaxSpeed;
            var distanceScale = DistanceScalePerSpeed.Evaluate(moveSpeedPercentage);
            var distance = distanceScale * (StepDistance + CurrentStepDistanceRandomOffset);
            return distance;
        }
    }
}