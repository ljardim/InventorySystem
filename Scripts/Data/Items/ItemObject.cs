using UnityEngine;

namespace LJardim.InventorySystem {
    public enum ItemType {
        Tool,
        Consumable,
        Resource,
        Component
    }

    public abstract class ItemObject : ScriptableObject {
        public int id;
        public ItemType type;
        [TextArea(15, 20)]
        public string description;
        public string tooltipMessage;

        public int maxStack;
        public float weight;

        public GameObject physicalRepresentation;
        public Sprite uiIcon;

        public bool IsStackable => maxStack > 1;
    }
}