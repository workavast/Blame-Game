using Unity.Mathematics;

namespace App.Ecs.Utills
{
    public struct RandomPosition
    {
        public static float3 GetPointOnRadius(float3 center, float radius, ref Random random)
        {
            var spawnAngle = random.NextFloat(0f, math.TAU);
            var spawnPoint = new float3()
            {
                x = math.sin(spawnAngle),
                y= 0f,
                z = math.cos(spawnAngle),
            };
            spawnPoint *= radius;
            spawnPoint += center;

            return spawnPoint;
        }
        
        public static float3 GetPointInRadius(float3 center, float minRadius, float maxRadius, ref Random random)
        {
            var spawnAngle = random.NextFloat(0f, math.TAU);
            var radius = random.NextFloat(minRadius, maxRadius);
                
            var spawnPoint = new float3()
            {
                x = math.sin(spawnAngle),
                y= 0f,
                z = math.cos(spawnAngle),
            };
            
            spawnPoint *= radius;
            spawnPoint += center;

            return spawnPoint;
        }
    }
}