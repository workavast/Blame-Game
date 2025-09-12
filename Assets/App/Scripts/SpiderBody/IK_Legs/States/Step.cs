using UnityEngine;

namespace App.SpiderBody.IK_Legs.States
{
    public class Step : IK_LegState
    {
        public Step(IK_LegPoint ikLegPoint) 
            : base(ikLegPoint) { }

        public override void Enter(string[] args) { }

        public override void Exit() { }

        public override void ManualUpdate() 
            => Animate();

        public override void ManualFixedUpdate()
        {
            CheckState();
        }
        
        private void Animate()
        {
            if (LerpTime < 1)
            {
                if (!LegPointPair.IsAnimate)
                {
                    IsAnimate = true;
                    var footPos = Vector3.Lerp(LastTargetPosition, TargetPosition, LerpTime);
                    footPos.y += Mathf.Sin(LerpTime * Mathf.PI) * StepHeight;
                    
                    CurrentPosition = footPos;
                    LerpTime += Time.deltaTime * Speed;
                }
            }
            else
            {
                IsAnimate = false;
                CurrentPosition = LastTargetPosition = TargetPosition;
            }

            IKTarget.position = CurrentPosition;
        }

        private void CheckState()
        {
            if (LerpTime >= 1)
            {
                SetState<Stay>();
                return;
            }
            
            var ray = new Ray(LegGroundPoint.position + RayVerticalOffset, Vector3.down);
            if (!Physics.Raycast(ray, out _, DeepDistance, GroundLayers))
            {
                SetState<RiseUp>();
                return;
            }
        }
    }
}