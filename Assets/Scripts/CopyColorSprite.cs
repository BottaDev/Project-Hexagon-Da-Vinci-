using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyColorSprite : MonoBehaviour {

    ChangeColor playerColor;
    Renderer render;

    void Start(){

        playerColor = GameObject.Find("Player").gameObject.GetComponent<ChangeColor>();

        render = gameObject.GetComponent<Renderer>();
    }

    void Update(){

        render.material.color = playerColor.render.material.color;
    }
}
