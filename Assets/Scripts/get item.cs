using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class getitem: MonoBehaviour
{
    public SpriteRenderer Img_Renderer;
    public Sprite sprites;


    void Start()
    {
        Img_Renderer = GetComponent<SpriteRenderer>();

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            gameObject.tag = "pickupitem";

            Img_Renderer.sprite = sprites;
            Destroy(collision.gameObject);
        }
    }

}
