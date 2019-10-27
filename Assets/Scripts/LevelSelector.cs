using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour {

    public int levelIndex;

	public void LoadLevel(){

        SceneManager.LoadScene(levelIndex);
    }

   public void ExitGame(){
        Application.Quit();
   }
}
