using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoad : MonoBehaviour {

    public static SaveLoad instance = null;

    public float highScore1;    // Nivel 1
    public float highScore2;    // Nivel 2
    public float highScore3;    // Nivel 3

    public Text scoreTime_1;
    public Text scoreTime_2;
    public Text scoreTime_3;

    void Awake(){

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start(){

        LoadBestTime();
    }
    
    public void SaveBestTime(int level, float time){

        switch (level){

            case 1:
                highScore1 = time;
                PlayerPrefs.SetFloat("HighScore 1", highScore1);
                break;

            case 2:
                highScore2 = time;
                PlayerPrefs.SetFloat("HighScore 2", highScore2);
                break;

            case 3:
                highScore3 = time;
                PlayerPrefs.SetFloat("HighScore 3", highScore3);
                break;
        }

        PlayerPrefs.Save();

        print("Guardo los Times");
    }

    void LoadBestTime(){

        highScore1 = PlayerPrefs.GetFloat("HighScore 1");
        highScore2 = PlayerPrefs.GetFloat("HighScore 2");
        highScore3 = PlayerPrefs.GetFloat("HighScore 3");
    }

    public void ResetTimes(){

        PlayerPrefs.DeleteAll();

        // Por razones que desconosco, los tiempos no se resetean si cargo los tiempos de vuelta
        SaveBestTime(1, 0f);
        SaveBestTime(2, 0f);
        SaveBestTime(3, 0f);
    }

    public void SetScoreText(){

        LoadBestTime();

        scoreTime_1.text = highScore1.ToString("F2");
        scoreTime_2.text = highScore2.ToString("F2");
        scoreTime_3.text = highScore3.ToString("F2");
    }
}
