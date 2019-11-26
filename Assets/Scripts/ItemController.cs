using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Sprite[] sprites;
    public float speed = 1.2f;

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
        if (transform.position == Vector3.zero)
            Destroy(gameObject);

        ChangeSprite();

        float realSpeed = GameManager.instance.hexagonSpeed * speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, center.position, realSpeed);
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
        if (collision.gameObject.layer == 8)
        {
            GameManager.instance.AddTimeEnergy();

            Destroy(gameObject);
        }
    }
}
