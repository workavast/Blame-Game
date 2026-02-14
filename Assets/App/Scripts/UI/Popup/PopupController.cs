using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Popup
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField] private Canvas parentCanvas;
        [SerializeField] private DynamicPopup dynamicPopup;
        [Header("Behavior")]
        [Tooltip("Offset from the cursor in pixels (x,y)")]
        [SerializeField] private Vector2 offset = new Vector2(10f, 10f);

        private RectTransform _canvasRect;
        private RectTransform _popupRect;

        private void Awake()
        {
            _popupRect = dynamicPopup.GetComponent<RectTransform>();
            _canvasRect = parentCanvas.GetComponent<RectTransform>();

            Hide();
        }

        private void OnDisable()
        {
            Hide();
        }

        public void Show(Vector2 screenPosition, string title, string description)
        {
            dynamicPopup.SetData(title, description);
            dynamicPopup.Show();

            // Update layout to take valid size
            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_popupRect);

            var popupSize = _popupRect.rect.size;

            // Convert offset from pixels to canvas metrics
            var scale = parentCanvas != null ? parentCanvas.scaleFactor : 1f;
            var offsetCanvas = offset / scale;

            // Convert screen point to canvas local point
            Vector2 localPoint;
            var cam =
                (parentCanvas != null && parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
                ? parentCanvas.worldCamera
                : null;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvasRect ?? _popupRect,
                screenPosition,
                cam,
                out localPoint);

            // Compute pivot using canvas bounds when available so popup tries to stay inside canvas
            var pivot = ComputePivots(localPoint, popupSize, offsetCanvas);

            _popupRect.pivot = pivot;

            var anchoredPos = localPoint;
            anchoredPos += new Vector2(pivot.x == 0f ? offsetCanvas.x : -offsetCanvas.x,
                pivot.y == 1f ? -offsetCanvas.y : offsetCanvas.y);

            anchoredPos = ClampAnchoredPosInCanvas(anchoredPos, pivot, popupSize);
            _popupRect.anchoredPosition = anchoredPos;
        }
        
        public void Hide()
        {
            dynamicPopup.Hide();
        }

        private Vector2 ComputePivots(Vector2 localPoint, Vector2 popupSize, Vector2 offsetCanvas)
        {
            // Use canvas rect (local coordinates) to decide pivot so popup will try to stay inside canvas
            var canvasRect = _canvasRect.rect;

            var overRight = localPoint.x + popupSize.x + offsetCanvas.x > canvasRect.xMax;
            var belowBottom = localPoint.y - popupSize.y - offsetCanvas.y < canvasRect.yMin;

            var pivotX = overRight ? 1f : 0f;
            var pivotY = belowBottom ? 0f : 1f;

            return new Vector2(pivotX, pivotY);
        }

        private Vector2 ClampAnchoredPosInCanvas(Vector2 anchoredPos, Vector2 pivot, Vector2 popupSize)
        {
            var canvasRect = _canvasRect.rect;
            // Compute allowed min/max for anchoredPos such that popup edges stay inside canvas rect
            var minX = canvasRect.xMin + pivot.x * popupSize.x;
            var maxX = canvasRect.xMax - (1f - pivot.x) * popupSize.x;
            var minY = canvasRect.yMin + pivot.y * popupSize.y;
            var maxY = canvasRect.yMax - (1f - pivot.y) * popupSize.y;

            // Ensure min <= max
            var clampedX = Mathf.Clamp(anchoredPos.x, Mathf.Min(minX, maxX), Mathf.Max(minX, maxX));
            var clampedY = Mathf.Clamp(anchoredPos.y, Mathf.Min(minY, maxY), Mathf.Max(minY, maxY));
            return new Vector2(clampedX, clampedY);
        }
    }
}