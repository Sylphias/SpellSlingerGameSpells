using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spells;
using UnityEditor;

public class PlayerScript : MonoBehaviour {

	public float moveForce = 0f;
	public GameObject fireBall, earthWall;
	public Transform gun;
	public float shootRate = 0f;
	public float spell1CD = 0f;
	public float spell2CD = 0f;
	public float spell3CD = 0f;
	public float spell4CD = 0f;
	public float earthWallCooldown = 0f;
	public float rotationalForce = 0f;
	private List<IBuffable> buffList= new List<IBuffable> ();
	private float fireBallCooldownTimer,earthWallCooldownTimer,iceballCooldownTimer;
	healthbar hb;
	// Use this for initialization
	void Start () {
		gameObject.tag = "Player";
		hb = gameObject.GetComponent<healthbar> ();
		fireBallCooldownTimer = Time.time;
		earthWallCooldownTimer = Time.time;
		iceballCooldownTimer = Time.time;
	}


	// Update is called once per frame
	void Update () {
		float h = Input.GetAxisRaw ("Horizontal") * rotationalForce;
		float v = Input.GetAxisRaw ("Vertical") * moveForce;

		transform.Rotate(0, h, 0);
		transform.Translate(0, 0, v);
		if(Input.GetKey(KeyCode.Space)){
 			if(Time.time > spell1CD){
				GameObject fireballPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicProjectiles/Prefabs/Fire/FireProjectileNormal.prefab", typeof(GameObject))as GameObject;
				GameObject go = (GameObject)Instantiate (fireballPrefab, gun.position, gun.rotation);
				Fireball fb = go.GetComponent<Fireball>();
				fb.initialize (5,5.0f, 5.0f, 20.0f, 10.0f, 7.0f);		
				spell1CD = Time.time + 1;
			}
		}
		if (Input.GetKey (KeyCode.Q)) {
			if(Time.time > spell2CD){
				GameObject iceballPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicProjectiles/Prefabs/Frost/FrostProjectileNormal.prefab", typeof(GameObject))as GameObject;
				GameObject go = (GameObject)Instantiate (iceballPrefab, gun.position, gun.rotation);
				IceBall ib = go.GetComponent<IceBall>();
				ib.initialize (5,5.0f, 5.0f, 20.0f, 10.0f, 3.0f);		
				spell2CD = Time.time + ib.Cooldown;
			}
		}

		if (Input.GetKey (KeyCode.R)) {
			if(Time.time > spell2CD){
				GameObject stonefistPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicProjectiles/Prefabs/Earth/EarthProjectileMega.prefab", typeof(GameObject))as GameObject;
				GameObject go = (GameObject)Instantiate (stonefistPrefab, gun.position, gun.rotation);
				StoneFist sf = go.GetComponent<StoneFist>();
				sf.initialize (5,5.0f, 5.0f, 20.0f, 5.0f, 2.0f);		
				spell2CD = Time.time + sf.Cooldown;
			}
		}
		if(Input.GetKey(KeyCode.E)){
			if (Time.time > earthWallCooldownTimer) {
				Instantiate (earthWall, gun.position, gun.rotation);
				earthWallCooldownTimer = Time.time + shootRate;
			}
		}

		if(Input.GetKey(KeyCode.Alpha1)){
			if (Time.time > spell3CD) {
				GameObject swiftnessPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicAuras/Prefabs/SpinningAura/SpinningStorm.prefab", typeof(GameObject))as GameObject;
				GameObject go = (GameObject)Instantiate (swiftnessPrefab,transform.position,transform.rotation);
				Swiftness sft = go.GetComponent<Swiftness>();
				sft.initialize (2,5,gameObject,2);		
				spell3CD = Time.time + sft.Cooldown;
			}
		}
		checkBuffs ();
	}

	// Checks and updates buffs on the user. 
	public void checkBuffs(){
		float oldTime = Time.time;
		foreach (IBuffable b in buffList){
			if ((Time.time - oldTime) >= b.TickTime) {
				b.Apply (this);
				oldTime = Time.time;
			}
			if(b.Finished){
				b.Reset (this);
				buffList.Remove (b);
			}
		}
	}


	public void TakeDamage(float damage){		
		Debug.Log ("damaged");
		hb.CurrentHealth -= damage;
		if (hb.CurrentHealth < 0) {
			hb.CurrentHealth = 0;
		}
		Update();
	}


	public void Heal(float heal){
		hb.CurrentHealth += heal;
		if (hb.CurrentHealth > 100) {
			hb.CurrentHealth = 100;
		}
		Update();
	}



	public void Swift(float speedMultiplier){
		SwiftBuff sb = new SwiftBuff(5,0,speedMultiplier,moveForce,rotationalForce);
		if (buffList.Count == 0) {
			buffList.Add (sb);
			return;
		}
		foreach (IBuffable b in buffList) {
			if (b.Type == "Swift") {
				FrostDebuff sbOld = b as FrostDebuff;
				if (sbOld.SpeedMultiplier < sb.SpeedMultiplier) {
					buffList.Remove (b);
					buffList.Add (sb);
				} else {
					sbOld.FinishTime = sb.FinishTime;
				}
			}
		}

	}


	public void Chilled(float speedMultiplier){
		FrostDebuff fd = new FrostDebuff(5,0,speedMultiplier,moveForce);
		if (buffList.Count == 0) {
			buffList.Add (fd);
			return;
		}
		foreach (IBuffable b in buffList) {
			if (b.Type == "Chilled") {
				FrostDebuff fdOld = b as FrostDebuff;
				if (fdOld.SpeedMultiplier < fd.SpeedMultiplier) {
					buffList.Remove (b);
					buffList.Add (fd);
				} else {
					fdOld.FinishTime = fd.FinishTime;
				}
			}
		}

	}
}
