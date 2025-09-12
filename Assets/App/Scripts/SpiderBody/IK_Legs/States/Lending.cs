using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public class Lending : IK_LegState
    {
        private readonly Transform _transform;
        private Vector3 _localLastTargetPosition;
        
        public Lending(IK_LegPoint ikLegPoint, Transform transform) : base(ikLegPoint)
        {
            _transform = transform;
        }

        public override void Enter(string[] args)
        {
            LastTargetPosition = CurrentPosition;
            _localLastTargetPosition = _transform.InverseTransformPoint(LastTargetPosition);
            HasGround_DB = true;
            LerpTime = 0;
        }

        public override void Exit() { }

        public override void ManualUpdate() 
            => Animate();

        public override void ManualFixedUpdate()
        {
            var hasGround = CalculateTargetPosition();
            CheckState(hasGround);
        }

        private void Animate()
        {
            if (LerpTime < 1)
            {
                IsAnimate = false;
                var footPos = Vector3.Lerp(_localLastTargetPosition, _transform.InverseTransformPoint(TargetPosition), LerpTime);

                CurrentPosition = _transform.TransformPoint(footPos);
                LerpTime += Time.deltaTime * GroundSpeed;
            }
            else
            {
                IsAnimate = false;
                CurrentPosition = LastTargetPosition = TargetPosition;
            }

            IKTarget.position = CurrentPosition;
        }
        
        private bool CalculateTargetPosition()
        {
            var ray = new Ray(LegGroundPoint.position + RayVerticalOffset, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, DeepDistance, GroundLayers))
            {
                TargetPosition = hitInfo.point;
                return true;
            }

            return false;
        }
        
        private void CheckState(bool hasGround)
        {
            if (!hasGround)
            {
                SetState<RiseUp>();
                return;
            }

            if (IsGrounded)
            {
                SetState<Stay>();
                return;
            }
        }
    }
}