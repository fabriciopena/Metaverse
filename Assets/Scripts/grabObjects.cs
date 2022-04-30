using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObjects : MonoBehaviour {
    // Crosshairs: https://forum.unity.com/threads/free-mega-crosshairs-pack.1071167/    
    [SerializeField]
    private Transform grabbedItemSlot;

    public PickableObject pickedItem;

    Vector3 rightHandPosition = new Vector3(0.02f, 0.2f, -0.5f);
    Vector3 leftHandPosition = new Vector3(-3.1f, 0.2f, -0.5f);
    Vector3 dropOffPosition = new Vector3(-1.5f, 0.5f, -0.5f);

    Color previousItemColor = Color.clear;
    MeshRenderer highlightedItem = new MeshRenderer();
    string highlightName = "";
    string highlightLayerName = "";
    bool highlightOn = false;
    Color highlightColor = Color.clear;

    public void resetBlockGrab() {
        pickedItem = null;
        previousItemColor = highlightColor;
    }
    
    public void objectHover(Transform hoveredObject) {
        highlightedItem = hoveredObject.GetChild(0).GetComponent<MeshRenderer>();
        // Mesh Renderer used to set the highlight color of the targeted object in range
            
        if (previousItemColor.r == 0 || (highlightName == hoveredObject.GetChild(0).name && hoveredObject.name.Trim() == highlightLayerName) && !highlightOn) {
            previousItemColor = highlightedItem.material.color;
            highlightOn = true;
        }
        // Stores the previous color once to prevent accidentally setting it to the highlight color
        // Checks if the object being hovered overed on is the same object that previously had a highlight based on the actual object name and outer layer enabling pickup.

        highlightedItem.material.SetColor("_Color", highlightColor);
        highlightName = hoveredObject.GetChild(0).name;
        highlightLayerName = hoveredObject.name.Trim();
        // Stores name of the object and its outer layer for highlight color comparison
    }

    public void grabObjectsEvent(Transform hitObject) {
        // Event used for block grabbing
        var pickable = hitObject.GetComponent<PickableObject>();
        if (!pickable) return;

        objectHover(hitObject);
        if (Input.GetMouseButtonDown(0) && !pickedItem) {
            // If the hit object has a Pickable object from parent GameObject, pick the item up
                
            highlightedItem.material.SetColor("_Color", previousItemColor);
            highlightedItem = new MeshRenderer();
            // Remove highlight color and highlight item instance

            if (hitObject.name.Trim() == "Pickupable Object") {
                Transform grabbedBlock = hitObject;
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
    }

    void FixedUpdate() {
        if (pickedItem && Input.GetMouseButtonDown(0)) {
            // Drops an item when an instance of the picked item has already been initialized
            DropItem(pickedItem);
        }
        else if (highlightedItem != null && highlightOn){
            highlightedItem.material.SetColor("_Color", previousItemColor);
            highlightedItem = new MeshRenderer();
            highlightOn = false;
            previousItemColor = highlightColor;
            // Clears highlighted color and previous color instance when not hovering over a block
        }
                    
        if (Input.GetKeyDown(KeyCode.T) && pickedItem) {    
            Destroy(pickedItem.gameObject);
            resetBlockGrab();
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
        
        previousItemColor = Color.clear;
        // Removes instance of previous color

        item.transform.SetParent(grabbedItemSlot);
        // if (item.transform.childCount > 1) {
        //     // Schema of Placed blocks:
        //     // Parent (Pickupable Block)
        //         // Start Block Sphere
        //         // Parent (Pickupable Block)
        //             // Block 1
        //         // Parent (Pickupable Block)
        //             // Block 3

        //     Debug.Log("Do Stuff here");
        //     // TODO: Change behavior to scale the child blocks accordingly when picking up a group of blocks
        // } else {
        //     item.transform.SetParent(grabbedItemSlot);
        // }
        
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
        
        if (item.transform.childCount > 1) {
            Debug.Log("Do Stuff Later");
            // for (int childCounter = 0; childCounter < item.transform.childCount; childCounter ++) {
            //     
            //     Transform blockInstance = item.transform.GetChild(childCounter);
            //     blockInstance.localPosition = dropOffPosition;
            // }
            // Error: Doesn't work yet for some odd reason
        } else {
            item.transform.GetChild(0).localPosition = dropOffPosition;
        }
        // Sets the picked up object to the center of the camera once dropped         

        if (item.objectRigidBody.useGravity) {
            item.objectRigidBody.AddForce(item.transform.forward * 0.5f, ForceMode.VelocityChange);
            // Provides the dropping "animation" by applying a small force when releasing the object
        }
    }
}
