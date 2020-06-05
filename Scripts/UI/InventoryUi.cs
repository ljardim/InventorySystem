using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LJardim.InventorySystem {
    public class InventoryUi : MonoBehaviour {
        [SerializeField]private InventoryObject inventory;
        [SerializeField]private GameObject slotPrefab;
        [SerializeField]private Transform slotParent;
        [SerializeField]private ItemDragData itemDragData;
        
        private readonly List<ItemUi> _slots = new List<ItemUi>();
        
        private void Start() {
            RefreshSlots();
        }
        
        public void RefreshSlots() {
            foreach (var slot in _slots) {
                slot.ClearSlot();
                Destroy(slot.gameObject);
            }
            _slots.Clear();

            for (var i = 0; i < inventory.items.Length; i++) {
                var newSlotObject = Instantiate(slotPrefab, slotParent);
                
                var newSlot = newSlotObject.GetComponent<ItemUi>();
                newSlot.Inventory = inventory;
                newSlot.ItemIndex = i;
                newSlot.Item = inventory.items[i];
                newSlot.RefreshSlot();
                
                AddEvent(newSlotObject, EventTriggerType.PointerEnter, delegate { OnEnter(newSlotObject); });
                AddEvent(newSlotObject, EventTriggerType.PointerExit, delegate { OnExit(newSlotObject); });
                AddEvent(newSlotObject, EventTriggerType.BeginDrag, delegate { OnDragStart(newSlotObject); });
                AddEvent(newSlotObject, EventTriggerType.EndDrag, delegate { OnDragEnd(newSlotObject); });
                AddEvent(newSlotObject, EventTriggerType.Drag, delegate { OnDrag(newSlotObject); });
                
                _slots.Add(newSlot);
            }
        }
        
        private static void SwapItems(ItemUi sourceItem, ItemUi targetItem) {
            // Cache target item
            var tempItem = targetItem.Item;

            // Replace target item with source
            targetItem.Inventory.items[targetItem.ItemIndex] = sourceItem.Item;
            targetItem.Item = sourceItem.Item;
            
            // Replace source with temp
            sourceItem.Inventory.items[sourceItem.ItemIndex] = tempItem;
            sourceItem.Item = tempItem;
            
            sourceItem.RefreshSlot();
            targetItem.RefreshSlot();
        }
        
        private static void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action) {
            var eventTrigger = new EventTrigger.Entry {eventID = type};
            eventTrigger.callback.AddListener(action);
            
            var trigger = obj.GetComponent<EventTrigger>();
            trigger.triggers.Add(eventTrigger);
        }

        private void OnEnter(GameObject obj) {
            var itemUi = obj.GetComponent<ItemUi>();
            if (itemUi == null) {
                return;
            }

            itemDragData.targetGameObject = obj;
            itemDragData.targetItemUi = itemUi;
        }
        
        private void OnExit(GameObject obj) {
            itemDragData.targetGameObject = null;
            itemDragData.targetItemUi = null;
        }
        
        private void OnDragStart(GameObject obj) {
            var itemUi = obj.GetComponent<ItemUi>();
            if (itemUi.Item.template == null) {
                return;
            }
            
            itemDragData.Clear();
            
            var selectedObject = new GameObject();
            var rectTransform = selectedObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(70, 70);
            selectedObject.transform.SetParent(GameObject.Find("Canvas").transform);
            
            var img = selectedObject.AddComponent<Image>();
            img.sprite = itemUi.Item.template.uiIcon;
            img.raycastTarget = false;

            itemDragData.sourceGameObject = selectedObject;
            itemDragData.sourceItemUi = itemUi;
        }
        
        private void OnDragEnd(GameObject obj) {
            if (itemDragData.targetGameObject) {
                SwapItems(itemDragData.sourceItemUi, itemDragData.targetItemUi);
            } else {
                // TODO drop item
            }
            Destroy(itemDragData.sourceGameObject);
            itemDragData.sourceItemUi = null;
        }
        
        private void OnDrag(GameObject obj) {
            if (itemDragData.sourceGameObject != null) {
                itemDragData.sourceGameObject.GetComponent<RectTransform>().position = Input.mousePosition;
            }
        }
    }
}