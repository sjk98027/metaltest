using UnityEngine;
using System.Collections;

public class NormalSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	public int weaponAttackCyclic;
	public GameObject bulletPrefeb;
	public Animator monsterAni;
	public Transform[] firePosition;
	//statenumber =  0 idle, 1 scare, 2 attack;
	public int statenumber = 2;




	public enum monsterNormalState{
		Idle = 1,
		Run,
		Attack,
		Death,
	}

	// Use this for initialization
	void Start () {
		life = 1;
		MonsterSetting setting = gameObject.GetComponentInParent<MonsterSetting> ();
		//test


		monsterAni = gameObject.GetComponent<Animator> ();
		Pattern (monsterNormalState.Idle);

		attacktype = 0;
		sR = gameObject.GetComponent<SpriteRenderer> ();
		playerRealizeDistance = 30;
		statenumber = 0;

		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		
		//test
		if(life != 0){
			float playertomonsterdistance =Vector3.Distance(this.transform.position,player.gameObject.transform.position);

			if(statenumber == 0){
				Pattern (monsterNormalState.Idle);

				if (playertomonsterdistance < playerRealizeDistance) {
					Pattern (monsterNormalState.Attack);
				}
			}
			if (Input.GetKeyDown (KeyCode.A)) {
				Debug.Log ("hi");
				Pattern (monsterNormalState.Attack);
				//Pattern (monsterNormalState.Run);
				//sR.flipX =true;
				//sr.flipX == true;

			}
//			if(Input.GetKeyUp(KeyCode.A))
//			{
//				sR.flipX = false;
//			}
			

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

	

	
	public void Idle(){
		monsterAni.SetTrigger ("idle");
//		idle.SetTrigger ("idle1");
	
		//		if (playerRealizeDistance == 10)
		//			Attack ();
	}
	
	
	public void  Attack(){

		monsterAni.SetTrigger("sitdowmfireinthehole");
			if (attacktype == 0) {
			if (sR.flipX == true) {
				GameObject bullet = Instantiate (bulletPrefeb, firePosition [1].position, Quaternion.identity) as GameObject;
				bullet.transform.rotation = new Quaternion (0, 0, 180, 0);	} 
				else {	GameObject bullet = Instantiate (bulletPrefeb, firePosition [0].position, Quaternion.identity) as GameObject;	}
		}
	
			if (attacktype == 1) {
//			Instantiate (unHavelifeBullet.bullet, this.transform.position, transform.rotation);
		//	bullet 
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
//
//	public void runningPattern(BulletName name)
//	{
//		switch (name) {
//		case BulletName.normalbullet: 
//			{
			//Instantiate (bullet);
//
//
//				break;
//			}
//
//		case BulletName.guidancebullet: 
//			{
//				//if()
//
//
//
//				break;		
//			}
//
//		case BulletName.parabolabullet:
//			{
//				break;
//			}
//
//		}
//	}

	
	
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

		case monsterNormalState.Attack:
			{
				Attack ();
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


