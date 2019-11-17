using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyColorRenderer : MonoBehaviour {

    ChangeColor playerColor;
    LineRenderer render;

    void Start(){

        playerColor = GameObject.Find("Player").gameObject.GetComponent<ChangeColor>();        

        render = gameObject.GetComponent<LineRenderer>();

        render.startColor = playerColor.render.material.color;
        render.endColor = playerColor.render.material.color;
    }

    void Update(){

        render.startColor = playerColor.render.material.color;
        render.endColor = playerColor.render.material.color;
    }
}
