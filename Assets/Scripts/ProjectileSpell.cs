using UnityEngine;
using System.Collections;
public class ProjectileSpell: MonoBehaviour{
	private bool isCooldown;

	public float duration{
		get{return duration;}
		set{this.duration = value;}
	}

	public float cooldown{
		get{ return cooldown; }
		set{ this.cooldown = value; }
	}

	public bool isDamage{
		get{ return isDamage;}
		set{ this.isDamage = value; }
	}

	public float damage{
		get{return damage;}
		set{ this.damage = value; }
	}

	public float radius{
		get{ return radius; }
		set{ this.radius = value; }
	}

	public float explosionForce{
		get{ return explosionForce; }
		set{ this.explosionForce = value; }
	}

	public float projectileSpeed{
		get{ return projectileSpeed; }
		set{ this.projectileSpeed = value; }
	}
//	Checks if spells is on cooldown
	public bool isOnCooldown(){
		return true;
	}
		
}


