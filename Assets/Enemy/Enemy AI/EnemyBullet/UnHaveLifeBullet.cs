using UnityEngine;
using System.Collections;

public class UnHaveLifeBullet : CreateBullet {
	//get gameobject target and ani and bulletobject
	public Animator bulletAni;

//	public gameObject.Transform.ImagePosition
	public GameObject player;
	public GameObject monster;
	public GameObject bullet;





	//get stateinfo , bulletname
	AnimatorStateInfo bulletstate;

	public enum BulletName	{
		normalbullet ,
		parabolabullet,
		guidancebullet

	};


	public BulletName bulletName;

	void Start(){
		CreateBullet createBullet = gameObject.GetComponentInParent<CreateBullet> ();

		//bullet = GameObject.Find("normalbullet").GetComponent<d
		sR = gameObject.GetComponent<SpriteRenderer>();
		string name = gameObject.layer.ToString ();
		//player = gameObject.GetComponent<player> ();
		//MonsterSetting setting = gameObject.GetComponentInParent<MonsterSetting> ();
	}

	void Update(){
		RunningPattern (BulletName.normalbullet);

		Debug.Log (BulletName.guidancebullet);
		Debug.Log ("hi");

	}

	public void OnCollisionEnter(Collision coll){
		if (coll.gameObject.layer == LayerMask.NameToLayer ("playerweapon")) {
			Destroy (this.gameObject);
		}

		//if()
	}

	public void Searchbulletname()
	{
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
				break;
			}

		}
	}




}
