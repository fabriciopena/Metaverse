using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{
  public GameObject character;

  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.name == "Main Character")
      {
          //debug.Log("Transform Main Character");
          character.transform.Translate(0, 0, 1);
      }

  }
/*
    //public gameObject maincharacter;
    void Start()
    {

    }

    void Update()
    {

        //collision.transform.Translate(0.05f, 0f,0f);
    }
*/
}
