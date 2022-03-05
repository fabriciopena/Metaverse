using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObjects : MonoBehaviour {
    // Crosshairs: https://forum.unity.com/threads/free-mega-crosshairs-pack.1071167/
    [SerializeField]
    private Camera mainCamera;
    
    [SerializeField]
    private Transform grabbedItemSlot;

    [SerializeField]
    private float grabDistance = 4f;

    private PickableObject pickedItem;

    Vector3 rightHandPosition = new Vector3(0.02f, 0.2f, -0.5f);
    Vector3 leftHandPosition = new Vector3(-3.1f, 0.2f, -0.5f);
    Vector3 dropOffPosition = new Vector3(-1.5f, 0.5f, -0.5f);

    Color previousItemColor = Color.clear;
    MeshRenderer highlightedItem = new MeshRenderer();
    Color highlightColor = Color.clear;

    void Update() {
        var ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        // Uses rays from center of the screen to pick up the block

        if (pickedItem && Input.GetMouseButtonDown(0)) {
            // Drops an item when an instance of the picked item has already been initialized
            DropItem(pickedItem);
        }
        else if (Physics.Raycast(ray, out hit, grabDistance)) {
            // Shot ray to find object to pick
            highlightedItem = hit.transform.GetChild(0).GetComponent<MeshRenderer>();
            // Mesh Renderer used to set the highlight color of the targeted object in range

            if (previousItemColor.r == 0) {
                previousItemColor = highlightedItem.material.color;
            }
            highlightedItem.material.SetColor("_Color", highlightColor);
            // Stores the previous color once to prevent accidentally setting it to the highlight color

            var pickable = hit.transform.GetComponent<PickableObject>();
            if (Input.GetMouseButtonDown(0) && pickable) {
                // If the hit object has a Pickable object component (inherited from parent GameObject), pick the item up
                
                highlightedItem.material.SetColor("_Color", previousItemColor);
                highlightedItem = new MeshRenderer();
                // Remove highlight color and highlight item instance

                if (hit.transform.name.Trim() == "Pickupable Object") {
                    Transform grabbedBlock = hit.transform;
                    grabbedBlock.GetChild(0).GetComponent<MeshRenderer>().material.SetColor("_Color", previousItemColor);
                    // Removes the highlight color again since removing it only once outside of this if statement doesn't work

                    var newBlockInstance = Instantiate(grabbedBlock);
                    PickItem(newBlockInstance.GetComponent<PickableObject>());
                    // Create a new instance of that item when grabbing a block from the "menu"
                } else {
                    PickItem(pickable);
                    // Don't create a new instance of the block and simply pick up the block.
                }
            }
            // BUGS: Some picked up instances still have their highlighted color
        } 
        else if (highlightedItem != null){
            highlightedItem.material.SetColor("_Color", previousItemColor);
            highlightedItem = new MeshRenderer();
            // BUG: Hovering over both of the blocks from the menu sets the blocks to the same color
            // One solution is to use previousItemColor = Color.clear; but using that causes some instances of the picked item to remain its highlight color
            
            // Clears highlighted color and previous color instance when not hovering over a block
        }
                    
        if (Input.GetKeyDown(KeyCode.T) && pickedItem) {    
            Destroy(pickedItem.gameObject);
            pickedItem = null;
            // Trashes the picked item by deleting it from the scene by pressing the T key.
        }
    }


    // Code below initially taken from https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/ but altered to fit use case
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
        
        pickedObject.GetComponent<MeshRenderer>().material.SetColor("_Color", previousItemColor);
                    
        previousItemColor = Color.clear;
        // Removes instance of previous color
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
