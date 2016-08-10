using UnityEngine;
using System.Collections;

public class CreateBullet : MonoBehaviour {
	public bool haveLife;
	public float moveSpeed;
	public int damage;
	public SpriteRenderer sR;

	public bool HaveLife
	{
		get { return haveLife;}
	}

	public float MoveSpeed{
		get { return moveSpeed;}
	}

	public int Damage{
		get { return damage;}
	}




}
