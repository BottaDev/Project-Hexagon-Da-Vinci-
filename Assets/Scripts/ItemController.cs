using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Transform center;

    private void Start()
    {
        center = GameObject.Find("Center").transform;    
    }

    void Update()
    {
        float speed = GameManager.instance.hexagonSpeed * 1.2f * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, center.position, speed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.SlowTime();

            Destroy(gameObject);
        }

        if (collision.gameObject.name == "Center")
            Destroy(gameObject);
            
    }
}
