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
	public AnimatorStateInfo aniStateInfo;
	public AnimationEvent aniEvent;

	//cheak list
	//scarecheck = if(monsterrealize) once true -> no return scare;
	public bool scareCheck = false;
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
	public float playerToMonsterDistance;
	float turnCheckDistance;

	public enum monsterNormalStateName{
		Idle =1,
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
		Pattern (monsterNormalStateName.Idle);



		player = GameObject.Find ("player");
		Knipe = GameObject.Find ("knipe");

		Knipe.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		aniStateInfo = monsterAni.GetCurrentAnimatorStateInfo (0);

		//test
		if (life != 0) {
			//such player distance and rotate;
			distanceVector2 = new Vector2 (this.gameObject.transform.position.x - player.gameObject.transform.position.x, this.gameObject.transform.position.y - player.gameObject.transform.position.y);
			//if turntoplayer == +, ->;  else turntoplayer == - ,<-;
			playerToMonsterDistance = Mathf.Abs(distanceVector2.x);
			turnCheckDistance = distanceVector2.x;
			//testvalue = playerToMonsterDistance;

			if (turnCheckDistance >= 0) {
				spinCheck = true;
			}
			if (turnCheckDistance < 0) {
				spinCheck = false;
			}
			TurnMonster ();


			if (playerToMonsterDistance <= playerRealizeDistance &&	!scareCheck) {
				Pattern (monsterNormalStateName.Scare);
				if(aniStateInfo.IsName ("Base Layer.NormalSolderScare")){
					scareCheck = true;

					Debug.Log ("Ai");
				}



			}	




			//pattern Condition;
			else if (playerToMonsterDistance < playerRealizeDistance && scareCheck) 
			{
				if (playerToMonsterDistance > fireIntheHoleDistance) {
						runCheck = true;
						if (runCheck) {
						
							Pattern (monsterNormalStateName.Run);
							
						}
					} else {
						Pattern (monsterNormalStateName.Idle);
					}

			}


		


				//pattern is Taked playerToMonsterDistance;
				if (playerToMonsterDistance <= fireIntheHoleDistance) {
					attackCyle += Time.deltaTime;	
					runCheck = false;
					monsterAni.SetBool ("Run", false);

					if (playerToMonsterDistance < knipeAttackDistance) {
						if (attackCyle >= 1.5) {
							Pattern (monsterNormalStateName.KA);
						}
					} else if (playerToMonsterDistance <= jumpKnipeAttackDistance & playerToMonsterDistance > knipeAttackDistance) {
						if (attackCyle >= 1.5) {
							Pattern (monsterNormalStateName.JKA);
						}	
					} else if (playerToMonsterDistance <= sitDownFireIntheHoleDistance & playerToMonsterDistance > jumpKnipeAttackDistance) {
						if (attackCyle >= 1.5) {
							Pattern (monsterNormalStateName.SFIH);
						}
					} else if (playerToMonsterDistance > sitDownFireIntheHoleDistance) {
						if (attackCyle >= 1.5) {
							Pattern (monsterNormalStateName.FIH);
						}
					}

				}

				//monster death Condition;
				if (playerRealizeDistance >= 100) {
					Pattern (monsterNormalStateName.Death);
				}

			}

		if (life <= 0) {
			Pattern (monsterNormalStateName.Death);
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


	public void Pattern(monsterNormalStateName state){
		switch(state){

		case monsterNormalStateName.Idle:
			{
				monsterAni.SetBool ("Scare", false);
				monsterAni.SetTrigger ("Idle");
				break;
			}
		case monsterNormalStateName.Scare:
			{
				monsterAni.SetBool ("Scare",true);


//				{
//					monsterAni.SetTrigger ("Idle");
//					scareCheck = true;
//				}





//				monsterAni.SetTrigger ("Scare");
//				//AnimationState
//				if (monsterAni.GetCurrentAnimatorStateInfo (0).IsName ("Base Layer.Scare")) {
//					this.gameObject.transform.Translate (0 * Time.deltaTime, 0, 0);
//					scareCheck = true;
//
//					if (scareCheck) {
//						monsterAni.SetTrigger ("Idle");
//						break;
//					}
//
//				}
				break;

			}

		case monsterNormalStateName.Run:
			{
				monsterAni.SetBool ("Scare", false);
				monsterAni.SetBool("Run",true);
					if (spinCheck) {
						this.gameObject.transform.Translate (-4 * Time.deltaTime, 0, 0);
						monsterAni.SetTrigger ("Idle");

					} else if (!spinCheck) {
						this.gameObject.transform.Translate (4 * Time.deltaTime, 0, 0);
						monsterAni.SetTrigger ("Idle");
					}
				break;
			}

		case monsterNormalStateName.Death:
			{
				monsterAni.SetTrigger ("Death");
				if (spinCheck) {
					sR.flipX = true;
				} else if(!spinCheck)
					sR.flipX = false;
				Destroy (this.gameObject, 5);

				break;
			}


		case monsterNormalStateName.FIH:
			{
				monsterAni.SetTrigger ("FireIntheHole");
				attackCyle = 0;

//				if (playerToMonsterDistance > fireIntheHoleDistance || playerToMonsterDistance <= sitDownFireIntheHoleDistance) {
					monsterAni.SetTrigger ("Idle");
//				}
				break;
			}
		case monsterNormalStateName.SFIH:
			{
				monsterAni.SetTrigger ("SitDownFireIntheHole");
				attackCyle = 0;
//				if (playerToMonsterDistance > sitDownFireIntheHoleDistance || playerToMonsterDistance <= jumpKnipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
//				}
				break;
			}
		case monsterNormalStateName.JKA:
			{
				monsterAni.SetTrigger ("JumpKnipeAttack");
				attackCyle = 0;
//				if (playerToMonsterDistance > jumpKnipeAttackDistance || playerToMonsterDistance <= knipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
//				}
				break;

			}
		case monsterNormalStateName.KA:
			{
				monsterAni.SetTrigger ("Knipeattack");	
				attackCyle = 0;
//				if (playerToMonsterDistance > knipeAttackDistance) {
					monsterAni.SetTrigger ("Idle");
//				}
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

//	IEnumerator Scare(){
//		monsterAni.SetTrigger ("Scare");
//
//		yield return new WaitForSeconds (2);
//		monsterAni.SetTrigger ("Idle");
//		scareCheck = true;
//
//	}

	//StartCoroutine(Shoot());

//	IEnumerator AttackRunning(){
//		attackCheck = true;
//
//		yield return new WaitForSeconds (5);
//
//
//	}


	//	IEnumerator Shoot(){
	//		//isShooting = true; 
	//		for(int i=0;i<10;i++){yield return new WaitForSeconds(0.5f);
	//			//총쏘기
	//		}
	//		//isShooting = false;
	//		} 




}


