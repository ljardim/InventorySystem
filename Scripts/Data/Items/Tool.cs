using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "Tool", menuName = "LJardim/Inventory/Items/Tool", order = 0)]
    public class Tool : HoldableItem {
        public float maxDurability;
        private void Awake() {
            type = ItemType.Tool;
        }
    }
}