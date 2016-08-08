using UnityEngine;
using System.Collections;

public class UnHaveLifeBullet : CreateBullet {
	//get gameobject target and ani and bulletobject
	public Animator bulletAni;
	public GameObject player;
	public GameObject bullet;

	//get stateinfo , bulletname
	AnimatorStateInfo bulletstate;

	public enum BulletStateName	{
		start = 1,
		run,
		burn
	};

	//test
	public enum BulletName{
		normalbullet = 1,

	}



	void start(){
		CreateBullet CB = gameObject.GetComponentInParent<CreateBullet> ();
		//player = gameObject.GetComponent<player> ();
		//MonsterSetting setting = gameObject.GetComponentInParent<MonsterSetting> ();	
	}

	void update(){
		
	}

	public void OnCollisionEnter(Collision coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("player")) {
			pattern (BulletStateName.burn);
		}

		//if()
	}



	public void pattern(BulletStateName state)
	{
		switch (state) {
		case BulletStateName.start: 
			{
				break;
			}

		case BulletStateName.run: 
			{
				break;		
			}

		case BulletStateName.burn:
			{
				break;
			}

		}
	}




}
