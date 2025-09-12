using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public class Initial : IK_LegState
    {
        public Initial(IK_LegPoint ikLegPoint) 
            : base(ikLegPoint) { }

        public override void Enter(string[] args)
        {
            CurrentStepDistanceRandomOffset = Random.Range(StepDistanceRandomOffset.x, StepDistanceRandomOffset.y);
            LerpTime = 1;
        }

        public override void Exit() { }

        public override void ManualUpdate() { }

        public override void ManualFixedUpdate()
        {
            var ray = new Ray(LegGroundPoint.position + RayVerticalOffset, Vector3.down);
            if (Physics.Raycast(ray, out var hitInfo, 10, GroundLayers))
            {
                HasGround_DB = true;
                LastTargetPosition = TargetPosition = CurrentPosition = hitInfo.point;
                SetState<Stay>();
            }
            else
            {
                HasGround_DB = false;
                LastTargetPosition = TargetPosition = CurrentPosition = LegAirPoint.position;
                SetState<RiseUp>();
            }
        }
    }
}