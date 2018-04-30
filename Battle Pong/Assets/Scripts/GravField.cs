using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravField : MonoBehaviour {

	float duration;
	void Start () {
		GetComponent<Rigidbody>().position += transform.forward * 5;
		Object.Destroy(gameObject, 5f);
	}

	void FixedUpdate () {
	}
}
