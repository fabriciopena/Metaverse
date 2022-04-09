using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_character : MonoBehaviour
{
    public bool positionUpdate = false;
    public bool velocityUpdate = false;
    public bool materialUpdate = false;
    public int materialIndex = 0;
    public float timer = 0f;
    public Vector3 moveVector = new Vector3(0, 0, 0);
    public Vector3 velocityVec = new Vector3(0, 0, 0);
        // Start is called before the first frame update
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
