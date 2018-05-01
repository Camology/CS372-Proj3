using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {
    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip[] audioClip;
    private Material trail;
    private GameObject lastHitBy = null;
    public float speed = 10f;
    float timeSinceHit;
    private Vector3 movement;

    void Start () {
        rb = GetComponent<Rigidbody> ();
        audioSource = GetComponent<AudioSource> ();
        trail = GetComponent<TrailRenderer> ().material;

        timeSinceHit = 0;

        movement = (GameInfo.playerCount == 2) ?
            get2PlayerInitalVelocity () :
            getMulitPlayerInitVelocity ();

        rb.velocity = movement.normalized * speed;
    }

    Vector3 get2PlayerInitalVelocity() {
        float maxAngle = 0.8f;
        float minAngle = 0.2f;

        float sign = (getRandomPick()) ? 1.0f : -1.0f;
        float moveIntialX = sign * Random.Range (minAngle, maxAngle);

        float moveIntialZ = (getRandomPick()) ? 1.0f : -1.0f;

        return new Vector3(moveIntialX, 0, moveIntialZ);
    }

    bool getRandomPick() {
        return (Random.Range (0.0f, 1.0f) > 0.5f);
    }

    Vector3 getMulitPlayerInitVelocity() {
        float moveIntialX = Random.Range (-0.9f, 0.9f);
        float moveIntialZ = Random.Range (-1.0f, 1.0f);

        return new Vector3(moveIntialX, 0, moveIntialZ);
    }

    void Update() {
        if(Time.time - timeSinceHit > 7) {
            rb.velocity = rb.velocity * 1.001f;
        }
    }

    void OnTriggerEnter (Collider c) {
        timeSinceHit = 0;

        if(c.gameObject.tag == "Goal") {
            onBallInGoal(c.gameObject.name);
        } else if (c.gameObject.tag == "GravField"){
            onBallEnterGravField();
        }
    }

    void onBallInGoal(string playerName) {
        if (didPlayerScore (playerName)) {
            ScoreKeeping ();
        }

        Destroy (gameObject);
    }


    void onBallEnterGravField() {
        movement = rb.velocity;
        rb.velocity *=  0.2f;
    }

    bool didPlayerScore(string playerName) {
        return
            lastHitBy != null &&
            playerName != GameInfo.goalMap [lastHitBy.name];
    }

    void ScoreKeeping() {
        PlayerMovement scoringPlayer = (PlayerMovement) lastHitBy.GetComponent(typeof(PlayerMovement));
        scoringPlayer.setScore (scoringPlayer.getScore () + 1);

        if (scoringPlayer.getScore () <= scoringPlayer.getWinPoints () - 1) {
            scoringPlayer.playSound (0, 1);
        } else if (scoringPlayer.getScore () == scoringPlayer.getWinPoints ()) {
            scoringPlayer.playSound (1, 1);
        }

        Debug.Log(scoringPlayer.gameObject.name + " scored!");
        lastHitBy = null;
    }

    void OnTriggerExit(Collider c) {
        if(c.gameObject.tag != "GravField") {
            return;
        }

        rb.velocity = movement;
    }


    void OnCollisionEnter (Collision c) {
        if (c.gameObject.tag != "Player") {
            return;
        }

        playSound (0);
        lastHitBy = GameObject.Find (c.gameObject.name);

        Color playerColor = lastHitBy.GetComponent<Renderer> ().sharedMaterial.GetColor ("_Color");

        setBallsColor(playerColor);

        timeSinceHit = Time.time;
        movement = rb.velocity;
    }

    void setBallsColor(Color playerColor) {
        trail.SetColor ("_TintColor", playerColor);

        GetComponent<Light>().color = playerColor;
        GetComponent<ParticleSystem>().startColor = playerColor;
        GetComponent<ParticleSystem>().Play();
    }


    void playSound(int clip) {
        audioSource.clip = audioClip [clip];
        audioSource.Play ();
    }


    GameObject getLastHitBy() {
        return lastHitBy;
    }
}
