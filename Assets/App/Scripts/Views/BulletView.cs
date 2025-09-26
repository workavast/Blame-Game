using Unity.Mathematics;

namespace App.Views
{
    public class BulletView : CleanupView
    {
        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation) 
            => transform.rotation = rotation;
    }
}