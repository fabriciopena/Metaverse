using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockMenu : MonoBehaviour {
    // Start is called before the first frame update
    string[] blockNames1 = {"Block 1", "Block 2", "Block 3", "Block 4", "Block 5", "Block 6", "Block 7", "Block 8", "Block 9", "Block 10", "Block 11", "Block 12"};
    string[] blockNames2 = {"Block A1", "Block A2", "Block A3", "Block A4", "Block A5", "Block A6", "Block A7", "Block A8", "Block A9", "Block A10", "Block A11", "Block A12"};
     
    List<GameObject> blockInstances1 = new List<GameObject>();
    List<GameObject> blockInstances2 = new List<GameObject>();

    [SerializeField]
    public Camera targetCamera;

    [SerializeField]
    public float grabDistance;

    void Start() {
        foreach (string block in blockNames1) {
            blockInstances1.Add(GameObject.Find(block));
        }

        foreach (string block in blockNames2) {
            GameObject blockInstance = GameObject.Find(block);
            blockInstances2.Add(blockInstance);
            blockInstance.SetActive(false);
            // Initializes and hides the second block list
        }
        // Adds the block instances from the names list to the instances list to be used later.

    }

    public void updateVisabilityBlockList1(bool blockStatus) {
        foreach (GameObject blockInstance in blockInstances1) {
            blockInstance.SetActive(blockStatus);
        }
        // Loops over the instances and sets the visibility of the block instances in the blockInstances1
    }

    public void updateVisabilityBlockList2(bool blockStatus) {
        foreach (GameObject blockInstance in blockInstances2) {
            blockInstance.SetActive(blockStatus);
        }
        // Loops over the instances and sets the visibility of the block instances in the blockInstances2
    }

    void Update() {
        var ray = targetCamera.ViewportPointToRay(Vector3.one * 0.5f);
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, grabDistance) && Input.GetMouseButtonDown(0)) {
            if (hit.transform.name.Trim() == "Scroll Up") {
                // Handles scrolling up the block menu
                updateVisabilityBlockList1(true);
                updateVisabilityBlockList2(false);

            } else if (hit.transform.name.Trim() == "Scroll Down") {
                // Handles scrolling down the block menu
                updateVisabilityBlockList1(false);
                updateVisabilityBlockList2(true);

            }
        }
    }                
}
