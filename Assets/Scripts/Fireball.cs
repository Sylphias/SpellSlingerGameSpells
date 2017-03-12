using System.Collections;
using UnityEngine;
using UnityEditor;
namespace Spells
{
	public class Fireball : ProjectileSpell
	{

		private GameObject fireballPrefab;
		private ContactPoint point;
		private GameObject impactPrefab;
		private float duration, radius, damage, projectileSpeed, explosionForce;

		public void initialize()
		{
			this.duration = 5.0f;
			this.radius = 5.0f;
			this.damage = 20.0f;
			this.projectileSpeed = 10.0f;
			this.explosionForce = 5.0f;
			fireballPrefab = gameObject;
			impactPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicProjectiles/Prefabs/Fire/FireImpactMega.prefab", typeof(GameObject))as GameObject;
		}


		void Start () {
			initialize();
			Destroy (fireballPrefab, duration);
		}

		void OnCollisionEnter(Collision col){
			point = col.contacts [0];
			Destroy (fireballPrefab);
		}
			
		void OnDestroy(){
			GameObject go = (GameObject)Instantiate (impactPrefab, gameObject.transform.position, gameObject.transform.rotation);
			Vector3 explosionPoint;
			if (point.Equals(null)) {
				explosionPoint = gameObject.transform.position;
			} else {
				explosionPoint = gameObject.transform.position;
			}
	
			Collider[] colliders = Physics.OverlapSphere (explosionPoint,radius);
			foreach (Collider c in colliders) {
				if (c.GetComponent<Rigidbody>() == null) {
					continue;
				}
				if (c.tag.Equals ("Player")) {
					Rigidbody otherPlayerBody = c.GetComponent<Rigidbody> ();
					RaycastHit rch;
					Physics.Linecast (gameObject.transform.position, otherPlayerBody.position, out rch);
					if(rch.collider.gameObject.tag == "Player"){
						Debug.Log ("Hit");
						otherPlayerBody.AddExplosionForce (explosionForce, explosionPoint, radius, 1, ForceMode.Impulse);
						c.SendMessage ("TakeDamage", damage);
					}
				}
			}
	
			Destroy (go, 1);
		}
	
		// Update is called once per frame
		void Update () {
			transform.Translate (Vector3.forward*Time.deltaTime*projectileSpeed);	
		}
			

	}
}