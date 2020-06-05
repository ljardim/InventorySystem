using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "Resource", menuName = "LJardim/Inventory/Items/Resource", order = 0)]
    public class Resource : HoldableItem {
        private void Awake() {
            type = ItemType.Resource;
        }
    }
}