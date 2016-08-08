using UnityEngine;
using System.Collections;

public class HaveLifeBullet : CreateBullet {
	public Animator bulletani;
	public GameObject bullet;

	AnimatorStateInfo bulletstate;

	public enum BulletStateName	{
		start = 1,
		run,
		burn
	};

	public enum BulletName{
		normalbullet = 1,

	}



	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
