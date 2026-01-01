using UnityEngine;
using UnityEngine.EventSystems;

namespace App.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIHoverTilt : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IPointerMoveHandler
    {
        [Header("References")]
        [SerializeField] private RectTransform visualTarget;
        [SerializeField] private RectTransform shadow;

        [Header("Scale")]
        [SerializeField] private float hoverScale = 1.1f;
        [SerializeField] private float scaleLerpSpeed = 10f;

        [Header("Tilt")]
        [SerializeField] private float maxTiltAngle = 10f;
        [SerializeField] private float tiltLerpSpeed = 10f;
        [SerializeField] private float inputSmoothingSpeed = 15f;

        [Header("Shadow")]
        [SerializeField] private float maxShadowOffset = 20f;
        [SerializeField] private float shadowFollowSpeed = 12f;

        private RectTransform _inputArea;
        private Vector3 _baseScale;
        private Quaternion _baseRotation;

        private Vector2 _smoothedInput;
        private Vector2 _pointerScreenPosition;
        private bool _isHovered;

        private void Awake()
        {
            _inputArea = GetComponent<RectTransform>();

            _baseScale = visualTarget.localScale;
            _baseRotation = visualTarget.localRotation;

            if (shadow != null)
            {
                shadow.anchoredPosition = Vector2.zero;
                shadow.localRotation = Quaternion.identity;
            }
        }

        private void OnEnable()
        {
            ApplyTilt(_baseRotation, false);
            SetScale(_baseScale, false);
            UpdateShadow(Vector2.zero, false);
        }

        private void OnDisable()
        {
            OnPointerExit(null);
        }

        private void Update()
        {
            UpdateScale();
            UpdateTilt();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
            _pointerScreenPosition = eventData.position;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _pointerScreenPosition = eventData.position;
        }

        private void UpdateScale()
        {
            var targetScale = _isHovered
                ? _baseScale * hoverScale
                : _baseScale;

            SetScale(targetScale, true);
        }

        private void SetScale(Vector3 targetScale, bool smooth)
        {
            if (smooth)
            {
                visualTarget.localScale = Vector3.Lerp(
                    visualTarget.localScale,
                    targetScale,
                    Time.unscaledDeltaTime * scaleLerpSpeed
                );
            }
            else
            {
                visualTarget.localScale = targetScale;
            }
        }

        private void UpdateTilt()
        {
            if (!_isHovered)
            {
                ApplyTilt(_baseRotation, true);
                UpdateShadow(Vector2.zero, true);
                return;
            }

            var normalizedInput = CalculateNormalizedInput();
            _smoothedInput = Vector2.Lerp(
                _smoothedInput,
                normalizedInput,
                Time.unscaledDeltaTime * inputSmoothingSpeed
            );

            CalculateTilt(_smoothedInput);
            UpdateShadow(_smoothedInput, true);
        }

        private void ApplyTilt(Quaternion targetRotation, bool smooth)
        {
            if (smooth)
            {
                visualTarget.localRotation = Quaternion.Lerp(
                    visualTarget.localRotation,
                    targetRotation,
                    Time.unscaledDeltaTime * tiltLerpSpeed
                );   
            }
            else
            {
                visualTarget.localRotation = targetRotation;
            }
        }

        private void CalculateTilt(Vector2 input)
        {
            var targetRotation = Quaternion.Euler(
                -input.y * maxTiltAngle,
                input.x * maxTiltAngle,
                0f
            );

            ApplyTilt(targetRotation, true);
        }

        private Vector2 CalculateNormalizedInput()
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _inputArea,
                _pointerScreenPosition,
                null,
                out var localCursor
            );

            var halfWidth = _inputArea.rect.width * 0.5f;
            var halfHeight = _inputArea.rect.height * 0.5f;

            return new Vector2(
                SoftNormalize(localCursor.x / halfWidth),
                SoftNormalize(localCursor.y / halfHeight)
            );
        }

        private void UpdateShadow(Vector2 input, bool smooth)
        {
            if (shadow == null)
                return;

            var targetOffset = -input * maxShadowOffset;
            if (smooth)
            {
                shadow.anchoredPosition = Vector2.Lerp(
                    shadow.anchoredPosition,
                    targetOffset,
                    Time.unscaledDeltaTime * shadowFollowSpeed
                );
            }
            else
            {
                shadow.anchoredPosition = targetOffset;
            }


            shadow.localScale = visualTarget.localScale;
            shadow.localRotation = visualTarget.localRotation;
        }

        private static float SoftNormalize(float value)
        {
            value = Mathf.Clamp(value, -1f, 1f);
            return Mathf.Sign(value) * Mathf.SmoothStep(0f, 1f, Mathf.Abs(value));
        }
    }
}
