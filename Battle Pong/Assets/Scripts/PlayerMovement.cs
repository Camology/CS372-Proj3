using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	public float speed;
	private Rigidbody rb;
	private int score;
	public Text scoreText;
	public bool usingController;
	bool canMove;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		score = 0;
		setScore (0);
		canMove = true;
		speed = 55;
		//set these here just in case someone forgot to do something in the GUI
		rb.drag = 10;
		rb.mass = 500;
		rb.isKinematic = false;
	}

	void Update() {
	}

	// Update is called once per frame
	void FixedUpdate () {
		movement ();
	}

	void movement() {
		float horizontal;
		if (usingController)
			horizontal = Input.GetAxis (this.gameObject.name + "Strafe-Controller");
		else
			horizontal = Input.GetAxis (this.gameObject.name + "Strafe");
		if (Mathf.Abs(horizontal) > 0.1 && canMove)
			rb.position += (this.transform.right * horizontal * speed * Time.deltaTime);

		if(Input.GetButtonDown(this.gameObject.name + "Push") && canMove)
		{
			canMove = false;
			StartCoroutine(push());
		}
	}



	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Wall") {
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.tag == "Wall") {
		}
	}

	IEnumerator push()
	{
		int force  = 30000;
		rb.velocity = Vector3.zero;
		Vector3 initialPos = rb.position;
		Vector3 newPos = transform.forward;
		
		rb.AddForce(newPos.x * force, 0 , newPos.z * force, ForceMode.Impulse);
		yield return new WaitForSeconds(0.3f);
		force += 1000;
		rb.AddForce(newPos.x * force *-1 , 0, newPos.z * force * -1 , ForceMode.Impulse);
		yield return new WaitForSeconds(0.3f);
		rb.velocity = Vector3.zero;
		rb.position = initialPos;
		canMove = true;
	}

	public void setScore(int val) {
		if (val < 0)
			val = 0;
		score = val;
		scoreText.text = "Score : " + score.ToString ();
	}

	public int getScore() {
		return score;
	}
}
