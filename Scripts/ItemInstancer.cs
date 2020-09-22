using System.Collections;
using UnityEngine;

namespace LJardim.InventorySystem {
    public class ItemInstancer : MonoBehaviour {
        [SerializeField]private Item item;
        [SerializeField]private LayerMask layerToAssign;

        public Item Item => item;

        private void Start() {
            var obj = Instantiate(item.template.physicalRepresentation, transform.position, Quaternion.identity, transform);
            obj.AddComponent<Rigidbody>();
            obj.layer = (int) Mathf.Log(layerToAssign.value, 2);
            obj.name = item.template.name;

            var objBounds = obj.GetComponent<Renderer>().bounds;
            
            var colliderInstance = obj.AddComponent<BoxCollider>();
            colliderInstance.center = objBounds.center - obj.transform.position;
            colliderInstance.size = objBounds.size;

            obj.transform.localEulerAngles = new Vector3(90, 0, 0);

            StartCoroutine(RemoveRigidBody(obj));
        }

        private static IEnumerator RemoveRigidBody(GameObject instantiated) {
            yield return new WaitForSeconds(3);
            Destroy(instantiated.GetComponent<Rigidbody>());

            instantiated.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}