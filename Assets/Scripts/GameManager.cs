using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public float hexagonSpeed = 1f;
    public float timeToWin = 75;

    public AudioController audioController;

    public Text actualTimeText;
    public Text lastTimeText;
    public Text highTimeText;

    public GameObject gameOverUI;
    public GameObject stageCompleteUI;
    public GameObject bestTimerUI;
    public GameObject lastTimerUI;

    float actualTimeNumber = 0;
    float lastTimeNumber = 0;

    float highScore;
    int level;

    int flag = 0;

    [HideInInspector] public bool playerDied = false;

    void Awake() {

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {

        actualTimeNumber = 0;
        Cursor.visible = false;

        // Busca el tiempo maximo del nivel actual
        level = SceneManager.GetActiveScene().buildIndex;

        if (level == 1){
            highScore = SaveLoad.instance.highScore1;
        }else if (level == 2){
            highScore = SaveLoad.instance.highScore2;
        } else if (level == 3){
            highScore = SaveLoad.instance.highScore3;
        }
    }

    void Update() {

        // Player muere
        if (playerDied){

            flag++;

            if (flag <= 25){       

                StopPlayerMovement();

                // Detener el movimiento de los obstaculos
                GameObject hexagonSpawner = GameObject.Find("Hexagon Spawner");
                hexagonSpawner.gameObject.GetComponent<Spawner>().spawnRate = 0;
                hexagonSpeed = 0f;

                // Detener la musica
                audioController.StartCoroutine(audioController.PlayerDeadSound());

                // Actualizar los tiempos
                lastTimeNumber = actualTimeNumber;
                lastTimeText.text = lastTimeNumber.ToString("F2");

                actualTimeText.transform.parent.gameObject.GetComponent<Animator>().enabled = true;

                // Gana el nivel 
                if (Time.timeSinceLevelLoad <= timeToWin){
                    Invoke("ShowGameOver", 2.5f);
                }else{
                    Invoke("ShowStageComplete", 2.5f);
                }
            }

            if (Input.GetKey(KeyCode.M))
            {
                GameObject tempButton = GameObject.Find("MenuButton");
                Button menuButton = tempButton.GetComponent<Button>();
                menuButton.onClick.Invoke();
            }
            else if (Input.GetKey(KeyCode.R))
            {
                if (gameOverUI.activeSelf)
                {
                    GameObject tempButton = GameObject.Find("RetryButton");
                    Button retryButton = tempButton.GetComponent<Button>();
                    retryButton.onClick.Invoke();
                }
            }
        }
        else{

            if (Input.GetKey(KeyCode.Escape))
                playerDied = true;

            // Contador de tiempo
            actualTimeNumber += Time.deltaTime;
            actualTimeText.text = actualTimeNumber.ToString("F2");
        }
    }
    
    void StopPlayerMovement(){

        GameObject player = GameObject.Find("Player");
        player.gameObject.GetComponent<PlayerController>().speed = 0;
    }

    void ShowGameOver(){

        actualTimeText.transform.parent.gameObject.SetActive(false);

        Cursor.visible = (true);

        CheckBestTime();

        gameOverUI.SetActive(true);

        ShowTimers();

        gameOverUI.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
    }

    void ShowStageComplete(){

        actualTimeText.transform.parent.gameObject.SetActive(false);

        Cursor.visible = (true);

        CheckBestTime();

        stageCompleteUI.SetActive(true);

        ShowTimers();

        stageCompleteUI.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        stageCompleteUI.transform.GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
    }

    void CheckBestTime(){
        
        if (lastTimeNumber > highScore){

            highScore = lastTimeNumber;
            SaveLoad.instance.SaveBestTime(level, lastTimeNumber);
        }

        highTimeText.text = highScore.ToString("F2");
    }

    void ShowTimers(){

        bestTimerUI.SetActive(true);
        lastTimerUI.SetActive(true);

        bestTimerUI.GetComponent<Animator>().enabled = true;
        lastTimerUI.GetComponent<Animator>().enabled = true;
    }
}
