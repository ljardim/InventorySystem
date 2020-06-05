using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "Component", menuName = "LJardim/Inventory/Items/Component", order = 0)]
    public class Component : HoldableItem {
        private void Awake() {
            type = ItemType.Component;
        }
    }
}