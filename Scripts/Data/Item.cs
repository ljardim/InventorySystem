using System;

namespace LJardim.InventorySystem {
    [Serializable]
    public class Item {
        public ItemObject template;
        public int amount;
        public float durability;

        public float MaxDurability {
            get {
                if (!(durability > -1)) {
                    return -1;
                }

                var tool = (Tool) template;
                return tool.maxDurability;
            }
        }

        // Default constructor required for new items instantiated from inspector
        public Item() {
            template = null;
            amount = 0;
            durability = -1;
        }

        public Item(ItemObject template, int amount, float durability) {
            this.template = template;
            this.amount = template.IsStackable ? amount : 1;
            this.durability = durability;
        }

        public void AddAmount(int value) {
            amount += value;
        }

        public void PerformAction() {
            
        }
        
        public static bool Compare(Item itemA, Item itemB) {
            return itemA.template == itemB.template && itemA.durability == itemB.durability;
        }
        
        public static void Swap(Item itemA, Item itemB) {
            var tempTemplate = itemA.template;
            var tempAmount = itemA.amount;
            var tempDurability = itemA.durability;

            itemA.template = itemB.template;
            itemA.amount = itemB.amount;
            itemA.durability = itemB.durability;

            itemB.template = tempTemplate;
            itemB.amount = tempAmount;
            itemB.durability = tempDurability;
        }

        public void Clear() {
            template = null;
            amount = 0;
            durability = -1;
        }
    }
}