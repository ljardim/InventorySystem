using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace LJardim.InventorySystem {
    [CreateAssetMenu(fileName = "Inventory", menuName = "LJardim/Inventory/Inventory", order = 0)]
    public class InventoryObject : ScriptableObject {
        public ItemDatabaseObject database;
        public Item[] items = new Item[24];
        public int maxItems = 24;
        public IntEvent itemsChangedEvent;
        public void AddItem(Item itemToAdd, out int amountTaken) {
            var amountToAdd = itemToAdd.amount;
            var itemToAddTemplate = itemToAdd.template;
            var itemToAddDurability = itemToAdd.durability;
            
            // TODO Do weight checks
            
            // STEP 1: If its not a stackable item, just try to add it and return
            if (!itemToAddTemplate.IsStackable) {
                // Check for an open spot and add it if there is
                for (var i = 0; i < items.Length; i++) {
                    if (items[i].template != null) {
                        continue;
                    }

                    items[i] = new Item(itemToAddTemplate, amountToAdd, itemToAddDurability);
                    amountTaken = 1;
                    itemsChangedEvent.Raise(amountTaken);
                    return;
                }
                // If we got here, then there is no space
                amountTaken = 0;
                return;
            }

            var amountTakenCounter = 0;
            
            // STEP 2: Item to add is stackable, so next step is to try add it to existing items by increasing amount
            foreach (var thisItem in items) {
                // If this item is empty, continue
                if (thisItem.template == null) {
                    continue;
                }
                
                // If its not the same item OR durability is not the same OR its already full, skip it and check next item
                if (thisItem.template != itemToAddTemplate || thisItem.durability != itemToAddDurability || thisItem.amount >= itemToAddTemplate.maxStack) {
                    continue;
                }
                
                // If this item is a match on type and durability AND it has space, add to its amount as much as we can
                var availableSpace = itemToAddTemplate.maxStack - thisItem.amount;
                
                // If the available space is more or equal to the amount we need to add, we can add everything and return
                if (availableSpace >= amountToAdd) {
                    thisItem.AddAmount(amountToAdd);
                    amountTakenCounter += amountToAdd;
                    amountTaken = amountTakenCounter;
                    itemsChangedEvent.Raise(amountTaken);
                    return;
                }

                // If the amount to add is more than this items available space, then add to available space and subtract from amount still to add
                thisItem.AddAmount(availableSpace);
                amountTakenCounter += availableSpace;
                amountToAdd -= availableSpace;
            }

            // STEP 3: If we got to this point, we filled up all existing items' space and need to create new items
            // So we loop until no more space or until we have added the full amount
            for (var i = 0; i < items.Length; i++) {
                var thisItem = items[i];
                if (thisItem.template != null) {
                    continue;
                }

                if (amountToAdd > itemToAddTemplate.maxStack) {
                    // If amount to add is still greater than maxStack, then add new item with max stack and update variables
                    items[i] = new Item(itemToAddTemplate, itemToAddTemplate.maxStack, itemToAddDurability);
                    amountTakenCounter += itemToAddTemplate.maxStack;
                    amountToAdd -= itemToAddTemplate.maxStack;
                } else {
                    // If not, then add new item with the remaining amount and break
                    items[i] = new Item(itemToAddTemplate, amountToAdd, itemToAddDurability);
                    amountTakenCounter += amountToAdd;
                    break;
                }
            }

            amountTaken = amountTakenCounter;
            itemsChangedEvent.Raise(amountTaken);
        }
    }
}