using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Spells
{
	public class Fireball : ProjectileSpell
	{

		private GameObject fireballPrefab;
		private ContactPoint point;
		private GameObject impactPrefab;
		//		private float duration, radius, damage, projectileSpeed, explosionForce;

		void Start () {
			impactPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicProjectiles/Prefabs/Fire/FireImpactMega.prefab", typeof(GameObject))as GameObject;
			Destroy (gameObject,Duration);
		}

		void OnCollisionEnter(Collision col){
			point = col.contacts [0];
			Destroy (gameObject);
		}

		void OnDestroy(){
			GameObject go = (GameObject)Instantiate (impactPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Vector3 explosionPoint;
			explosionPoint = gameObject.transform.position;
			Collider[] colliders = Physics.OverlapSphere (explosionPoint,Radius);
			Dictionary<string,float> messages = new Dictionary<string,float> ();
			messages.Add ("TakeDamage", Damage);
			explosionScan (messages, colliders, explosionPoint);
			Destroy (go, 1);
		}

		// Update is called once per frame
		void Update () {
			transform.Translate (Vector3.forward*Time.deltaTime*ProjectileSpeed);	
		}
	}
}