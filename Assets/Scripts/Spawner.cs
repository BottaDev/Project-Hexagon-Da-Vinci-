using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Range (0,2)]
	public float spawnRate = 1f;

    public GameObject[] hexagons;

	GameObject hexagon;

    float nextTimeSpawn = 0f;

	void Update () {
        
        if (Time.time >= nextTimeSpawn && Time.timeSinceLevelLoad >= 1f){

            SelectHexagon();

            Instantiate(hexagon, Vector3.zero, Quaternion.identity);
            nextTimeSpawn = Time.time + 1 / spawnRate;
        }
	}

    void SelectHexagon(){

        int random;

        random = Random.Range(0, hexagons.Length);

        hexagon = hexagons[random];
    }
}
