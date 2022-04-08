using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_block : MonoBehaviour {
    // Start is called before the first frame update
    public Vector3 move = new Vector3(0f, 0f, 0f);
    void Start()
    {
      
    }   

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown() // temporarily will be using a click but control method will be found in the future
    {
        // this object was clicked - do something
        GameObject.Find("move_controller").GetComponent<move_character>().teleport = true;
        Vector3 movement_vector = GameObject.Find("move_controller").GetComponent<move_character>().moveVector = move;
    }
}
