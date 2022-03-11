using UnityEngine;

public class CubeMovement : MonoBehaviour {

	public float speed = 25f;
	public Rigidbody rb;
	private void Start(){
		rb = GetComponent<Rigidbody>();
	}
	void Update () {
		float horizontalMovement = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
		float verticalMovement = Input.GetAxis("Vertical") * Time.deltaTime * speed;
		transform.Translate(horizontalMovement, 0, verticalMovement);
		
		//Space bar 4 jump
		if(Input.GetButtonDown("Jump")){rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);}
	}
}
