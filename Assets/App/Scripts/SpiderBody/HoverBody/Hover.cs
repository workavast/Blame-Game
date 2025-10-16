using System;
using App.Ecs.Characters;
using App.SpiderBody.IK_Legs;
using DCFApixels;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

namespace App.SpiderBody.HoverBody
{
    public partial class Hover : MonoBehaviour
    {
        [SerializeField] private RigBuilder rigBuilder;
        [SerializeField] private CharacterView viewRef;
        [SerializeField] private SpringData[] springs;
        [Space]
        [SerializeField] private IK_LegPoint[] legs;
        [SerializeField] private int legsCountToBeGrounded = 2;
        [Space]
        [SerializeField] private LayerMask groundLayers;
        [SerializeField] private float minHoverHeight = 2f;
        [SerializeField] private float maxHoverHeight = 3f;
        [Space]
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float movePower;
        [SerializeField] private float moveDamping;
        [SerializeField] private float maxSpeed;
        [SerializeField] private AnimationCurve speedPerHeight;
        
        public float MaxSpeed => maxSpeed;
        public float MinHeight => minHoverHeight;
        public float MaxHeight => maxHoverHeight;
        public float TargetHeight => _targetHeight;
        public float CurrentLinerVelocity { get; private set; }

        private Vector2 _moveDirectionInput;
        private float _rotationInput;
        private float _heightInput;
        private float _targetHeight;
        
        private void OnMove(InputValue move) 
            => _moveDirectionInput = move.Get<Vector2>();

        private void OnRotation(InputValue move) 
            => _rotationInput = move.Get<float>();
        
        private void OnHoverHeight(InputValue move) 
            => _heightInput = move.Get<float>();

        private void Awake()
        {
            _targetHeight = Mathf.Clamp((maxHoverHeight + minHoverHeight) / 2, minHoverHeight, maxHoverHeight);
        }

        private void Update()
        {
            DrawGizmos();
        }

        private void FixedUpdate()
        {
            CurrentLinerVelocity = viewRef.Velocity;
            var grounded = IsGrounded();
            if (grounded)
            {
                var ray = new Ray(transform.position, Vector3.down);
                if (Physics.Raycast(ray, out var hit, 10f))
                {
                    var heightDif = _targetHeight/2 - hit.distance;
                    transform.position += Vector3.up * heightDif;
                }
            }
            else
            {
                //just to draw gizmos correctly
                for (var i = 0; i < springs.Length; i++) 
                    springs[i].GetAverageHeight(_targetHeight * 1.5f);
            }
        }

        public void SetVelocityValue(float velocity)
        {
            CurrentLinerVelocity = velocity;
        }

        private bool IsGrounded()
        {
            var groundedCount = 0;
            foreach (var leg in legs)
                if (leg.IsGrounded) 
                    groundedCount++;

            isGrounded_DB = groundedCount >= legsCountToBeGrounded;
            return groundedCount >= legsCountToBeGrounded;
        }

        private float GetHeightPercentage() 
            => (_targetHeight - minHoverHeight) / (maxHoverHeight - minHoverHeight);
        
        [Serializable]
        private struct SpringData
        {
            [SerializeField] private Transform spring;
            [SerializeField] private IK_LegPoint[] legPoints;

            public Transform Transform => spring;
            public Vector3 Position => spring.position;
            
            [SerializeField] private float averageHeight_DB;
            [SerializeField] private float[] averageHeights_DB;

            public float GetAverageHeight(float defaultHeight)
            {
                var height = 0f;

                for (var i = 0; i < legPoints.Length; i++)
                {
                    if (legPoints[i].IsGrounded)
                    {
                        var legPoint = legPoints[i].LegPoint.position;
                        var localLegPoint = spring.InverseTransformPoint(legPoint);
                        height += -localLegPoint.y;
                        averageHeights_DB[i] = -localLegPoint.y;                        
                    }
                    else
                    {
                        height += defaultHeight;
                        averageHeights_DB[i] = defaultHeight;
                    }
                }

                height /= legPoints.Length;
                averageHeight_DB = height;
                
                return height;
            }

            public void DrawAveragePoint(float targetHeight)
            {
                DebugX.Draw(Color.black).Sphere(spring.position, 0.2f);
                DebugX.Draw(Color.black).Line(spring.position, spring.position + -spring.up * targetHeight * 2f);
                DebugX.Draw(Color.black).Sphere(spring.position + -spring.up * averageHeight_DB, 0.2f);
            }
        }
    }
}