using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openMenu : MonoBehaviour
{
    private Color colores;
    // Start is called before the first frame update
    void Start()
    {
        colores = GetComponent<Renderer>().material.color;

        
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
      

    }
    void OnMouseDown()
    {
        // this object was clicked - do something
        Destroy(this.gameObject);
        Debug.Log("click!");
    }
    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = new Color(0.182f, 0.083f, 0.201f, 0.8f);
       
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = colores;

    }
}
