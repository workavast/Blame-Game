using App.SpiderBody.HoverBody;
using UnityEngine;

namespace App.SpiderBody.IK_Legs
{
    public class IK_LegsData : MonoBehaviour
    {
        [SerializeField] private Hover hover;
        [Space]
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private float isGroundCheckRadius = 0.5f;
        [SerializeField] private float stepHeight = 1;
        [SerializeField] private float stepDistance = 1;
        [SerializeField] private float speed = 1;
        [SerializeField] private float groundSpeed = 1;
        [SerializeField] private Vector2 stepDistanceRandomOffset = new(-0.1f, 0.1f);
        [SerializeField] private AnimationCurve distanceScalePerSpeed;
        [SerializeField] private float deepDistance = 4f;

        public Hover Hover => hover;
        public LayerMask GroundLayers => groundLayers;
        public float CurrentLinerVelocity => Hover.CurrentLinerVelocity;
        public float IsGroundCheckRadius => isGroundCheckRadius;
        public float StepHeight => stepHeight;
        public float StepDistance => stepDistance;
        public float Speed => speed;
        public float GroundSpeed => groundSpeed;
        public Vector2 StepDistanceRandomOffset => stepDistanceRandomOffset;
        public AnimationCurve DistanceScalePerSpeed => distanceScalePerSpeed;
        public float DeepDistance => deepDistance;
    }
}