using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickableObject : MonoBehaviour {
    // Code Sample taken from https://www.patrykgalach.com/2020/03/16/pick-up-items-in-unity/
    
    public Rigidbody objectRigidBody;

    private void Awake() {
        objectRigidBody = GetComponent<Rigidbody>();
    }
}
