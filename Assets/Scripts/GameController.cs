using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField] PlayerController pc;
    [SerializeField] Racket racket;
    [SerializeField] GameObject gameOverScreen;

	public void RestartGame() {
        print("restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ZombieReachedEnd() {
        pc.DamagePlayer(1);
        if (!pc.IsAlive()) {
            print("Oh no! I is Dead");
            // TODO: end game
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = true;
            racket.gameObject.SetActive(false);
            this.gameOverScreen.SetActive(true);
            Cursor.visible = true;
        }
    }
}
