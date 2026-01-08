using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.UI.Links
{
    [RequireComponent(typeof(TMP_Text))]
    public class LinkReader : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private LinksConfig linksConfig;

        private TMP_Text _textComponent;

        /// Regex parse id of ID_1 style
        private static readonly Regex TrailingNumber = new Regex("(\\d+)$", RegexOptions.Compiled);

        private void Awake()
        {
            _textComponent = GetComponent<TMP_Text>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(_textComponent, eventData.position, eventData.pressEventCamera);
            if (linkIndex == -1) 
                return;

            var linkInfo = _textComponent.textInfo.linkInfo[linkIndex];
            var id = linkInfo.GetLinkID();

            var url = ResolveLinkFromId(id);
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogWarning($"LinkReader: cannot resolve link for id '{id}'");
                return;
            }

            Application.OpenURL(url);
        }

        private string ResolveLinkFromId(string idStr)
        {
            if (string.IsNullOrEmpty(idStr) || linksConfig == null)
                return null;

            var match = TrailingNumber.Match(idStr);
            if (!match.Success)
            {
                Debug.LogWarning($"LinkReader: can't parse number from id '{idStr}'");
                return null;
            }

            if (!int.TryParse(match.Value, out var id)) 
                return null;

            if (id >= 0 && id < linksConfig.Count) 
                return linksConfig.GetLinkByIndex(id);

            Debug.LogWarning($"LinkReader: index {id} not found in config (size {linksConfig.Count})");
            return null;
        }
    }
}
