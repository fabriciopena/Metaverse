using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal : MonoBehaviour
{
  public GameObject character;

  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.name == "Main Character")
      {
        SceneManager.LoadScene("SampleScene");
      }

  }
}
