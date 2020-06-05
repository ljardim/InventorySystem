using System.Collections.Generic;
using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "LJardim/Inventory/ItemDatabase", order = 0)]
    public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver {
        public ItemObject[] items;
        public Dictionary<ItemObject, int> getId = new Dictionary<ItemObject, int>();
        
        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize() {
            getId = new Dictionary<ItemObject, int>();
            for (var i = 0; i < items.Length; i++) {
                getId.Add(items[i], i);
            }
        }
    }
}