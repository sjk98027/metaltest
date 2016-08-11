using UnityEngine;
using System.Collections;

public class NormalSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	public int weaponAttackCyclic;
	public GameObject bulletPrefeb;
	public Animator monsterAni;
	public Transform[] firePosition;

	//cheak list
	//scarecheck = if(monsterrealize) once true -> no return scare;
	public bool scarecheck = false;
	//sprite flip on off
	public bool spincheck = false;

	//attack type distance 
	public int fireIntheHoleDistance=20;
	public int sitDownFireIntheHoleDistance = 10;
	public int jumpKnipeAttackDistance = 5;
	public int knipeAttackDistance = 2;




	public enum monsterNormalState{
		Idle ,
		Scare,
		Run,
		Attack,
		Death,

		FIH,
		SFIH,
		JKA,
		KA,
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

		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		
		//test
		if (life != 0) {
			float playerToMonsterDistance = Vector3.Distance (this.transform.position, player.gameObject.transform.position);

			if (playerToMonsterDistance > playerRealizeDistance) {
				Pattern (monsterNormalState.Idle);
			}
			if (playerToMonsterDistance <= playerRealizeDistance & scarecheck == false) {
				Pattern (monsterNormalState.Scare);
				//scarecheck == true;
			}
			if(scarecheck == true){
				if (playerToMonsterDistance < fireIntheHoleDistance) {
					if (playerToMonsterDistance < knipeAttackDistance) {
						Pattern (monsterNormalState.KA);
					} else if (playerToMonsterDistance <= jumpKnipeAttackDistance & playerToMonsterDistance > knipeAttackDistance) {
						Pattern (monsterNormalState.JKA);		
					} else if (playerToMonsterDistance <= sitDownFireIntheHoleDistance & playerToMonsterDistance < jumpKnipeAttackDistance) {
						Pattern (monsterNormalState.SFIH);
					} else if (playerToMonsterDistance > sitDownFireIntheHoleDistance) {
						Pattern (monsterNormalState.FIH);
					}

				}
			
			
			
			
			
			
			
//			if (playerToMonsterDistance < fireIntheHoleDistance) {
//				if (playerToMonsterDistance > sitDownFireIntheHoleDistance) {
//					Pattern (monsterNormalState.FIH);
//				}
//				else if (playerToMonsterDistance > jumpKnipeAttackDistance) {
//					Pattern (monsterNormalState.SFIH);
//				}
//
//				else if (playerToMonsterDistance > knipeAttackDistance) {
//					Pattern (monsterNormalState.JKA);
//				}
//				else if (playerToMonsterDistance > 0)
//					Pattern (monsterNormalState.KA);
//			
			
			}
		}

			
			



			//Pattern (monsterNormalState.Attack);


//			if (Input.GetKeyDown (KeyCode.A)) {
//				Debug.Log ("hi");
//				Pattern (monsterNormalState.Attack);


				//Pattern (monsterNormalState.Run);
				//sR.flipX =true;
				//sr.flipX == true;

			
//			if(Input.GetKeyUp(KeyCode.A))
//			{
//				sR.flipX = false;
//			}
			

		
			if (playerRealizeDistance >= 1000) {
				Pattern (monsterNormalState.Death);

			}

		if (life <= 0) {
			Pattern (monsterNormalState.Death);
		}
	}


	public void Pattern(monsterNormalState state){
		switch(state){
		case monsterNormalState.Idle:
			{

				Idle ();break;
			}
		case monsterNormalState.Scare:
			{
				Scare ();break;
			}

		case monsterNormalState.Run:
			{
				Run ();	break;
			}

		case monsterNormalState.Death:
			{
				Death ();break;
			}

		case monsterNormalState.Attack:
			{
				Attack ();break;
			}
		case monsterNormalState.FIH:
			{
				monsterAni.SetTrigger ("FireIntheHole");break;
			}
		case monsterNormalState.SFIH:
			{
				monsterAni.SetTrigger ("SitDownFireIntheHole");	break;
			}
		case monsterNormalState.JKA:
			{
				monsterAni.SetTrigger ("JumpKnipeAttack");	break;
			}
		case monsterNormalState.KA:
			{
				monsterAni.SetTrigger ("Knipeattack");	break;
			}


		}
	}


	public void OncollisionEnter(Collider coll){		
		if (coll.gameObject.layer == LayerMask.NameToLayer ("playerweapon")) {
			life -= 1;
		}
	}

	

	
	public void Idle(){
		monsterAni.SetTrigger ("Idle");
	}

	public void Scare(){
		monsterAni.SetTrigger ("Scare");
		monsterAni.SetTrigger ("ScareRun");
		scarecheck = true;
	}
	
	
	public void  Attack(){
		{
			monsterAni.SetTrigger ("sitdowmfireinthehole");
			if (attacktype == 0) {
				if (sR.flipX == true) {
					GameObject bullet = Instantiate (bulletPrefeb, firePosition [1].position, Quaternion.identity) as GameObject;
					bullet.transform.rotation = new Quaternion (0, 0, 180, 0);
				} else {
					GameObject bullet = Instantiate (bulletPrefeb, firePosition [0].position, Quaternion.identity) as GameObject;
				}
			}
	
			if (attacktype == 1) {

			} else
				Idle ();
		}
	}
	
	public void Run()
	{
		this.gameObject.transform.Translate(4*Time.deltaTime, 0, 0);
		monsterAni.SetTrigger ("Run");
	}

	public void Death(){
		Destroy (this.gameObject, 5);

	}




}


