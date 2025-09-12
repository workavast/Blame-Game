using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public class RiseUp : IK_LegState
    {
        private readonly Transform _transform;
        private Vector3 _localLastTargetPosition;
        
        public RiseUp(IK_LegPoint ikLegPoint, Transform transform) : base(ikLegPoint)
        {
            _transform = transform;
        }

        public override void Enter(string[] args)
        {
            LastTargetPosition = CurrentPosition;
            _localLastTargetPosition = _transform.InverseTransformPoint(LastTargetPosition);
            HasGround_DB = false;
            LerpTime = 0;
        }

        public override void Exit() { }

        public override void ManualUpdate()
        {
            TargetPosition = LegAirPoint.position;
            Animate();
        }

        public override void ManualFixedUpdate() 
            => CheckState();

        private void CheckState()
        {
            var ray = new Ray(LegGroundPoint.position + RayVerticalOffset, Vector3.down);
            if (Physics.Raycast(ray, out _, DeepDistance, GroundLayers)) 
                SetState<Lending>();
        }

        private void Animate()
        {
            if (LerpTime < 1)
            {
                IsAnimate = true;
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
    }
}