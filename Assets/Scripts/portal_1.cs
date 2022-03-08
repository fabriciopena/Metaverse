using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_1 : MonoBehaviour
{
  public GameObject character;

  void OnCollisionEnter(Collision collision)
  {
      if (collision.gameObject.name == "Main Character")
      {
        SceneManager.LoadScene("Level 1");
      }

  }
}
