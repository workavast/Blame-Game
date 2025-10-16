using App.Ecs.Clenuping;
using Unity.Mathematics;

namespace App.Ecs.Bullets
{
    public class BulletView : CleanupView
    {
        public void SetPosition(float3 position) 
            => transform.position = position;

        public void SetRotation(quaternion rotation) 
            => transform.rotation = rotation;
    }
}