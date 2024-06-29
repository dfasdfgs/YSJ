using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class getitem: MonoBehaviour
{
    public SpriteRenderer Img_Renderer;
    public Sprite sprites;
    SpriteRenderer spriteRenderer;

    public bool isLeft;
    SpriteRenderer player;

    Quaternion rightPos = Quaternion.Euler(0.574f, -0.125f, 1f);
    Quaternion rightPosReverse = Quaternion.Euler(-0.125f, -0.125f, 1f);



    private void Awake()
    {
        player = GetComponentInParent<SpriteRenderer>();
    }

    private void LateUpdate()
    {

        bool isReverse = player.flipX;
        if (isLeft)
        {
            transform.localRotation = isReverse ? rightPosReverse : rightPos;
            spriteRenderer.flipX = isReverse;
        }
    }

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
    private void Update()
    {
//        if (Input.GetButtonDown("Horizontal"))
//            transform.localScale = new Vector3(-1, 1, 1); 
    }
}
