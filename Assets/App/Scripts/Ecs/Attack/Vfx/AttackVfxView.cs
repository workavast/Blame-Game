using UnityEngine;

namespace App.Ecs.Attack.Vfx
{
    public class AttackVfxView : MonoBehaviour
    {
        [SerializeField] private ParticleProvider attackVfx;

        public void PerformAttack()
            => attackVfx.Play();
    }
}