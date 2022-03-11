using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//On Collision the gameobject "character" will be transported to the scene you specify.
//Note: To add one more portal you have to duplicate a portal and remove the script in that portal. Then duplicate this script and change the text "Level 1" or what ever is in 8:186 (8th row and 186th character) to the scene you need.
public class portal_1 : MonoBehaviour{public GameObject character; void OnCollisionEnter(Collision collision){if (collision.gameObject.name == "Main Character"){SceneManager.LoadScene("Level 1");}}}
