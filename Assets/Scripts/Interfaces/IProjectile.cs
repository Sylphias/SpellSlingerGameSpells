using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
	public interface IProjectile
	{
		float ProjectileSpeed{ get; set;}
		float Radius{ get; set;}
		float Damage{ get; set; }
		float ExplosionForce{ get; set;}
		float Cooldown{ get; set; }
		void initialize (float cooldown, float duration, float radius, float dmg, float speed, float force);
		void explosionScan (IDictionary messages,Collider[] colliders, Vector3 explosionPoint);
	}
}

