using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "ItemDragData", menuName = "LJardim/Inventory/ItemDragData", order = 0)]
    public class ItemDragData : ScriptableObject {
        public GameObject sourceGameObject;
        public ItemUi sourceItemUi;
        public GameObject targetGameObject;
        public ItemUi targetItemUi;

        public void Clear() {
            sourceGameObject = null;
            sourceItemUi = null;
            targetGameObject = null;
            targetItemUi = null;
        }
    }
}