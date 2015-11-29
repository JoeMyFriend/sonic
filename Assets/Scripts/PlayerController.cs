using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//TESTANDO O GIT

	// Animaçao
	public Animator  anime;
	
	
	// Personagem
	public Rigidbody2D playerRigidbody;
	
	// ANDAR
	public float  speed;
	public float maxSpeed;
	public float scaleX;
	public float movimento_horizontal;
	public bool   walk;
	public bool faceDireita = true;
	public bool   stopping;
	public float  stoppingTemp;
	private float  tempTimeStopping;

	// PULO
	public int   forceJump;
	
	// VERIFICA O CHAO
	public Transform groundCheck;
	public bool   grounded;
	public LayerMask whatIsGround;
	
	
	// ROLAMENTO
	public float  rollTemp;
	public bool   roll;
	private float  tempTimeRoll;
	
	// COLISOR
	public Transform colisor;
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	/*
	if (Input.GetAxisRaw("Walk") > 0) {
		Debug.Log("ANDANDO PARA DIREITA");
   		playerRigidbody.transform.Translate (Vector2.right * speed * Time.deltaTime);
			walk = true
  }

	if (Input.GetAxisRaw("Walk") < 0) {
		Debug.Log("ANDANDO PARA ESQUERDA");
		playerRigidbody.transform.Translate (-Vector2.right * speed * Time.deltaTime);
			walk = true;
	
	}
  */

		/*
		// ANDAR
		if(((Input.GetAxisRaw("Walk") > 0) && grounded)) {
			Debug.Log("Andando para direita");
			playerRigidbody.AddForce(new Vector2(speed * Time.deltaTime, 0));
			playerRigidbody.transform.eulerAngles = new Vector2(0, 0);
		
			walk = true;
			tempTimeStopping = 0;
		}
		
		
		if(((Input.GetAxisRaw("Walk") < 0) && grounded)) {
			Debug.Log("Andando para esquerda");
			playerRigidbody.AddForce(-new Vector2(speed * Time.deltaTime, 0));
			playerRigidbody.transform.eulerAngles = new Vector2(0, 180);
		
			walk = true;
			tempTimeStopping = 0;
		}
		
		
		// PARAR DE ANDAR (ANIMACAO)
		
		if(Input.GetButtonUp("Walk")) {
			Debug.Log("PARANDOOOOOOOOOOO");
			stopping = true;
			
		}
		if(stopping == true) {
			tempTimeStopping += Time.deltaTime;
			if(tempTimeStopping >= stoppingTemp) {
				stopping = false;
				walk = false;
			}
		}
		*/



		movimento_horizontal = Input.GetAxis("Walk");

		if (movimento_horizontal != 0) {
			walk = true;
			anime.SetBool ("walk", walk);
		} else {
			walk = false;
			anime.SetBool("walk", walk);
		}
		if (movimento_horizontal > 0 && !faceDireita) {
			Flip();
		} else if (movimento_horizontal < 0 && faceDireita) {
			Flip();
		}


		if(Input.GetButtonUp("Walk")) {
			Debug.Log("PARANDOOOOOOOOOOO");
			stopping = true;
			anime.SetBool ("stopping", stopping);
			
		}
		if(stopping == true) {
			Debug.Log("Entrou nessa parte do stopping = true");
			tempTimeStopping += Time.deltaTime;
			if(tempTimeStopping >= stoppingTemp) {
				stopping = false;
				walk = false;

				anime.SetBool("stopping", stopping);
				anime.SetBool("walk", walk);
			}
		}

		playerRigidbody.velocity = new Vector2 (movimento_horizontal * maxSpeed, playerRigidbody.velocity.y);


  

		// PULAR
		if(Input.GetButtonDown("Jump") && grounded) {
			playerRigidbody.AddForce(new Vector2(speed * Time.deltaTime, forceJump));
			if(roll) {
				colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.6f, colisor.position.z);
				roll = false;
			}
			
		}
		
		// ROLAR
		if(Input.GetButtonDown("Roll") && grounded == true && roll == false) {
			colisor.position = new Vector3(colisor.position.x, colisor.position.y - 0.6f, colisor.position.z);
			roll = true;
			tempTimeRoll = 0;
		}
		
		grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, whatIsGround);
		
		if(roll) {
			tempTimeRoll += Time.deltaTime;
			if(tempTimeRoll >= rollTemp) {
				colisor.position = new Vector3(colisor.position.x, colisor.position.y + 0.6f, colisor.position.z);
				roll = false;
			}
		}

		//anime.SetBool ("walk", walk);
		//anime.SetBool ("stopping", stopping);
		anime.SetBool("jump", !grounded);
		anime.SetBool ("roll", roll);

		
	}

	void Flip() {
		faceDireita = !faceDireita;
		scaleX = transform.localScale.x;
		//Vector3 theScale = transform.localScale;
		scaleX *= -1;
		transform.localScale = new Vector3 (scaleX, 1, 1);

	}

	void OnTriggerEnter2D() {
		Debug.Log ("BATEU");
		Application.LoadLevel("chamada1");
	}
}
