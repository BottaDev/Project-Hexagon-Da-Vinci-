using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    public float hexagonSpeed = 1f;
    public float timeToWin = 75;
    //public float buffTimer = 5;
    public float itemSpawnTimer = 8;
    public float timeEnergy = 100;
    public float itemEnergyValue = 15;

    public Transform[] itemSpawns;
    public GameObject item;

    public AudioController audioController;

    public Text actualTimeText;
    public Text lastTimeText;
    public Text highTimeText;
    public Text timeEnergyText;

    public GameObject gameOverUI;
    public GameObject stageCompleteUI;
    public GameObject bestTimerUI;
    public GameObject lastTimerUI;
    public Image timeEnergyBarImage;

    Spawner hexagonSpawner;
    float actualTimeNumber = 0;
    float lastTimeNumber = 0;
    //float currentBuffTime = 0;
    float currentItemSpawnTime = 0;
    float defaultHexagonSpeed;
    float defaultHexagonSpawnRate;
    bool slowModeAcive = false;

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

    void Start()
    {
        GameObject objHexagonSpawner = GameObject.Find("Hexagon Spawner");
        hexagonSpawner = objHexagonSpawner.gameObject.GetComponent<Spawner>();

        actualTimeNumber = 0;
        Cursor.visible = false;
        //currentBuffTime = buffTimer;
        defaultHexagonSpeed = hexagonSpeed;
        defaultHexagonSpawnRate = hexagonSpawner.spawnRate;

        UpdateEnergyBarUI();

        // Busca el tiempo maximo del nivel actual
        level = SceneManager.GetActiveScene().buildIndex;

        if (level == 1)
            highScore = SaveLoad.instance.highScore1;
        else if (level == 2)
            highScore = SaveLoad.instance.highScore2;
        else if (level == 3)
            highScore = SaveLoad.instance.highScore3;
    }

    void Update()
    {
        // Player muere
        if (playerDied)
            EndGame();
        else
            ContinueGame();
    }

    void EndGame()
    {
        flag++;

        if (flag <= 25)
        {
            StopPlayerMovement();

            ResetGameSpeed();

            // Detener el movimiento de los obstaculos
            hexagonSpawner.gameObject.GetComponent<Spawner>().spawnRate = 0;
            hexagonSpeed = 0f;

            // Detener la musica
            audioController.StartCoroutine(audioController.PlayerDeadSound());

            // Actualizar los tiempos
            lastTimeNumber = actualTimeNumber;
            lastTimeText.text = lastTimeNumber.ToString("F2");

            actualTimeText.transform.parent.gameObject.GetComponent<Animator>().enabled = true;
            timeEnergyText.transform.parent.gameObject.GetComponent<Animator>().enabled = true;

            // Gana el nivel 
            if (Time.timeSinceLevelLoad <= timeToWin)
                Invoke("ShowGameOverUI", 2.5f);
            else
                Invoke("ShowStageCompleteUI", 2.5f);
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

    void ContinueGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            playerDied = true;

        if (Input.GetKeyDown(KeyCode.Space) && timeEnergy > 0)
            SlowGameSpeed();
        else if (Input.GetKeyUp(KeyCode.Space))
            ResetGameSpeed();

        // Coloco este if aca porque si no nunca entra al estar en slowMode
        if (timeEnergy < 0)
        {
            slowModeAcive = false;
            timeEnergy = 0;
        }

        if (slowModeAcive)
        {
            timeEnergy -= Time.deltaTime * 10f;

            UpdateEnergyBarUI();
        }

        // Spawn Items
        currentItemSpawnTime -= Time.deltaTime;
        if (currentItemSpawnTime <= 0)
        {
            currentItemSpawnTime = itemSpawnTimer;
            SpawnItem();
        }
        
        /*
        if (slowModeAcive)
        {
            currentBuffTime -= Time.deltaTime;
            if (currentBuffTime <= 0)
            {
                currentBuffTime = buffTimer;
                ResetGameSpeed();
            }
        }
        */

        // Contador de tiempo
        actualTimeNumber += Time.deltaTime;
        actualTimeText.text = actualTimeNumber.ToString("F2");
    }

    public void SlowGameSpeed()
    {
        hexagonSpeed /= 1.7f;
        audioController.audioSource.pitch = 0.8f;
        hexagonSpawner.spawnRate /= 1.7f; 

        slowModeAcive = true;
    }

    void ResetGameSpeed()
    {
        hexagonSpeed = defaultHexagonSpeed;
        audioController.audioSource.pitch = 1;
        hexagonSpawner.spawnRate = defaultHexagonSpawnRate;

        slowModeAcive = false;
    }

    void SpawnItem()
    {
        int random = Random.Range(0, 10);
        if (random == 9)
        {
            int randomPos = Random.Range(0, 6);
            Instantiate(item, itemSpawns[randomPos].position, itemSpawns[randomPos].rotation);
        }
    }
    
    void StopPlayerMovement()
    {
        GameObject player = GameObject.Find("Player");
        player.gameObject.GetComponent<PlayerController>().speed = 0;
    }

    void ShowGameOverUI()
    {
        actualTimeText.transform.parent.gameObject.SetActive(false);
        timeEnergyText.transform.parent.gameObject.SetActive(false);

        Cursor.visible = (true);

        CheckBestTime();

        gameOverUI.SetActive(true);

        ShowTimersUI();

        gameOverUI.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        gameOverUI.transform.GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
    }

    void ShowStageCompleteUI()
    {
        actualTimeText.transform.parent.gameObject.SetActive(false);
        timeEnergyText.transform.parent.gameObject.SetActive(false);

        Cursor.visible = (true);

        CheckBestTime();

        stageCompleteUI.SetActive(true);

        ShowTimersUI();

        stageCompleteUI.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled = true;
        stageCompleteUI.transform.GetChild(1).gameObject.GetComponent<Animator>().enabled = true;
    }

    void CheckBestTime()
    {
        if (lastTimeNumber > highScore)
        {
            highScore = lastTimeNumber;
            SaveLoad.instance.SaveBestTime(level, lastTimeNumber);
        }

        highTimeText.text = highScore.ToString("F2");
    }

    void ShowTimersUI()
    {
        bestTimerUI.SetActive(true);
        lastTimerUI.SetActive(true);

        bestTimerUI.GetComponent<Animator>().enabled = true;
        lastTimerUI.GetComponent<Animator>().enabled = true;
    }

    public void AddTimeEnergy()
    {
        timeEnergy += itemEnergyValue;

        if (timeEnergy > 100)
            timeEnergy = 100;

        UpdateEnergyBarUI();
    }

    void UpdateEnergyBarUI()
    {
        timeEnergyText.text = timeEnergy.ToString("F0") + "%";
        timeEnergyBarImage.fillAmount = timeEnergy / 100;
    }
}
