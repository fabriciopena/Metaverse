using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMenu : MonoBehaviour {
    // Start is called before the first frame update
    List<string> blockLists = new List<string>() {"Block List 1", "Block List 2"};
    string visibleBlockList = "Block List 1";

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
        // Grabs the parent block list Game Object to iterate through each child and sets the visibility of the block instances in the blockInstances1
    }

    public void hideAllBlocks(string exemptedBlockList) {
        foreach (string blockList in blockLists) {
            if (exemptedBlockList != blockList) {
                updateBlocksVisibility(blockList, false);
            }
            // Exclude the starting initial block list from being hidden
        }
        // Initializes the other block lists as hidden based on the parent Game Object names in the blockLists List
    }

    void Start() { 
        hideAllBlocks(visibleBlockList);
    }

    void Update() {
        var ray = targetCamera.ViewportPointToRay(Vector3.one * 0.5f);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, grabDistance) && Input.GetMouseButtonDown(0)) {
            if (hit.transform.name.Trim() == "Scroll Up") {
                // Handles scrolling up the block menu
                updateBlocksVisibility("Block List 1", true);
                hideAllBlocks("Block List 1");

            } else if (hit.transform.name.Trim() == "Scroll Down") {
                // Handles scrolling down the block menu
                updateBlocksVisibility("Block List 2", true);
                hideAllBlocks("Block List 2");

            }
        }
    }                
}
