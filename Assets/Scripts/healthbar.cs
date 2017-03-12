using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class healthbar : MonoBehaviour {

	// Use this for initialization
	public RawImage currentHealthBar;

	public float maxHealth;
	private float currHealth;

	void Start () {
		currHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		float ratio = currHealth / maxHealth;
		currentHealthBar.rectTransform.localScale = new Vector3 (ratio, 1, 1);
		if(currHealth ==0) {
			Debug.Log ("Dead");
		}

	}

	public void TakeDamage(float damage){
		currHealth -= damage;
		if (currHealth < 0) {
			currHealth = 0;
		}
		Update();
	}


	public void Heal(float heal){
		currHealth += heal;
		if (currHealth > 100) {
			currHealth = 100;
		}
		Update();
	}
}
