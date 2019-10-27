using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyColorMenu : MonoBehaviour {

    ChangeColorMenu playerColor;
    Renderer render;

    void Start(){

        playerColor = GameObject.Find("Player").gameObject.GetComponent<ChangeColorMenu>();

        render = gameObject.GetComponent<Renderer>();
    }

    void Update(){

        render.material.color = playerColor.render.material.color;
    }
}
