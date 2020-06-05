using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "Consumable", menuName = "LJardim/Inventory/Items/Consumable", order = 0)]
    public class Consumable : HoldableItem {
        public float restoreThirstValue;
        public float restoreHungerValue;
        public float restoreHealthValue;
        private void Awake() {
            type = ItemType.Consumable;
        }
    }
}