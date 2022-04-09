using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class UserDrag : MonoBehaviour
{
    public Color mouseOverColor = Color.blue;
    public Color originalColor = Color.yellow;
    public bool dragging = false;


    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = mouseOverColor;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = originalColor;
    }

    void OnMouseDown()
    {
        dragging = true;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    void Update()
    {
        if (dragging)
        {

            Vector3 rayPoint = Camera.main.ScreenPointToRay(Input.mousePosition).GetPoint(Vector3.Distance(transform.position, Camera.main.transform.position));
            transform.position = rayPoint;
        }
    }
}
