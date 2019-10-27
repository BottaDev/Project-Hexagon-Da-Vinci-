using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    //public float duration = 0.15f;
    [Range(0,1)]public float magnitude = 0.25f;

	public IEnumerator Shake (float duration){

        float elapsed = 0;      // Tiempo transcurrido

        while(elapsed < duration){

            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, -10);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = new Vector3(0, 0, -10);
    }
}
