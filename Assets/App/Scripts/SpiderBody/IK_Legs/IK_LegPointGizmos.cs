using App.SpiderBody.HoverBody;
using DCFApixels;
using UnityEngine;

namespace App.SpiderBody.IK_Legs
{
    public partial class IK_LegPoint
    {
        [Space, SerializeField] private HoverDebugData debugData;
        
        public void SetHoverDebugData(HoverDebugData newDebugData) 
            => debugData = newDebugData;

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                DrawGizmos();
        }

        private void DrawGizmos()
        {
            if(debugData.drawTargetPosition) 
                DrawTargetPosition();
            if(debugData.drawStepSphere) 
                DrawStepSphere();
            if(debugData.drawHolderPoint) 
                DrawGroundPoint();
            if(debugData.drawIsGroundCheck) 
                DrawIsGroundCheck();
        }

        private void DrawTargetPosition()
        {
            DebugX.Draw(Color.red).Sphere(TargetPosition, 0.25f);
        }

        private void DrawStepSphere()
        {
            if (Application.isPlaying)
                DebugX.Draw(Color.blue).WireSphere(LastTargetPosition, GetStepDistance());
            else
                DebugX.Draw(Color.blue).WireSphere(LegGroundPoint.position, StepDistance);
        }

        private void DrawGroundPoint()
        {
            DebugX.Draw(Color.black).WireSphere(LegGroundPoint.position, 0.15f);
            DebugX.Draw(Color.black).Line(LegGroundPoint.position + RayVerticalOffset, LegGroundPoint.position - Vector3.up * DeepDistance + RayVerticalOffset);
        }

        private void DrawIsGroundCheck()
        {
            DebugX.Draw(Color.white).WireSphere(LegPoint.position, IsGroundCheckRadius);
        }
    }
}