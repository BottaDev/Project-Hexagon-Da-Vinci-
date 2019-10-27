using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonController : MonoBehaviour {

    public Rigidbody2D rb;

    float shrinkSpeed;

    void Start(){

        SelectRotation();

        transform.localScale = Vector3.one * 11f;       // Tamaño en el mundo
	}

	void Update(){

        shrinkSpeed = GameManager.instance.hexagonSpeed;

        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

		if(transform.localScale.x <= 0.05f){

			Destroy (gameObject);
		}
	}

    void SelectRotation(){

        int random = Random.Range(0, 6);

        switch (random){

            case 0:
                rb.rotation = 0;
                break;

            case 1:
                rb.rotation = 60;
                break;

            case 2:
                rb.rotation = 120;
                break;

            case 3:
                rb.rotation = 180;
                break;

            case 4:
                rb.rotation = 240;
                break;

            case 5:
                rb.rotation = 300;
                break;
        }
    }
}
