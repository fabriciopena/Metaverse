using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObjects : MonoBehaviour {
    // Code initially taken from https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/ but altered to fit use case
    // Crosshairs: https://forum.unity.com/threads/free-mega-crosshairs-pack.1071167/
    [SerializeField]
    private Camera mainCamera;
    
    [SerializeField]
    private Transform grabbedItemSlot;

    [SerializeField]
    private float grabDistance = 2f;

    private PickableObject pickedItem;

    Vector3 rightHandPosition = new Vector3(0.02f, 0.2f, -0.5f);
    Vector3 leftHandPosition = new Vector3(-3.1f, 0.2f, -0.5f);
    Vector3 dropOffPosition = new Vector3(-1.5f, 0.5f, -0.5f);

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            // Triggers picking up an item when the user left clicks
            if (pickedItem) {
                // Drops an item when an instance of the picked item has already been initialized
                DropItem(pickedItem);
            }
            else {
                var ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
                RaycastHit hit;
                // Uses rays from center of the screen to pick up the block

                if (Physics.Raycast(ray, out hit, grabDistance)) {
                    // Shot ray to find object to pick
                    
                    var pickable = hit.transform.GetComponent<PickableObject>();
                    if (pickable) {
                        // If the hit object has a Pickable object component (inherited from parent GameObject), pick the item up
                    
                        if (hit.transform.name.Trim() == "Pickupable Object") {
                            var newBlockInstance = Instantiate(hit.transform);
                            PickItem(newBlockInstance.GetComponent<PickableObject>());
                            // Create a new instance of that item when grabbing a block from the "menu"
                        } else {
                            PickItem(pickable);
                            // Don't create a new instance of the block and simply pick up the block.
                        }
                        
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && pickedItem) {    
            Destroy(pickedItem.gameObject);
            pickedItem = null;
            // Trashes the picked item by deleting it from the scene by pressing the T key.
        }


    }

    private void PickItem(PickableObject item) {
        pickedItem = item;
        item.objectRigidBody.isKinematic = true;
        item.objectRigidBody.velocity = Vector3.zero;
        item.objectRigidBody.angularVelocity = Vector3.zero;
        // Disable rigidbody and reset velocities
        
        item.transform.SetParent(grabbedItemSlot);
        // Uses parent slot to "grab" the object and make it a child of the empty GameObject. The GameObject must be a child of the main camera since attaching the picked-up item to the camera's Transform breaks the movement.

        item.transform.localPosition = Vector3.zero;
        item.transform.localEulerAngles = Vector3.zero;
        // Reset grabbed item's position and rotation

        Transform pickedObject = item.transform.GetChild(0);
        // Get child references the actual picked up item, not the PickableObject Game Object (the parent)
        
        pickedObject.localPosition = rightHandPosition;
        pickedObject.localScale = Vector3.one * 0.5f;
        // Sets the location of the picked object to create the illusion of the user holding the object in his hand
    }

    private void DropItem(PickableObject item) {
        pickedItem = null;
        item.transform.SetParent(null);
        // Remove parent

        item.objectRigidBody.isKinematic = false;
        // Enable rigidbody
        
        item.transform.GetChild(0).localPosition = dropOffPosition;
        // Sets the picked up object to the center of the camera once dropped 

        if (item.objectRigidBody.useGravity) {
            item.objectRigidBody.AddForce(item.transform.forward * 0.5f, ForceMode.VelocityChange);
            // Provides the dropping "animation" by applying a small force when releasing the object
        }
    }
}
