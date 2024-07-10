using UnityEngine;

public class getitem: MonoBehaviour
{
    public SpriteRenderer Img_Renderer;
    public Sprite sprites;
    SpriteRenderer spriteRenderer;

    public GameObject Object;


    private void Awake()
    {

    }

    private void LateUpdate()
    {


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
