using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public Rigidbody rb;
    private float timer = 0.0f;
    private int timerType = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0.0f)
        {
            if (timerType == 1)
            {
                rb.velocity = new Vector3(0, 0, 0);
                timerType = 0;
            }
        }
        if (GameObject.Find("move_controller").GetComponent<move_character>().teleport == true)
        {//teleport
            Vector3 movement = GameObject.Find("move_controller").GetComponent<move_character>().moveVector;
            transform.position = movement;
            GameObject.Find("move_controller").GetComponent<move_character>().teleport = false;
        }
        if (GameObject.Find("move_controller").GetComponent<move_character>().moving == true)
        {//smooth move
            Vector3 velocity = GameObject.Find("move_controller").GetComponent<move_character>().velocityVec;
            rb.velocity = velocity;
            timer = GameObject.Find("move_controller").GetComponent<move_character>().timer;
            timerType = 1;
            GameObject.Find("move_controller").GetComponent<move_character>().moving = false;
        }
    }
   
}
