using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Sprite[] sprites;

    float changeTime = 0.2f;
    Transform center;
    SpriteRenderer spriteRenderer;
    int i = 0;

    private void Start()
    {
        center = GameObject.Find("Center").transform;    
        spriteRenderer = GetComponent<SpriteRenderer>() ;
    }

    void Update()
    {
        ChangeSprite();

        float speed = GameManager.instance.hexagonSpeed * 1.2f * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, center.position, speed);
        /*
        if (transform.position.x =)
            Destroy(gameObject);*/
    }

    void ChangeSprite()
    {
        changeTime -= Time.deltaTime;
        if (changeTime <= 0)
        {
            if (i == sprites.Length)
                i = 0;

            changeTime = 0.2f;
            spriteRenderer.sprite = sprites[i];
            i++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //GameManager.instance.SlowGameSpeed();
            GameManager.instance.AddTimeEnergy();

            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Center")
        {
            print("Entro if");
            Destroy(gameObject);
        }
    }
}
