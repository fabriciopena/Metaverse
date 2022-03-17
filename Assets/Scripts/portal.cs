using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//On Collision the gameobject "character" will be transported to the scene you specify.
public class portal : MonoBehaviour{public GameObject character; [SerializeField] private string levelToEnter; void OnCollisionEnter(Collision collision){if (collision.gameObject.name == "Main Character"){SceneManager.LoadScene(levelToEnter);}}}
