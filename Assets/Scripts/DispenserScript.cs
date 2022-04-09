using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DispenserScript : MonoBehaviour
{
    public GameObject block;
    [SerializeField]
    Vector3 DropPoint = new Vector3(4, 1, -23);

    void Update()
    {
       if (Physics.CheckSphere (DropPoint, 0)) {}
       else {
         Instantiate(block, DropPoint, Quaternion.identity);
       }
    }
}
