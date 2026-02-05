using UnityEngine;
using UnityEngine.UI;

namespace App.Perks.CooldownViews
{
    public class UiCooldownViewCell : MonoBehaviour
    {
        [SerializeField] private Sprite[] bgs;
        [SerializeField] private Image bg;
        [SerializeField] private Image icon;
        [SerializeField] private Image fillImage;

        public void UpdateOrder(UiCooldownViewCellOrder order) 
            => bg.sprite = bgs[(int)order];

        public void SetIcon(Sprite sprite) 
            => icon.sprite = sprite;

        public void UpdateView(float percentage) 
            => fillImage.fillAmount = percentage;
    }
}