using UnityEngine;
using System.Collections;

public class UnHaveLifeBullet : CreateBullet {
	//get gameobject target and ani and bulletobject
	public Animator bulletAni;

//	public gameObject.Transform.ImagePosition
	public GameObject player;
	public GameObject monster;
	public GameObject bullet;


	Vector2 pos;
	public Vector2 playposition;
	public Rigidbody2D rdb;


	float starty;
	float startx;

	float xSpeed;
	float gravity = 5f;
	float ySpeed = 5f;





	//get stateinfo , bulletname
	AnimatorStateInfo bulletstate;

	public enum BulletName{
		normalbullet ,
		parabolabullet,
		guidancebullet

	};




	public BulletName bulletName;

	void Start(){
		//testtime = Time.realtimeSinceStartup;


		CreateBullet createBullet = gameObject.GetComponentInParent<CreateBullet> ();

		sR = gameObject.GetComponent<SpriteRenderer>();
		string name = gameObject.layer.ToString ();

		//to player search position
		player = GameObject.Find ("Player");
		playposition = player.transform.position;

		//this object transform.position.y ;
		starty = this.transform.position.y;
		startx = this.transform.position.y;
	
		xSpeed = playposition.x - startx;

	}

	void Update(){
		RunningPattern (bulletName);
	
		Destroy (this.gameObject, 10);
		if (this.gameObject.transform.position.y <= 0) {
			Debug.Log (Time.realtimeSinceStartup);
		}


	}

	public void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("player")) {
			//sR.sprite
			Destroy (this.gameObject);
			//damage calculate;

			coll.gameObject.SendMessage ("Death");

			if (coll.gameObject.layer == LayerMask.NameToLayer ("slug")) {
				//coll.gameObject.SendMessage
			}
		}

		if (coll.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			Destroy (this.gameObject);
		}


	}

	public void RunningPattern(BulletName name)
	{
		switch (name) {
		case BulletName.normalbullet: 
			{
				this.gameObject.transform.Translate (1*Time.deltaTime*10, 0, 0);
				break;
			}

		case BulletName.guidancebullet: 
			{
				



				break;		
			}

		case BulletName.parabolabullet:
			{
				ySpeed -= gravity * Time.deltaTime;
				this.gameObject.transform.Translate (new Vector3 (xSpeed*Time.deltaTime*0.333f, -ySpeed*Time.deltaTime, 0));
				break;
			}

		}
	}






	void OnTriggerEnter(Collider coll){
		//Debug.Log(testtime);
	}







}
