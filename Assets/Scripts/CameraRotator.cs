using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour {
 
	public float rotationSpeed = 50f;
    public float multipliedSpeed = 35f;

    [SerializeField]float rotationTimer;     // El tiempo que dura la rotacion

    Vector3 direction;
    float speed;

    void Start(){

        int random = Random.Range(0, 2);

        // Hardcodeo ya que al inicio del nivel, el Time.deltaTime = 0.2
        // Ese valor hace que la camara se vuelva loca

        if (random == 1){
            direction = Vector3.forward * -1;
            rotationTimer = 5;
            speed = 0.01660183f * rotationSpeed;
            //speed = Time.deltaTime * rotationSpeed;
        } else {
            direction = Vector3.forward;
            rotationTimer = 5;
            speed = 0.01660183f * rotationSpeed;
            //speed = Time.deltaTime * rotationSpeed;
        }
    }

    void Update(){

        rotationTimer -= Time.deltaTime;

        if (rotationTimer > 0){
            transform.Rotate(direction, speed);
        }else if (rotationTimer <= 0){
            SelectRotation();
        }
    }
    
    void SelectRotation(){

        int random = Random.Range(0, 5);

        switch (random){

            // Rotacion hacia la derecha
            case 1:
                direction = Vector3.forward * -1;
                rotationTimer = 5;
                speed = Time.deltaTime * rotationSpeed;
                break;

            // Rotacion hacia la izquierda
            case 2:
                direction = Vector3.forward;
                rotationTimer = 5;
                speed = Time.deltaTime * rotationSpeed;
                break;

            // Rotacion rapida y corta hacia la derecha
            case 3:
                direction = Vector3.forward * -1;
                rotationTimer = 1f;
                speed = Time.deltaTime * rotationSpeed * multipliedSpeed;
                break;

            // Rotacion rapida y corta hacia la izquierda
            case 4:
                direction = Vector3.forward;
                rotationTimer = 1f;
                speed = Time.deltaTime * rotationSpeed * multipliedSpeed;
                break;
        }
    }
}
