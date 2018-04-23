using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
	private Rigidbody rb;
	private Material trail;
	private GameObject lastHitBy = null;
	public float speed = 10f;
	float timeSinceHit;
	private Vector3 movement;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		trail = GetComponent<TrailRenderer> ().material;
		timeSinceHit = 0;
		float moveIntialX = Random.Range (-0.9f, 0.9f);
		float moveIntialZ = Random.Range (-1.0f, 1.0f);

		movement = new Vector3(moveIntialX, 0, moveIntialZ);
		rb.velocity = movement.normalized * speed;
	}

	void FixedUpdate()
	{
		if(Time.time - timeSinceHit > 6)
		{
			rb.velocity = rb.velocity * 1.0005f;
		}
	}
	void OnTriggerEnter (Collider c) {
		timeSinceHit = 0;
		if(c.gameObject.tag == "Goal")
		{
			ScoreKeeping ();
			for (int i=1; i < 9; i++) {
				if (c.gameObject.name == "GoalP"+i)  {
					Destroy (gameObject);
				}
			}
		} else if (c.gameObject.tag == "GravField")
		{
			rb.velocity = rb.velocity * 0.1f;
		}
	}

	void OnCollisionEnter (Collision c) {
		if (c.gameObject.tag == "Player") {
			lastHitBy = GameObject.Find (c.gameObject.name);
			trail.SetColor ("_TintColor", lastHitBy.GetComponent<Renderer> ().sharedMaterial.GetColor("_Color"));
			GetComponent<Light>().color = lastHitBy.GetComponent<Renderer> ().sharedMaterial.GetColor("_Color");
			GetComponent<ParticleSystem>().startColor = lastHitBy.GetComponent<Renderer> ().sharedMaterial.GetColor("_Color");
			GetComponent<ParticleSystem>().Play();
			timeSinceHit = Time.time;
		}

	}

	void ScoreKeeping() {
		if (lastHitBy != null) {
			PlayerMovement scoringPlayer = (PlayerMovement) lastHitBy.GetComponent(typeof(PlayerMovement));
				scoringPlayer.setScore (scoringPlayer.getScore () + 1);
				Debug.Log(scoringPlayer.gameObject.name + " scored!");
		} 
	}

	GameObject getLastHitBy() {
		return lastHitBy;
	}

}
