using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorMenu : MonoBehaviour {

    [HideInInspector] public Renderer render;
    Color targetColor;

    void Start(){

        render = gameObject.GetComponent<Renderer>();
    }

    public void SetMenuColorGreen(){

        targetColor = new Color(0, 1, 0);
        render.material.color = targetColor;
    }

    public void SetMenuColorRed(){

        targetColor = new Color(1, 0, 0);
        render.material.color = targetColor;
    }

    public void SetMenuColorWhite(){

        targetColor = new Color(1, 1, 1);
        render.material.color = targetColor;
    }
}
