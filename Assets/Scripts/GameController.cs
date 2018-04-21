using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class GameController : MonoBehaviour {

    [SerializeField] PlayerController pc;
    [SerializeField] Racket racket;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ZombieReachedEnd() {
        pc.DamagePlayer(1);
        if (!pc.IsAlive()) {
            print("Oh no! I is Dead");
            // TODO: end game
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = true;
            racket.gameObject.SetActive(false);
        }
    }
}
