using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    public GameObject block;
    Vector3 truePos;
    public float gridSize;
    private float distance;


    void OnMouseDown(){
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 rayPoint = ray.GetPoint(distance);
        transform.position = rayPoint;
    }
    void OnMouseUp(){}
    void LateUpdate()
      {
//      target.transform.position = new Vector3(block.transform.position.x, block.transform.position.y, block.transform.position.z);

      truePos.x = -3;
      truePos.y = Mathf.Floor(block.transform.position.y/gridSize)*gridSize;
      truePos.z = Mathf.Floor(block.transform.position.z/gridSize)*gridSize;

      block.transform.position = truePos;
    }

}
