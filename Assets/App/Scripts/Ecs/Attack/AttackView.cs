using UnityEngine;

namespace App.Ecs.Attack
{
    public class AttackView : MonoBehaviour
    {
        [SerializeField] private ParticleProvider attackVfx;
        
        public void PerformAttack() 
            => attackVfx.Play();
    }
}