﻿using UnityEngine;
using UnityEditor;
using System.Collections;
namespace Spells{
	public class Swiftness : BuffSpells {
		private float speedModifier;

		public float SpeedModifier{
			get{ return speedModifier; }
			set{ speedModifier = value; }
		}

		public void initialize(float duration, float cooldown,GameObject player,float speedModifier){
			Duration = duration;
			Cooldown = cooldown;
			Player = player;
			this.speedModifier = speedModifier;
		}

		// Use this for initialization
		void Start () {
			gameObject.transform.Rotate (-90, 0, 0);
			GameObject swiftnessEnchantPrefab = AssetDatabase.LoadAssetAtPath ("Assets/MagicArsenal/MagicEnchant/Prefabs/StormEnchant.prefab", typeof(GameObject))as GameObject;	
			GameObject go = Instantiate (swiftnessEnchantPrefab,gameObject.transform.position,gameObject.transform.rotation) as GameObject;
			if (!go.GetComponent<ParticleSystem> ().IsAlive ()) {
				Destroy (go);
			}
			Player.GetComponent<PlayerScript>().Swift (2);
			Destroy (gameObject,5);
		}

		// Update is called once per frame
		void Update () {
			gameObject.transform.position = Player.transform.position;
		}
	}
}
