using UnityEngine;
using System.Collections;

//빌어먹을 런상태제어, 몬스터 공격상태에서 몬스터움직이기.

public class NormalSolder : MonsterSetting {
	//monsterSetting contain monsterinfomation; 

	public int weaponAttackCyclic;
	public GameObject bulletPrefeb;
	public Animator monsterAni;
	public Transform[] firePosition;
	public GameObject Knipe;

	//cheak list
	//scarecheck = if(monsterrealize) once true -> no return scare;
	bool scareCheck = false;
	//sprite flip on off
	bool spinCheck = false;
	bool runCheck = false;
	//bool attackCheck = false;


	//attack type distance 
	int fireIntheHoleDistance=20;
	int sitDownFireIntheHoleDistance = 10;
	int jumpKnipeAttackDistance = 5;
	int knipeAttackDistance = 2;
	float attackCyle = 0;
	public float testvalue;
	//bullet instantiate, knipe wear timing == animation event process;

	Vector2 distanceVector2;
	float playerToMonsterDistance;
	float turnCheckDistance;

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
		playerRealizeDistance = 40;

		//test
		monsterAni = gameObject.GetComponent<Animator> ();
		Pattern (monsterNormalState.Idle);



		player = GameObject.Find ("player");
		Knipe = GameObject.Find ("knipe");

		Knipe.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
		//test
		if (life != 0) {
			//such player distance and rotate;
			distanceVector2 = new Vector2 (this.gameObject.transform.position.x - player.gameObject.transform.position.x, this.gameObject.transform.position.y - player.gameObject.transform.position.y);
			//if turntoplayer == +, ->;  else turntoplayer == - ,<-;
			playerToMonsterDistance = Mathf.Abs(distanceVector2.x);
			turnCheckDistance = distanceVector2.x;
			testvalue = playerToMonsterDistance;

			if (turnCheckDistance >= 0) {
				spinCheck = true;
			}
			if (turnCheckDistance < 0) {
				spinCheck = false;
			}
			TurnMonster ();

			//pattern Condition;
			if (playerToMonsterDistance > playerRealizeDistance) {
				Pattern (monsterNormalState.Idle);
			}
			if (playerToMonsterDistance <= playerRealizeDistance &&	 !scareCheck) {
				Pattern (monsterNormalState.Scare);
			}
			if (scareCheck) {
				if (playerToMonsterDistance > fireIntheHoleDistance) {
					runCheck = true;
					Pattern (monsterNormalState.Run);
				}

				//pattern is Taked playerToMonsterDistance;
				if (playerToMonsterDistance <= fireIntheHoleDistance) {
					attackCyle += Time.deltaTime;	
					runCheck = false;
					if (playerToMonsterDistance < knipeAttackDistance) {
						if (attackCyle >= 3) {
							Pattern (monsterNormalState.KA);
						}
					} else if (playerToMonsterDistance <= jumpKnipeAttackDistance & playerToMonsterDistance > knipeAttackDistance) {
						if (attackCyle >= 3) {
							Pattern (monsterNormalState.JKA);
						}	
					} else if (playerToMonsterDistance <= sitDownFireIntheHoleDistance & playerToMonsterDistance > jumpKnipeAttackDistance) {
						if (attackCyle >= 3) {
							Pattern (monsterNormalState.SFIH);
						}
					} else if (playerToMonsterDistance > sitDownFireIntheHoleDistance) {
						if (attackCyle >= 3) {
							Pattern (monsterNormalState.FIH);
						}
					}

				}

				//monster death Condition;
				if (playerRealizeDistance >= 100) {
					Pattern (monsterNormalState.Death);
				}


			
			}
		}
		if (life <= 0) {
			Pattern (monsterNormalState.Death);
		}

	}

//StartCoroutine(Shoot());

//	IEnumerator Shoot(){
//		//isShooting = true; 
//		for(int i=0;i<10;i++){yield return new WaitForSeconds(0.5f);
//			//총쏘기
//		}
//		//isShooting = false;
//		} 

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

		case monsterNormalState.Idle:
			{
				monsterAni.SetTrigger ("Idle");
				break;
			}
		case monsterNormalState.Scare:
			{
				monsterAni.SetTrigger ("Scare");
				this.gameObject.transform.Translate (0 * Time.deltaTime, 0, 0);
				scareCheck = true;

				if (scareCheck) {
					monsterAni.SetTrigger ("Idle");
				}
				break;
			}

		case monsterNormalState.Run:
			{
				if(runCheck){
					monsterAni.SetTrigger ("Run");
					if (spinCheck) {
						this.gameObject.transform.Translate (-4 * Time.deltaTime, 0, 0);
						monsterAni.SetTrigger ("Idle");

					} else if (!spinCheck) {
						this.gameObject.transform.Translate (4 * Time.deltaTime, 0, 0);
						monsterAni.SetTrigger ("Idle");
					}
				}

			
				Debug.Log ("ho");
				break;
			}

		case monsterNormalState.Death:
			{
				monsterAni.SetTrigger ("Death");
				if (spinCheck) {
					sR.flipX = true;
				} else if(!spinCheck)
					sR.flipX = false;
				Destroy (this.gameObject, 5);

				break;
			}


		case monsterNormalState.FIH:
			{
				monsterAni.SetTrigger ("FireIntheHole");
				attackCyle = 0;

				if (playerToMonsterDistance > fireIntheHoleDistance || playerToMonsterDistance <= sitDownFireIntheHoleDistance) {
					monsterAni.SetTrigger ("Idle");
				}
				break;
			}
		case monsterNormalState.SFIH:
			{
				monsterAni.SetTrigger ("SitDownFireIntheHole");
				attackCyle = 0;
				if (playerToMonsterDistance > sitDownFireIntheHoleDistance || playerToMonsterDistance <= jumpKnipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
				}
				break;
			}
		case monsterNormalState.JKA:
			{
				monsterAni.SetTrigger ("JumpKnipeAttack");
				attackCyle = 0;
				if (playerToMonsterDistance > jumpKnipeAttackDistance || playerToMonsterDistance <= knipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
				}
				break;

			}
		case monsterNormalState.KA:
			{
				monsterAni.SetTrigger ("Knipeattack");	
				attackCyle = 0;
				if (playerToMonsterDistance > knipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
				}
				break;

			}


		}
	}


	public void OncollisionEnter2D(Collider2D coll){		
		if (coll.gameObject.layer == LayerMask.NameToLayer ("playerweapon")) {
			life -= 1;
		}
	}

	public void InstantiateBullet()
	{
		if (spinCheck) {
			GameObject bullet = Instantiate (bulletPrefeb, firePosition [1].position, Quaternion.identity) as GameObject;
			bullet.transform.rotation = new Quaternion (0, 0, 180, 0);
		} else if(!spinCheck){
			GameObject bullet = Instantiate (bulletPrefeb, firePosition [0].position, Quaternion.identity) as GameObject;
		}

		Debug.Log ("hay");
	}

	public void KnipeWear()
	{
		Knipe.SetActive (true);
		if (spinCheck) {
			Knipe.transform.rotation = new Quaternion (0, 180, 0, 0);
			Knipe.transform.position = new Vector3 (-0.1f, 0.1f, 0f);
		}
		else if(!spinCheck)
		{
		Knipe.transform.rotation = new Quaternion (0, 0, 0, 0);
		Knipe.transform.position = new Vector3 (0.1f, 0.1f, 0f);
		}
	}

	public void KnipeUndress()
	{
		Knipe.SetActive (false);
	}





}


