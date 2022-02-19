using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject cam1;
    public GameObject cam3;
    public GameObject domeCam;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1stPersonCamera")){
          cam1.SetActive(true);
          cam3.SetActive(false);
          domeCam.SetActive(false);
        }

        if (Input.GetButtonDown("3rdPersonCamera")){
          cam1.SetActive(false);
          cam3.SetActive(true);
          domeCam.SetActive(false);
        }

        if (Input.GetButtonDown("DomeCamera")){
          cam1.SetActive(false);
          cam3.SetActive(false);
          domeCam.SetActive(true);
        }
        
    }
}
