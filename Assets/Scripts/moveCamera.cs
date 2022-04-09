using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour {
    [SerializeField]
    Camera mainCamera;

    [SerializeField]
    float movementSpeed = 5f;

    [SerializeField, Range(1,10)]
    float sensitivity = 5.0f;

    [SerializeField]
    bool freeCameraMovement = false;
    // Free Camera movement involves not using right click to rotate the camera around. Warning: It is a little glitchy due to measures in place to prevent continuous camera movement.

    Transform cameraPosition;
    Vector3 mousePos;
    Vector3 screenCenter;
    Vector2 previousMouseDistance;
    float mouseDifferenceThreshold = 0.2f;

    void Start() {
        cameraPosition = mainCamera.transform;
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;
    }


    void Update() {
        float xPositionChange = 0f;
        float zPositionChange = 0f;
        float positionMultiplier = Time.deltaTime * movementSpeed * 50f;
        // Change in forward and right position is small. Multiplier helps with moving the camera accurately. Delta time ensures that the camera movement is smooth.
        
        if (Input.GetKey(KeyCode.W)) {
            xPositionChange = cameraPosition.forward.x;
            zPositionChange = cameraPosition.forward.z;
            // Forward movement
        }
        if (Input.GetKey(KeyCode.S)) {
            xPositionChange = -cameraPosition.forward.x;
            zPositionChange = -cameraPosition.forward.z;
            // Backward movement
        }
        if (Input.GetKey(KeyCode.D)) {
            xPositionChange = cameraPosition.right.x;
            zPositionChange = cameraPosition.right.z;
            // Right movement
        }
        if (Input.GetKey(KeyCode.A)) {
            xPositionChange = -cameraPosition.right.x;
            zPositionChange = -cameraPosition.right.z;
            // Left movement
        }
        // Changes position using the Vector3's Transform.right and Transform.forward of the Main Camera. These vectors' values allow for relative movement forward, backwards, right and left since the values change based on the camera's rotation.
        
        cameraPosition.position = new Vector3(cameraPosition.position.x + (xPositionChange * positionMultiplier), 1, cameraPosition.position.z + (zPositionChange * positionMultiplier));
        // Updating position with new X and Z positions. Y-axis never changes because that is the elevation of the camera.


        if (!freeCameraMovement) {
            // Click and Drag Camera Movement by right clicking.
            if (Input.GetMouseButtonDown(1)){
                // Fires when the user right clicks
                mousePos = Input.mousePosition;
                // Grabs current mouse position on the play scene and sets it as an initial point of comparison
            }
            if (Input.GetMouseButton(1)) {
            // Fires when the user continuously holds down right click
            
                if (mousePos.x != Input.mousePosition.x || mousePos.y != Input.mousePosition.y) {
                    // Updates camera rotation when there are any changes in the x or y direction of the mouse from the initial

                    Vector3 existingRotation = cameraPosition.localRotation.eulerAngles;
                    Quaternion newRotation = Quaternion.identity;
                    existingRotation.y += (Input.mousePosition.x - mousePos.x) / sensitivity;
                    existingRotation.x -= (Input.mousePosition.y - mousePos.y) / sensitivity;
                    existingRotation.z = 0;
                    // Changes rotation with Euler Angles -- Using a Vector3 to rotate the camera in x, y, and z axis. Z-axis not used due to some weird behavior and only needing to rotate in x  and y direction.

                    newRotation.eulerAngles = existingRotation;
                    cameraPosition.localRotation = newRotation;
                    // Converts Vector3 into a Quaternion since actual camera rotation uses Quaternion, not a Vector3

                    mousePos = Input.mousePosition;
                    // Resets the initial mouse position to prevent continuous camera rotation
                }
            }
            return;
        }



        mousePos = Input.mousePosition;
        float mouseDistanceFromCenterX = screenCenter.x - mousePos.x;
        float mouseDistanceFromCenterY = screenCenter.y - mousePos.y;
        if (Mathf.Abs(mouseDistanceFromCenterX - previousMouseDistance.x) > mouseDifferenceThreshold || Mathf.Abs(mouseDistanceFromCenterY - previousMouseDistance.y) > mouseDifferenceThreshold) {
            // Updates camera rotation when there are any changes in the difference between the screen center and mouse position beyond the preset threshold
            
            Vector3 existingRotation = cameraPosition.localRotation.eulerAngles;
            Quaternion newRotation = Quaternion.identity;
            existingRotation.y -= mouseDistanceFromCenterX / (200f / sensitivity);
            existingRotation.x += mouseDistanceFromCenterY / (200f / sensitivity);
            existingRotation.z = 0;
            // Changes rotation with Euler Angles -- Using a Vector3 to rotate the camera in x, y,and z axis. Z-axis not used due to some weird behavior and only needing to rotate in x and y direction.
            
            previousMouseDistance = new Vector2(mouseDistanceFromCenterX, mouseDistanceFromCenterY);
            // Saves previous mouse distance to prevent continuous camera rotation and act as a point of comparison
            newRotation.eulerAngles = existingRotation;
            cameraPosition.localRotation = newRotation;
            // Converts Vector3 into a Quaternion since actual camera rotation uses Quaternion, not a Vector3
        }
    }
}
