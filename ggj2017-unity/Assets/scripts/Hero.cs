using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour {

	public BoxCollider2D upperCollider;
	public BoxCollider2D bottomCollider;
	public Rigidbody2D rigitBody;
	public Rigidbody2D boat;
	public Rigidbody2D wall;

	bool isGrounded;
	public bool isBoatGrounded;
	bool doubleJumpReady;

	public float maxDelay;
	public float maxDelyForBent;

	bool bentingDown;

	public float jumpVelocity;
	public float jumpWithBoatVelocity;

    public int maxLives = 3;
    private int lives = 0;
    private List<GameObject> hearts = new List<GameObject>();

	AudioSource jumpAudio;
	AudioSource jumpWithBoatAudio;


	Animator animator;
    GameObject heartTemplate;

    // Use this for initialization
    void Start () {

		animator = GetComponent<Animator> ();
		jumpAudio = GameObject.Find ("jump1").GetComponent<AudioSource> ();
		jumpWithBoatAudio = GameObject.Find ("jumpWithBoat2").GetComponent<AudioSource> ();

        heartTemplate = GameObject.Find("Heart");
        int i = 0;
        while (i++ < maxLives) AddLife();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			Invoke ("bentDown", maxDelyForBent);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			
			CancelInvoke ("bentDown");
			if (bentingDown) {
				bentingDown = false;
				bentUp ();
			}
			else {
				if (isGrounded && isBoatGrounded) {
					animator.SetTrigger ("jump");
				}
				if (doubleJumpReady) {
					CancelInvoke ("jump");
					doubleJump ();
				} else {
					Invoke ("jump", maxDelay);
					doubleJumpReady = true;
				}
			}
		}
	}

	void FixedUpdate()
	{
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.name == "boat") {
			isGrounded = true;
		}
        if ((collision.gameObject.name == "FlyingCup(Clone)" ||
            collision.gameObject.name == "FlyingBra(Clone)") &&
            collision.gameObject.tag != "Finish")
        {
            collision.gameObject.tag = "Finish";
            RemoveLife();
        }
	}

    public void AddLife()
    {
        if (lives < maxLives)
        {
            lives++;
            var heart = Instantiate(heartTemplate);
            heart.transform.position = new Vector3(5.0f - 0.7f * (maxLives - lives), 4.0f);
            hearts.Add(heart);
        }
    }

    public void RemoveLife()
    {
        lives--;
        Destroy(hearts[lives]);
        hearts.RemoveAt(lives);
        if (lives <= 0)
        {
            gameOver();
        }
    }

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "dieElements") {
			// gameOver ();
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.name == "boat") {
			isGrounded = false;
		}
	}

	void bentDown()
	{

		animator.SetBool ("isBend", true);
		bentingDown = true;
		upperCollider.enabled = false;
	}

	void bentUp()
	{

		animator.SetBool ("isBend", false);
		upperCollider.enabled = true;

	}

	void jump()
	{
		doubleJumpReady = false;
		if (!isGrounded || !isBoatGrounded) {
			return;
		}
//		Vector2 velocity = new Vector2 (0, jumpVelocity);
//		rigitBody.velocity = velocity;
		Vector2 force = new Vector2(0, jumpVelocity);
		rigitBody.AddForce (force);

		jumpAudio.Play ();

	}

	void doubleJump()
	{
		doubleJumpReady = false;
		if (!isGrounded || !isBoatGrounded) {
			return;
		}

		Vector2 force = new Vector2 (0, jumpWithBoatVelocity);
		rigitBody.velocity = force;
		boat.velocity = force;

		jumpWithBoatAudio.Play ();
		
	}

	public void gameOver()
	{
		print ("gameOver");
        SceneManager.LoadScene("menu");
	}
}
