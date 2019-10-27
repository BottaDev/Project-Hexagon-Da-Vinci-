using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour {

    //public float r;
    //public float g;
    //public float b;
    //public bool randomMode;

    [HideInInspector]public Renderer render;
    float timeLeft;
    Color targetColor;
    
    void Start(){

        render = gameObject.GetComponent<Renderer>();
    }

    void Update(){

        if (timeLeft <= Time.deltaTime){

            render.material.color = targetColor;

            //if(randomMode){
            targetColor = new Color(Random.value, Random.value, Random.value);
            //} else {
                //targetColor = new Color(r, g, b);
            //}

            timeLeft = 1f;
        } else{

            render.material.color = Color.Lerp(render.material.color, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
    }
}
