using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMover : MonoBehaviour
{
    void Start()
    {
    }


    void goToXYZ(float x, float y, float z){
      transform.position = new Vector3(x, y, z);
    }

    void GlideXAxis(float xunits){
      transform.position = transform.position + new Vector3(xunits, 0, 0);
    }

    void GlideYAxis(float yunits){
      transform.position = transform.position + new Vector3(0, yunits, 0);
    }

    void GlideZAxis(float zunits){
      transform.position = transform.position + new Vector3(0, 0, zunits);
    }


}
