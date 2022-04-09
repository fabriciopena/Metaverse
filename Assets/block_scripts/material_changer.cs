using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_changer : MonoBehaviour
{
    public int materialIndex = 0;
    // Start is called before the first frame update
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
        GameObject.Find("move_controller").GetComponent<move_character>().materialUpdate = true;
       GameObject.Find("move_controller").GetComponent<move_character>().materialIndex = materialIndex;
        

    }
}
