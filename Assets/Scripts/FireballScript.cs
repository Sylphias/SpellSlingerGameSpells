using UnityEngine;
using System.Collections;
public class FireballScript : MonoBehaviour {
	public float expiryTime = 0f;
	// Use this for initialization
	public float explosionForce = 0f;
	public float explosionRadius = 0f;
	public float upwardsModifier = 0f;
	public float damageMultiplier = 0f;
	public bool isDamaging = true;
	public float bulletSpeedMultiplier = 0f;
	public float bulletBaseSpeed = 0f;
	public GameObject impactAnimation;

	private ContactPoint point;
	void Start () {
		Destroy (gameObject, expiryTime);
	}
		
	void OnCollisionEnter(Collision col){
		point = col.contacts [0];
		Destroy (gameObject);
	}


	void OnDestroy(){
		GameObject go = (GameObject)Instantiate (impactAnimation, gameObject.transform.position, gameObject.transform.rotation);
		Vector3 explosionPoint;
		if (point.Equals(null)) {
			explosionPoint = gameObject.transform.position;
		} else {
			explosionPoint = gameObject.transform.position;
		}
			

		Collider[] colliders = Physics.OverlapSphere (explosionPoint, explosionRadius);
		foreach (Collider c in colliders) {
			if (c.GetComponent<Rigidbody>() == null) {
				continue;
			}
			c.GetComponent<Rigidbody>().AddExplosionForce (explosionForce, explosionPoint, explosionRadius, 1, ForceMode.Impulse);
			c.SendMessage ((isDamaging) ? "TakeDamage" : "Heal", damageMultiplier * 1);
		}

		Destroy (go, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward*Time.deltaTime*bulletBaseSpeed*bulletSpeedMultiplier);	
	}
}
