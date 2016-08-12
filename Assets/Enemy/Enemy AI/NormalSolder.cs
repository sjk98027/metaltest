using UnityEngine;
using System.Collections;

public class NormalSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	public int weaponAttackCyclic;
	public GameObject bulletPrefeb;
	public Animator monsterAni;
	public Transform[] firePosition;
	public GameObject knipe;

	//cheak list
	//scarecheck = if(monsterrealize) once true -> no return scare;
	public bool scareCheck = false;
	//sprite flip on off
	public bool spinCheck = false;
	public bool runCheck = false;

	//attack type distance 
	public int fireIntheHoleDistance=20;
	public int sitDownFireIntheHoleDistance = 10;
	public int jumpKnipeAttackDistance = 5;
	public int knipeAttackDistance = 2;
	public float attackcyle = 0;

	//bullet instantiate, knipe wear timing == animation event process;



	public enum monsterNormalState{
		Idle ,
		Scare,
		Run,
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
		attacktype = 0;
		sR = gameObject.GetComponent<SpriteRenderer> ();
		playerRealizeDistance = 30;


		//test
		monsterAni = gameObject.GetComponent<Animator> ();
		Pattern (monsterNormalState.Idle);



		player = GameObject.Find ("player");
		knipe = gameObject.GetComponent<GameObject> ();
		knipe.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		//test
		if (life != 0) {
			//such player distance and rotate;
			Vector2 distanceVector2 = new Vector2 (this.gameObject.transform.position.x - player.gameObject.transform.position.x, this.gameObject.transform.position.y - player.gameObject.transform.position.y);
			//if turntoplayer == +, ->;  else turntoplayer == - ,<-;
			float playerToMonsterDistance = Mathf.Abs(distanceVector2.x);
			float turnCheckDistance = distanceVector2.x;


			if (turnCheckDistance >= 0) {
				spinCheck = true;
			}
			if (turnCheckDistance < 0) {
				spinCheck = false;
			}
			TurnMonster ();

//			float playerToMonsterDistance = Vector3.Distance (this.transform.position, player.gameObject.transform.position);
//
			if (playerToMonsterDistance > playerRealizeDistance) {
				Pattern (monsterNormalState.Idle);
			}
			if (playerToMonsterDistance <= playerRealizeDistance & scareCheck == false) {
				Pattern (monsterNormalState.Scare);
			}
			if(scareCheck == true){
				if (playerToMonsterDistance > fireIntheHoleDistance) {
					runCheck = true;
					Pattern (monsterNormalState.Run);
				}
				if (playerToMonsterDistance < fireIntheHoleDistance) {
					attackcyle += Time.deltaTime;
					runCheck = false;
						if (playerToMonsterDistance < knipeAttackDistance) {
							if (attackcyle >= 1.5) {
								Pattern (monsterNormalState.KA);
							}
						} else if (playerToMonsterDistance <= jumpKnipeAttackDistance & playerToMonsterDistance > knipeAttackDistance) {
							if (attackcyle >= 1.5) {
								Pattern (monsterNormalState.JKA);
							}	
						} else if (playerToMonsterDistance <= sitDownFireIntheHoleDistance & playerToMonsterDistance > jumpKnipeAttackDistance) {
							if (attackcyle >= 3) {
								Pattern (monsterNormalState.SFIH);
							}
						} else if (playerToMonsterDistance > sitDownFireIntheHoleDistance) {
							if (attackcyle >= 1.5) {
								Pattern (monsterNormalState.FIH);
							}
						}

				}
				if (playerRealizeDistance >= 100) {
					Pattern (monsterNormalState.Death);
				}

				if (life <= 0) {
					Pattern (monsterNormalState.Death);
				}
			
			}
		}
	}


	public void TurnMonster()
	{
		if (!spinCheck) {
			sR.flipX = false;
		}
		else if (spinCheck) {
			sR.flipX = true;
		}
	}


	public void Pattern(monsterNormalState state){
		switch(state){
		//if()
		case monsterNormalState.Idle:
			{
				monsterAni.SetTrigger ("Idle");
				break;
			}
		case monsterNormalState.Scare:
			{
				monsterAni.SetTrigger ("Scare");
				monsterAni.SetTrigger ("ScareRun");
				scareCheck = true;
				this.gameObject.transform.Translate (6 * Time.deltaTime, 0, 0);
				break;
			}

		case monsterNormalState.Run:
			{
				if (spinCheck) {
					this.gameObject.transform.Translate (-4 * Time.deltaTime, 0, 0);
					monsterAni.SetTrigger ("Run");
				} else if (!spinCheck) {
					this.gameObject.transform.Translate (4 * Time.deltaTime, 0, 0);
					monsterAni.SetTrigger ("Run");
				}
					break;
			}

		case monsterNormalState.Death:
			{
				Destroy (this.gameObject, 5);
				break;
			}


		case monsterNormalState.FIH:
			{
				monsterAni.SetTrigger ("FireIntheHole");
				attackcyle = 0;
				break;
			}
		case monsterNormalState.SFIH:
			{
				monsterAni.SetTrigger ("SitDownFireIntheHole");
				attackcyle = 0;
				break;
			}
		case monsterNormalState.JKA:
			{
				monsterAni.SetTrigger ("JumpKnipeAttack");
				attackcyle = 0;
				break;

			}
		case monsterNormalState.KA:
			{
				monsterAni.SetTrigger ("Knipeattack");	
				attackcyle = 0;
				break;

			}


		}
	}


	public void OncollisionEnter(Collider coll){		
		if (coll.gameObject.layer == LayerMask.NameToLayer ("playerweapon")) {
			life -= 1;
		}
	}

	public void InstantiateBullet()
	{
		if (sR.flipX == true) {
			GameObject bullet = Instantiate (bulletPrefeb, firePosition [1].position, Quaternion.identity) as GameObject;
			bullet.transform.rotation = new Quaternion (0, 0, 180, 0);
		} else {
			GameObject bullet = Instantiate (bulletPrefeb, firePosition [0].position, Quaternion.identity) as GameObject;
		}

		Debug.Log ("hay");
	}

	public void KnipeWear()
	{
		knipe.SetActive (true);
	}

	public void KnipeUndress()
	{
		knipe.SetActive (false);
	}





}


