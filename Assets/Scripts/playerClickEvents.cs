using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClickEvents : MonoBehaviour {
    [SerializeField]
    public Camera mainCamera;
    
    [SerializeField]
    public float grabDistance = 3f;

    grabObjects GrabObjects;
    blockMenu BlockMenu;
    blockConnection BlockConnection;
    public Transform detectedObject;

    void Start() {
        GrabObjects = mainCamera.GetComponent<grabObjects>();    
        BlockMenu = mainCamera.GetComponent<blockMenu>();
        BlockConnection = mainCamera.GetComponent<blockConnection>();
    }

    void FixedUpdate() {
        var ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hitRay;
        // Uses rays from center of the screen to handle click detections 

        if (Physics.Raycast(ray, out hitRay, grabDistance)) {
            detectedObject = hitRay.transform;

            if (detectedObject.name.Trim() == "Scroll Up" && Input.GetMouseButtonDown(0)) {
                // Event for scrolling up the block menu on click
                if (BlockMenu.blockSelectorIndex < BlockMenu.maxBlockListIndex) {
                    BlockMenu.blockSelectorIndex ++;
                }
                BlockMenu.changeBlockMenu(BlockMenu.blockSelectorIndex);

            } else if (detectedObject.name.Trim() == "Scroll Down" && Input.GetMouseButtonDown(0)) {
                // Event for scrolling down the block menu on click
                if (BlockMenu.blockSelectorIndex > BlockMenu.minBlockListIndex) {
                    BlockMenu.blockSelectorIndex --;
                }
                BlockMenu.changeBlockMenu(BlockMenu.blockSelectorIndex);

            } else if (GrabObjects.pickedItem && detectedObject.GetChild(0).name.Trim() == "Start Block" && detectedObject.name != "Pickupable Object") {
                if (Input.GetKeyDown(KeyCode.P)) {
                    BlockConnection.connectBlocks(detectedObject, GrabObjects.pickedItem.transform);
                    GrabObjects.resetBlockGrab();
                }
                // Temp for now since mouse clicked doesn't work

            } else if (!GrabObjects.pickedItem) {
                // Event for picking up blocks and enabling block hover
                GrabObjects.grabObjectsEvent(detectedObject);
            } 
        } 
    }
}
