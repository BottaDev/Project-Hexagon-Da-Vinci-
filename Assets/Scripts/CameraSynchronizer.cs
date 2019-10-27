using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSynchronizer : MonoBehaviour {

    public AudioSource audioSource;

    public float timeShaking;
    public float timeResting;
    public float startShaking;

    CameraShake cameraShake;
    float musicTime;

    // Cancion 1 = 160 BPM / 0,375 beat

    void Start(){

        cameraShake = GetComponent<CameraShake>();
    }

    void Update(){

        musicTime = audioSource.time;

        if(musicTime >= startShaking && musicTime < startShaking + 0.5f){

            StartCoroutine(TempoShake());
        }
    }

    IEnumerator TempoShake(){
        
        while(true){
          
            StartCoroutine(cameraShake.Shake(timeShaking));

            yield return new WaitForSeconds(timeResting);
        }
    }
}
