using UnityEngine;

public class playerClickEvents : MonoBehaviour {
    [SerializeField]
    public Camera mainCamera;
    
    [SerializeField]
    public float grabDistance = 3f;

    grabObjects GrabObjects;
    blockMenu BlockMenu;
    blockConnection BlockConnection;
    blockCodeCompile BlockCodeCompile;
    public Transform detectedObject;

    void Start() {
        GrabObjects = mainCamera.GetComponent<grabObjects>();    
        BlockMenu = mainCamera.GetComponent<blockMenu>();
        BlockConnection = mainCamera.GetComponent<blockConnection>();
        BlockCodeCompile = mainCamera.GetComponent<blockCodeCompile>();

        // Imports all external scripts necessary to run different player click and key press events
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

            } else if (detectedObject.GetChild(0).name.Trim() == "Start Block" && detectedObject.name != "Pickupable Object") {
                // Triggers when hovering over the start block

                // GrabObjects.objectHover(detectedObject);
                // BUG: Currently, resetting the color of the connector block after connection doesn't work
                // Possible solutions tried with the object hover function returning the original color of the block: 
                    // setting the previous color in grabObjects.cs
                    // exporting a previous color setter function and calling in in this file
                    // directly setting the material in this file
                    // Resetting the color in the resetBlockGrab function


                // if (Input.GetMouseButton(0)) {
                //     BlockConnection.connectBlocks(detectedObject, GrabObjects.pickedItem.transform);
                //     GrabObjects.resetBlockGrab();
                // }
                // Works on the first try in the following condition: Placing any other block besides the start block first, then placing the start block when hovering over the first block
                // Takes 2 tries (which is supposed to be the right way): Placing the start block first, then placing any other block when hovering over the start block


                if (Input.GetKeyDown(KeyCode.P) && GrabObjects.pickedItem && GrabObjects.pickedItem.transform.GetChild(0).name.Trim() != "Start Block") {
                    // Must hold a grabbed block that is not the start block and be hovering over the start block
                    BlockConnection.connectBlocks(detectedObject, GrabObjects.pickedItem.transform);
                    GrabObjects.resetBlockGrab();
                }
                // Temp for now since mouse clicked doesn't work

                else if (Input.GetKeyDown(KeyCode.R)) {
                    // Must be hovering over the start block (try various distances if key press doesn't work) to compile the block code
                    BlockCodeCompile.compileBlockCode(detectedObject);
                    // Run the blocks in order of connection
                }

            } else if (!GrabObjects.pickedItem) {
                // Event for picking up blocks and enabling block hover
                GrabObjects.grabObjectsEvent(detectedObject);
            } 

        } 
    }
}
