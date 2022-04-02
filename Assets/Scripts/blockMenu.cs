using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMenu : MonoBehaviour {
    public int minBlockListIndex = 1;
    public int maxBlockListIndex = 4;
    // NOTE: Only change these two indexes after creating new Block Menus

    public int blockSelectorIndex = 0;

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
}
