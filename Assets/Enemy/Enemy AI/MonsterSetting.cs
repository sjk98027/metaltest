using UnityEngine;
using System.Collections;


public class MonsterSetting : MonoBehaviour {
	//monster generic infomation
	public int life;
	public float moveSpeed;
	public float jumpSpeed;
	public float playerRealizeDistance;
	public float turnToPlayer;
	public SpriteRenderer sR;

	//Get gameobject search;
	public GameObject player;
	public GameObject bullet;

	//able unable
	public bool ableMove; // movespeed = 0  or movespeed = casebycase;
	public int attacktype; // near attack 0 ~ far attack 1;
	public bool wearWeapon; // gun, knife, 


}