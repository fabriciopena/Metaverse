using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float rotSpeedX = 25f;
    public float rotSpeedY = 25f;
    public float rotDamp = 25f;

    public float mY = 0f;
    public float mX = 0f;

    public float walkSpeed = 25f;
    public float runSpeed = 50f;

    public float currentSpeed;

    [SerializeField]
    public KeyCode runKey;

    public CharacterController cc;
    public Rigidbody rb;

    [SerializeField]
    public GameObject playerCamera;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
    }

    public void LateUpdate()
    {
        if(Input.GetButtonDown("Jump")){rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);}

        mX += Input.GetAxis("Mouse X") * rotSpeedX * (Time.deltaTime * rotDamp);
        mY += -Input.GetAxis("Mouse Y") * rotSpeedY * (Time.deltaTime * rotDamp);

        mY = Mathf.Clamp(mY, -80, 80);

        playerCamera.transform.localEulerAngles = new Vector3(mY, 0f, 0f);

        transform.eulerAngles = new Vector3(0f, mX, 0f);

        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        currentSpeed = walkSpeed;

        if (Input.GetKey(runKey) && Input.GetKey(KeyCode.W)) currentSpeed = runSpeed;


        Vector3 moveDir = (transform.right * hor) + (transform.forward * ver);

        cc.Move(moveDir * currentSpeed * Time.deltaTime);
    }
}
