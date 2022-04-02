using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClickEvents : MonoBehaviour {
    // Update is called once per frame
    [SerializeField]
    public Camera mainCamera;
    
    [SerializeField]
    public float grabDistance = 3f;

    grabObjects GrabObjects;
    blockMenu BlockMenu;

    void Start() {
        GrabObjects = mainCamera.GetComponent<grabObjects>();    
        BlockMenu = mainCamera.GetComponent<blockMenu>();
    }

    void Update() {
        var ray = mainCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hitRay;
        // Uses rays from center of the screen to handle click detections 

        if (Physics.Raycast(ray, out hitRay, grabDistance)) {
            Transform detectedObject = hitRay.transform;

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

            } else if (!GrabObjects.pickedItem) {
                // Event for picking up blocks and enabling block hover
                GrabObjects.grabObjectsEvent(detectedObject);

            }


        } 
    }
}
