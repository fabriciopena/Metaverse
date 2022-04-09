using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    public Rigidbody rb;
    private float timer = 0.0f;
    private int timerType = 0;
    public GameObject player;
    public Material[] materials;
    public Renderer characterRenderer;
    // Start is called before the first frame update
    void Start()
    {
        characterRenderer = player.GetComponent<Renderer>();

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
        //Timer data for types of blocks

        if (GameObject.Find("move_controller").GetComponent<move_character>().positionUpdate == true)
        {//teleport
            Vector3 movement = GameObject.Find("move_controller").GetComponent<move_character>().moveVector;
            transform.position = movement;
            GameObject.Find("move_controller").GetComponent<move_character>().positionUpdate = false;
        }
        if (GameObject.Find("move_controller").GetComponent<move_character>().velocityUpdate == true)
        {//smooth move
            Vector3 velocity = GameObject.Find("move_controller").GetComponent<move_character>().velocityVec;
            rb.velocity = velocity;
            timer = GameObject.Find("move_controller").GetComponent<move_character>().timer;
            timerType = 1;
            GameObject.Find("move_controller").GetComponent<move_character>().velocityUpdate = false;
        }
        if (GameObject.Find("move_controller").GetComponent<move_character>().materialUpdate == true)
        {
            int materialIndex = GameObject.Find("move_controller").GetComponent<move_character>().materialIndex;
            if (materialIndex >= materials.Length)
            {
                materialIndex = 0;
            }
            characterRenderer.material = materials[materialIndex];

        }
    }
   
}
