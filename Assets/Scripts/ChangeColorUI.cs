using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorUI : MonoBehaviour
{
    public Image image;
    float timeLeft;
    Color targetColor;

    void Update()
    {
        if (timeLeft <= Time.deltaTime)
        {

            image.color = targetColor;

            targetColor = new Color(Random.value, Random.value, Random.value);

            timeLeft = 1f;
        }
        else
        {

            image.color = Color.Lerp(image.color, targetColor, Time.deltaTime / timeLeft);
            timeLeft -= Time.deltaTime;
        }
    }
}
