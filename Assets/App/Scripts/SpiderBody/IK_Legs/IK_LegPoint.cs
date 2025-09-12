using System.Collections.Generic;
using App.FSM;
using App.SpiderBody.HoverBody;
using App.SpiderBody.IK_Legs.States;
using UnityEngine;

namespace App.SpiderBody.IK_Legs
{
    public partial class IK_LegPoint : MonoBehaviour
    {
        [SerializeField] public IK_LegData ikLegData;

        public Transform LegPoint => ikLegData.LegPoint;
        private Transform LegGroundPoint => ikLegData.LegGroundPoint;
        
        
        public bool IsGrounded 
        { 
            get => ikLegData.IsGrounded;
            private set => ikLegData.IsGrounded = value;
        }

        private StateMachine _fsm;
        
        public bool IsAnimate => ikLegData.IsAnimate;
        private float CurrentStepDistanceRandomOffset => ikLegData.CurrentStepDistanceRandomOffset;
        private Vector3 LastTargetPosition => ikLegData.LastTargetPosition;
        private Vector3 TargetPosition => ikLegData.TargetPosition;
        private Vector3 RayVerticalOffset => ikLegData.RayVerticalOffset;

        private float CurrentLinerVelocity => ikLegData.CurrentLinerVelocity;
        private Hover Hover => ikLegData.Hover;
        private LayerMask GroundLayers => ikLegData.GroundLayers;
        private float IsGroundCheckRadius => ikLegData.IsGroundCheckRadius;
        private float StepDistance => ikLegData.StepDistance;
        private Vector2 StepDistanceRandomOffset => ikLegData.StepDistanceRandomOffset;
        private AnimationCurve DistanceScalePerSpeed => ikLegData.DistanceScalePerSpeed;
        private float DeepDistance => ikLegData.DeepDistance;
        
        private void Awake()
        {
            _fsm = new StateMachine(new List<StateBase>
            {
                new Initial(this),
                new Stay(this), 
                new Step(this),
                new RiseUp(this, transform),
                new Lending(this, transform)
            });
            _fsm.Init<Initial>();
        }

        private void Update()
        {
            _fsm.ManualUpdate();
            DrawGizmos();
        }
        
        public void FixedUpdate()
        {
            IsGrounded = IsGroundedCheck();
            _fsm.ManualFixedUpdate();
        }

        private bool IsGroundedCheck() 
            => Physics.CheckSphere(LegPoint.position, IsGroundCheckRadius, GroundLayers);

        private float GetStepDistance()
        {
            var moveSpeedPercentage = CurrentLinerVelocity / Hover.MaxSpeed;
            var distanceScale = DistanceScalePerSpeed.Evaluate(moveSpeedPercentage);
            var distance = distanceScale * (StepDistance + CurrentStepDistanceRandomOffset);
            return distance;
        }
    }
}