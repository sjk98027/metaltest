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

	float parabolaX;
	float parabolaY;
	float starty;
	float gravity = 0.1f;
	float gravityaccel = 0.002f;
	float parabolaVerticalAccel;

	int parabolastate;

	float testtime;


	//get stateinfo , bulletname
	AnimatorStateInfo bulletstate;

	public enum BulletName{
		normalbullet ,
		parabolabullet,
		guidancebullet

	};




	public BulletName bulletName;

	void Start(){
		testtime = Time.realtimeSinceStartup;


		CreateBullet createBullet = gameObject.GetComponentInParent<CreateBullet> ();

		sR = gameObject.GetComponent<SpriteRenderer>();
		string name = gameObject.layer.ToString ();

		//to player search position
		player = GameObject.Find ("Player");
		playposition = player.transform.position;

		//this object transform.position.y ;
		parabolaY = this.transform.position.y;
		starty = this.transform.position.y;
		parabolastate = 1;
		parabolaX = this.transform.position.x;
		parabolaVerticalAccel = (playposition.x - parabolaX)/100;
	}

	void Update(){
		RunningPattern (bulletName);
		//Debug.Log (parabolaVerticalAccel);
//		Debug.Log (player.transform.position);
//		Debug.Log (playposition);

		//bullet destroy;
		Destroy (this.gameObject, 10);

	}

	public void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("player")) {
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
				
				//if()



				break;		
			}

		case BulletName.parabolabullet:
			{
				pos = new Vector2 (this.gameObject.transform.position.x, this.gameObject.transform.position.y);
				pos.y = parabolaY;
				pos.x = parabolaX;
				gameObject.transform.position = pos;


				parabolaBulletmove ();


//				this.gameObject.transform.Translate (player.transform.position, 0);
				//this.gameObject.transform.Translate (player.transform.position,0);
				//this.gameObject.transform.Translate (1*Time.deltaTime*10, 0, 0);
				break;
			}

		}
	}

	void parabolaBulletmove ()
	{parabolaX += parabolaVerticalAccel;
		switch (parabolastate) 
		{
		case 1: 
			{
				gravity -= gravityaccel;
				parabolaY += gravity;

//				if (parabolaY >= starty+100) {
//					parabolastate = 2;
//				}
				break;
			}

		case 2: 
			{
				
				parabolaY -= gravity;
				gravity += gravityaccel;
				break;
			}
		

		}
	}

	void OnTriggerEnter(Collider coll){
		Debug.Log(testtime);
	}







}
