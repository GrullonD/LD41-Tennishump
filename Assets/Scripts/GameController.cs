using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] PlayerController pc;
    [SerializeField] Racket racket;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] int scoreMultiplier = 100;
    [SerializeField] Text scoreText, gameOverScoreText;
    [SerializeField] GameObject heartContainer, heartPrefab;
    [SerializeField] Sprite fullHeart, emptyHeart;
    public List<GameObject> heartContainerHearts;
    private AudioSource audioSource;
    [SerializeField] private AudioClip zombieEndSound;


    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    private void Awake() {
        this.audioSource = GetComponent<AudioSource>();
        this.scoreText.text = "Score: " + this.Score;
        this.gameOverScoreText.text = "Score: " + this.Score;
        CreateHearts();
    }

    private void CreateHearts() {
        float offset = 150f;
        Vector3 position = new Vector3(0, 0, 0);
        GameObject heart;
        RectTransform heartTransform;
        this.heartContainerHearts = new List<GameObject>();
        for (int i = 0; i < this.pc.Health; i++) {
            heart = Instantiate(this.heartPrefab, this.heartContainer.transform);
            heartTransform = heart.GetComponent<RectTransform>();
            heartTransform.localPosition += position;
            position.x += offset;
            heartContainerHearts.Add(heart);
        }
    }

    private void UpdateHearts() {
        if (this.pc.Health >= 0) {
            GameObject heart = this.heartContainerHearts[this.pc.Health];
            heart.GetComponent<Image>().overrideSprite = this.emptyHeart;
        }
    }

    public void RestartGame() {
        print("restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ZombieReachedEnd() {
        pc.DamagePlayer(1);
        UpdateHearts();
        this.audioSource.Stop();
        this.audioSource.PlayOneShot(this.zombieEndSound);
        if (!pc.IsAlive()) {
            print("Oh no! I is Dead");
            // TODO: end game
            Camera.main.gameObject.GetComponent<BlurOptimized>().enabled = true;
            racket.gameObject.SetActive(false);
            this.gameOverScreen.SetActive(true);
            Cursor.visible = true;
        }
    }
    
    public void AddToScore(int score) {
        this.Score += score * scoreMultiplier;
        this.scoreText.text = "Score: " + this.Score;
        this.gameOverScoreText.text = "Score: " + this.Score;
    }
}
