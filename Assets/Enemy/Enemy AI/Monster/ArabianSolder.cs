using UnityEngine;
using System.Collections;

public class ArabianSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	UnHaveLifeBullet unHavelifeBullet;
	//public int statenumber = 2;
	public int weaponAttackCyclic;
	public GameObject arabianSolder;
	public GameObject user;
	public Animator monsterAni;



	public enum monsterNormalState{
		Idle = 1,
		Run,
		Attack,
		Death,
	}

	// Use this for initialization
	void Start () {
		user = GameObject.FindGameObjectWithTag ("player");
		//player = gameObject.GetComponent<PlayerPrefs> ();
		life = 1;
		MonsterSetting setting = gameObject.GetComponentInParent<MonsterSetting> ();
		//test
		unHavelifeBullet = gameObject.GetComponent<UnHaveLifeBullet> ();

		monsterAni = gameObject.GetComponent<Animator> ();
		Pattern (monsterNormalState.Idle);
		//Instantiate (bullet);
		attacktype = 0;
		sR = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		
		//test
		if (life != 0) {
			
			TurnMonster ();




			if (Input.GetKeyDown (KeyCode.A)) {
				//playerRealizeDistance = float (Vector3(user.transform.position)-Vector3(arabianSolder.transform.position)).distance;
				Debug.Log ("hi");
				Pattern (monsterNormalState.Run);
				sR.flipX =true;
				//sr.flipX == true;

			}
			if(Input.GetKeyUp(KeyCode.A))
			{
				sR.flipX = false;
			}

			if (life <= 0) {
				//Pattern (monsterNormalState.Death);
			}

			if (playerRealizeDistance >= 1000) {
				Pattern (monsterNormalState.Death);

			}
		}
		if (life == 0) {
			Pattern (monsterNormalState.Death);
		}
	}

	public void TurnMonster()
	{
		//such player distance and rotate;
		Vector2 distanceVector2 = new Vector2 (arabianSolder.transform.position.x - user.transform.position.x, arabianSolder.transform.position.y - user.transform.position.y);
		//if turntoplayer == +, ->;  else turntoplayer == - ,<-;

		turnToPlayer = distanceVector2.x;
		playerRealizeDistance		= distanceVector2.magnitude;

		if (distanceVector2.x >= 0) {
			sR.flipX = false;
		}
		else if (distanceVector2.x<0) {
			sR.flipX = true;
		}
	}


	public void Idle(){

		//idle.SetTrigger ("idle1");

		//		if (playerRealizeDistance == 10)
		//			Attack ();
	}


	public void  Attack(){

		if (attacktype == 0) {

		}

		if (attacktype == 1) {
			Instantiate (unHavelifeBullet.bullet, this.transform.position, transform.rotation);
			//Vector2 bulletVector2 = new Vector2 (-1, 0);
			//Instantiate (UnHaveLifeBullet, this.transform.position, this.transform.rotation);
		} 

		else
			Idle ();
	}

	public void Run()
	{
		this.gameObject.transform.Translate(4*Time.deltaTime, 0, 0);
		monsterAni.SetTrigger ("Run");
	}

	public void Death(){
		Destroy (this.gameObject, 5);

	}


	public void Pattern(monsterNormalState state){
		switch(state){
		case monsterNormalState.Idle:
			{
				Idle ();
				break;
			}

		case monsterNormalState.Run:
			{
				Run ();
				break;
			}

		case monsterNormalState.Death:
			{
				Death ();
				break;
			}
		}
	}


	public void OncollisionEnter(Collider coll){		
		if (coll.gameObject.layer == LayerMask.NameToLayer ("playerweapon")) {
			life -= 1;
		}
	}
}
