using UnityEngine;

namespace LJardim.InventorySystem {
    public class Inventory : MonoBehaviour {
        [SerializeField]protected InventoryObject inventory;

        public virtual void AddItem(Item item, out int amountTaken) {
            inventory.AddItem(item, out amountTaken);
        }
    }
}