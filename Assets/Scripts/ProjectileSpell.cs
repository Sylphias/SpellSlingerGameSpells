using UnityEngine;
using System.Collections;
public class ProjectileSpell: MonoBehaviour,ISpell
{

	public ProjectileSpell ()
	{

	}

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

	public bool damage{
		get{return damage;}
		set{ this.damage = value; }
	}

	public bool explosiveForce{
		get{ return explosiveForce; }
		set{ this.explosiveForce = value; }
	}
		
}


