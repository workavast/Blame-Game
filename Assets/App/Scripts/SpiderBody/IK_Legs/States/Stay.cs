using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public class Stay : IK_LegState
    {
        public Stay(IK_LegPoint ikLegPoint) 
            : base(ikLegPoint) { }

        public override void Enter(string[] args) { }

        public override void Exit() { }

        public override void ManualUpdate() 
            => Animate();

        public override void ManualFixedUpdate() 
            => CheckState();

        private void Animate()
        {
            IsAnimate = false;
            CurrentPosition = LastTargetPosition = TargetPosition;
            IKTarget.position = CurrentPosition;
        }

        private void CheckState()
        {
            var ray = new Ray(LegGroundPoint.position + RayVerticalOffset, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, DeepDistance, GroundLayers))
            {
                HasGround_DB = true;
                var stepDistance = GetStepDistance();

                var targetPositionXZ = new Vector2(TargetPosition.x, TargetPosition.z);
                var hitPointXZ = new Vector2(hitInfo.point.x, hitInfo.point.z);
                if (Vector2.Distance(targetPositionXZ, hitPointXZ) > stepDistance)
                {
                    if (IsAnimate)
                        LastTargetPosition = CurrentPosition;

                    CurrentStepDistanceRandomOffset = Random.Range(StepDistanceRandomOffset.x, StepDistanceRandomOffset.y);
                    LerpTime = 0;

                    var direction = (hitInfo.point - TargetPosition).normalized;
                    var nextTargetRayPoint = LegGroundPoint.position + direction * stepDistance / 2;
                    nextTargetRayPoint.y = LegGroundPoint.position.y;
                    ray = new Ray(nextTargetRayPoint + RayVerticalOffset, Vector3.down);
                    if (Physics.Raycast(ray, out var hitInfoV2, DeepDistance, GroundLayers))
                        TargetPosition = hitInfoV2.point;
                    else
                        TargetPosition = hitInfo.point;
                    
                    SetState<Step>();
                    return;
                }
            }
            else
            {
                if (!IsGrounded)
                {
                    SetState<RiseUp>();
                    return;
                }
                else
                {
                    var stepDistance = GetStepDistance();
                    var legGroundPositionXZ = new Vector2(LegGroundPoint.position.x, LegGroundPoint.position.z);
                    var currentPositionXZ = new Vector2(CurrentPosition.x, CurrentPosition.z);
                    if (Vector2.Distance(legGroundPositionXZ, currentPositionXZ) > stepDistance)
                    {
                        SetState<RiseUp>();
                        return;
                    } 
                }
            }
        }
    }
}