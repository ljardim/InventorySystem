using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LJardim.InventorySystem {
    public class ItemUi : MonoBehaviour {
        [SerializeField]private Image icon;
        [SerializeField]private TextMeshProUGUI amount;
        [SerializeField]private Image durability;

        public Item Item { get; set; }
        public InventoryObject Inventory { get; set; }
        public int ItemIndex { get; set; }

        public void ClearSlot() {
            Item = null;
            Inventory = null;
        }

        public void RefreshSlot() {
            UpdateIcon();
            UpdateAmount();
            UpdateCondition();
        }

        private void UpdateCondition() {
            if (Item == null || Item.template == null || Item.durability == -1 || Item.MaxDurability < 1) {
                durability.enabled = false;
            } else {
                durability.enabled = true;
                var conditionPercent = Item.durability / Item.MaxDurability;
                var barWidth = (GetComponent<RectTransform>().rect.width - durability.rectTransform.anchoredPosition.x * 2) * conditionPercent;
                durability.rectTransform.sizeDelta = new Vector2(barWidth, durability.rectTransform.sizeDelta.y);
                durability.color = Color.Lerp(Color.red, Color.green, conditionPercent);
            }
        }

        private void UpdateAmount() {
            if (Item == null || Item.template == null || Item.amount < 2) {
                amount.enabled = false;
            } else {
                amount.enabled = true;
                amount.text = Item.amount.ToString();
            }
        }

        private void UpdateIcon() {
            if (Item == null || Item.template == null || Item.template.uiIcon == null) {
                icon.enabled = false;
            } else {
                icon.enabled = true;
                icon.sprite = Item.template.uiIcon;
            }
        }
    }
}