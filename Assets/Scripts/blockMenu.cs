using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMenu : MonoBehaviour {
    int minBlockListIndex = 1;
    int maxBlockListIndex = 4;
    // NOTE: Only change these two indexes after creating new Block Menus

    int counter = 0;

    [SerializeField]
    public Camera targetCamera;

    [SerializeField]
    public float grabDistance;

    public void updateBlocksVisibility(string blockListName, bool blockStatus) {
        GameObject blockList = GameObject.Find(blockListName);
        
        for (int childCounter = 0; childCounter < blockList.transform.childCount; childCounter ++) {
            GameObject blockInstance = blockList.transform.GetChild(childCounter).gameObject;
            blockInstance.SetActive(blockStatus);
        }
        // Grabs the parent block list Game Object to iterate through each child and sets the visibility of each block instance
    }

    public void changeBlockMenu(int chosenMenuIndex) {
        for (int blockIndex = minBlockListIndex; blockIndex <= maxBlockListIndex; blockIndex ++) {
            if (blockIndex != chosenMenuIndex) {
                string blockListInstanceName = "Block List " + blockIndex;
                updateBlocksVisibility(blockListInstanceName, false);
            }
        }
        // Hides the other block lists based on the parent Game Object combined with the iterated index
        
        updateBlocksVisibility("Block List " + chosenMenuIndex, true);
        // Excluded the passed menu index to be shown, not hidden like the other block menus
    }

    void Start() { 
        changeBlockMenu(minBlockListIndex);
    }

    void Update() {
        var ray = targetCamera.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, grabDistance) && Input.GetMouseButtonDown(0)) {
            if (hit.transform.name.Trim() == "Scroll Up") {
                // Handles scrolling up the block menu
                if (counter < maxBlockListIndex) {
                    counter ++;
                }
                changeBlockMenu(counter);

            } else if (hit.transform.name.Trim() == "Scroll Down") {
                // Handles scrolling down the block menu
                if (counter > minBlockListIndex) {
                    counter --;
                }
                changeBlockMenu(counter);
            }
        }
    }                
}
