using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText;
    UnityEngine.UI.Text shieldsText;
    UnityEngine.UI.Text maxScoreText;
    int score = 0;
    int maxScore = 0;
    public UnityEngine.UI.Button startButton;
    public UnityEngine.UI.Button pauseButton;
    public GameObject menu;
    public GameObject pauseMenu;
    public GameObject spaceShip;
    bool isStarted = false;
    bool isPause = true;
    bool timerIsRunning = false;
    float deathDelay = 3;
    GameObject[] gameObjects;

    public static GameControllerScript instance;

    void Start() {
        maxScoreText = GameObject.FindWithTag("maxScore").GetComponent<Text>();
        shieldsText = GameObject.FindWithTag("shields").GetComponent<Text>();

        instance = this;
        pauseMenu.SetActive(false);
        startButton.onClick.AddListener(delegate {
            DestoryAll();
            increaseScore(-score);
            Instantiate(spaceShip, new Vector3(0, 10, -20), Quaternion.identity);
            menu.SetActive(false);
            isStarted = true;
            isPause = false;
        });

        pauseButton.onClick.AddListener(delegate {
            pauseMenu.SetActive(false);
            isPause = false;
            Time.timeScale = 1;
        });
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            isPause = true;
        } else if (Input.GetKeyDown(KeyCode.Escape) && isPause) {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            isPause = false;
        }

        if (GameObject.FindWithTag("Player") is null && !timerIsRunning) {
            timerIsRunning = true;

        } else if (GameObject.FindWithTag("Player") is null) {
            if (deathDelay > 0) {
                deathDelay -= Time.deltaTime;
            } else {
                deathDelay = 3;
                timerIsRunning = false;
                menu.SetActive(true);
                maxScoreText.text = "MaxScore: " + maxScore;
                isStarted = false;
                isPause = true;
            }
        };

        if (ShipScript.instance != null) {
            shieldsText.text = "Shields: " + GameObject.FindGameObjectsWithTag("Shield").Length;
        }
    }

    public void DestoryAll() {
        gameObjects = GameObject.FindGameObjectsWithTag("Asteroid");
        for(int i = 0; i < gameObjects.Length; ++i) {
            Destroy(gameObjects[i]);
        }
        gameObjects =  GameObject.FindGameObjectsWithTag("Enemy");
        for(int i = 0; i < gameObjects.Length; ++i) {
            Destroy(gameObjects[i]);
        }
        gameObjects =  GameObject.FindGameObjectsWithTag("Medicine");
        for(int i = 0; i < gameObjects.Length; ++i) {
            Destroy(gameObjects[i]);
        }
    }

    public bool getIsStarted() {
        return isStarted;
    }

    public bool getIsPaused() {
        return isPause;
    }

    public void increaseScore(int increment) {
        score += increment;
        scoreText.text = "Score: " + score;
        if (score > maxScore) {
            maxScore = score;
        }
    }
}
