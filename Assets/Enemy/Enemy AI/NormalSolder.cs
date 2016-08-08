using UnityEngine;
using System.Collections;

public class NormalSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	UnHaveLifeBullet unHavelifeBullet;
	//public int statenumber = 2;
	public int weaponAttackCyclic;

	public Animator monsterAni;


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
		if(life != 0){
			if (Input.GetKeyDown (KeyCode.A)) {
				Debug.Log ("hi");
				Pattern (monsterNormalState.Run);
				sR.flipX =true;
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
	
		//idle.SetTrigger ("idle1");
	
		//		if (playerRealizeDistance == 10)
		//			Attack ();
	}
	
	
	public void  Attack(){
		
			if (attacktype == 0) {
			
			}
	
			if (attacktype == 1) {
			Instantiate (unHavelifeBullet.bullet, this.transform.position, transform.rotation);
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
