using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed = 600f;

	float movement;

	void Update()
    {	
		movement = Input.GetAxisRaw ("Horizontal");
	}

	void FixedUpdate()
    {
		transform.RotateAround (Vector3.zero, Vector3.forward, movement * Time.deltaTime * -speed);			// Si la velocidad no es negativa, el moviemiento es invertido
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Hexagon")
            GameManager.instance.playerDied = true;
    }

}
