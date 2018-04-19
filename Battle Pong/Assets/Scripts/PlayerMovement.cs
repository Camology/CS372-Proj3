using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Bounds
{
	public float xMin, xMax;
}

public class PlayerMovement : MonoBehaviour {
	public float speed = 0.5f;
	public GameObject boundingLine;
	public Bounds bounds;
	private Rigidbody rb;
	public float zVal;
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

		//set these here just in case someone forgot to do something in the GUI
		rb.drag = 10;
		rb.mass = 500;
		rb.isKinematic = false;
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

	IEnumerator push()
	{
		int force = 1500;
		Vector3 initialPos = rb.position;
		rb.AddForce(rb.position.x * -force, 0 , rb.position.z * -force, ForceMode.Impulse);
		yield return new WaitForSeconds(0.3f);
		force += 500;
		rb.AddForce(rb.position.x * force, 0, rb.position.z * force , ForceMode.Impulse);
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
