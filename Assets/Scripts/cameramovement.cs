using UnityEngine;
using System.Collections;

public class cameramovement : MonoBehaviour {

    public GameObject follow;
    float mainSpeed = 5.0f;
    float maxShift = 1000.0f;
    float camSens = 0.9f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    private float totalRun= 1.0f;

    void Update () {
        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;

        transform.position = new Vector3(GameObject.Find("Main Character").transform.position.x, GameObject.Find("Main Character").transform.position.y, GameObject.Find("Main Character").transform.position.z);

        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0){
          if (Input.GetKey (KeyCode.LeftShift)){
              totalRun += Time.deltaTime;
              p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
              p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
              p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
          } else {
             totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
             p = p * mainSpeed;
          }
        }
    }

    private Vector3 GetBaseInput() {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.W)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.A)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
