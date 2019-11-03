using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    [HideInInspector]public Renderer render;
    float timeLeft;
    Color targetColor;
    
    void Start(){

        render = gameObject.GetComponent<Renderer>();
    }

    void Update(){

        if (timeLeft <= Time.deltaTime){

            render.material.color = targetColor;

            targetColor = new Color(Random.value, Random.value, Random.value);

            timeLeft = 1f;
        } else{

            render.material.color = Color.Lerp(render.material.color, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
    }
}
