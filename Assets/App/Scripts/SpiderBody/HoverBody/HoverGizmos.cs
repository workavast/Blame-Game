using UnityEngine;

namespace App.SpiderBody.HoverBody
{
    public partial class Hover
    {
        [Space, Header("Debug")] 
        [SerializeField] private HoverDebugData debugData;
        [SerializeField] private Vector3 linearVelocity_DB;
        [SerializeField] private float velocityXZ_DB;
        [SerializeField] private float velocity_DB;
        [SerializeField] private bool isGrounded_DB;

        public void SetDebugData(HoverDebugData hoverDebugData)
        {
            debugData = hoverDebugData;

            for (var i = 0; i < legs.Length; i++) 
                legs[i].SetHoverDebugData(debugData);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                DrawGizmos();
        }

        private void DrawGizmos()
        {
            if (debugData.drawSprings)
                DrawSprings();

            if (debugData.drawVelocity) 
                DrawVelocity();
        }
        
        private void DrawSprings()
        {
            for (var i = 0; i < springs.Length; i++)
                springs[i].DrawAveragePoint(_targetHeight);
        }

        private void DrawVelocity()
        {
            // DebugX.Draw(Color.yellow).Line(transform.position + rb.centerOfMass, transform.position + rb.centerOfMass + rb.linearVelocity);
        }
    }
}