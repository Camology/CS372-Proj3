
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private AudioSource audioSource;
    private int score;
    private int winPoints = 2;
    public Text scoreText;
    public AbilityCooldowns abilityBar;
	Light light;
    public bool usingController;
    public string inputIdentifier;
    bool canMove;
    public GameObject gravField;
    private Vector3 initialPos;
    public AudioClip[] audioClip;


    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        abilityBar = scoreText.GetComponentInChildren<AbilityCooldowns>();
        inputIdentifier = GameInfo.inputMap[gameObject.name];
        audioSource = GetComponent<AudioSource>();

        initialPos = rb.position;
        score = 0;
        setScore(0);
        canMove = true;
        speed = 55;
        rb.drag = 10;
        rb.mass = 500;
        rb.isKinematic = false;
        scoreText.resizeTextMaxSize = 1;

        //set ability cooldowns
        abilityBar.skills[0].cooldown = 5; //push cooldown
        abilityBar.skills[1].cooldown = 20; //gravity cooldown

		//configure light settings
		light = GetComponent<Light>();
		light.color = GetComponent<Renderer> ().sharedMaterial.GetColor("_Color");
		light.intensity = 0.8f;
		light.range = 15;

    }

    void Update()
    {
		if (score == winPoints)
			WinnerMode ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement();
    }

    void movement()
    {
        float horizontal;

        horizontal = Input.GetAxis(inputIdentifier + "Strafe");
        if (Mathf.Abs(horizontal) > 0.1 && canMove)
            rb.position += (this.transform.right * horizontal * speed * Time.deltaTime);

        if (Input.GetButtonDown(inputIdentifier + "Push") && canMove && abilityBar.AbilityReady(0))
        {
            abilityBar.StartCooldown(0);
            canMove = false;
            StartCoroutine(push());
        }
        if (Input.GetButtonDown(inputIdentifier + "Grav") && abilityBar.AbilityReady(1))
        {
            abilityBar.StartCooldown(1); //start ability cooldown
			audioSource.pitch = 0.8f;
            playSound(3, 0.03f);
			//create the grav field
            Instantiate(gravField, initialPos, transform.rotation);
        }
    }

    public void playSound(int clip, float volume)
    {
		audioSource.volume = volume;
        audioSource.clip = audioClip[clip];
        audioSource.Play();

    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Goal")
            rb.position += (this.transform.forward * 1);
    }

    IEnumerator push()
    {
        //play sound effect but make it quieter
		audioSource.pitch = 0.8f;
        playSound(2, 0.03f);

        //get initial position so we can reset after
        int force = 15000;
        rb.velocity = Vector3.zero;
        Vector3 initialPos = rb.position;
        Vector3 newPos = transform.forward;

        //move the thing and change the light
        GetComponent<Light>().intensity = 1.5f;
        GetComponent<Light>().range = 20;
        rb.AddForce(newPos.x * force, 0, newPos.z * force, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        GetComponent<Light>().intensity = 1f;
        force += 1000;
        rb.AddForce(newPos.x * force * -1, 0, newPos.z * force * -1, ForceMode.Impulse);
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
        rb.position = initialPos;
        canMove = true;
        GetComponent<Light>().intensity = 0.8f;
        GetComponent<Light>().range = 15;
    }

    public void setScore(int val) {
        score = val;
        scoreText.text = "Score : " + score.ToString();
    }

    public int getScore() {
        return score;
    }

    public int getWinPoints() {
        return winPoints;
    }

    public void WinnerMode() {
        Color winColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        RectTransform textRect = scoreText.GetComponent<RectTransform>();
        GameObject floor = GameObject.Find("platform");
        GetComponent<Renderer>().material.color = winColor;
        //floor.GetComponent<Renderer>().material.color = winColor;
        scoreText.color = winColor;
        scoreText.text = this.gameObject.name + " is winner !!!";;
        //textRect.anchoredPosition = new Vector3 (0,0,0);
        textRect.anchoredPosition = rb.position;
        scoreText.fontSize = 100;
        Time.timeScale = 0f;
    }
}
