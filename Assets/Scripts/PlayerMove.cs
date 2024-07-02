using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerMove : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpPower;
    public Transform respawnPoint; // ��Ȱ ����
    private Animator animator;

    public FadeInEffect fadeEffect; // FadeInEffect ��ũ��Ʈ ����
    private float fadeTime; // FadeInEffect�� fadeTime ������ �����ϱ� ���� ����

    private bool IsJumping;
    private Rigidbody2D PlayerRigid;
    public bool ismoving = false;

    private Vector3 initialcameraPosition;
    public Vector3[] mapCameraInitialpositions;
    private int currentMapIndex = 0;
    private SpriteRenderer spriterenderer;

    public RuntimeAnimatorController RAC2;
    public Animator nn;
    public AudioSource moveaudio;

    public static bool Getitem { get; private set; }  // ������ ȹ�� ����
    public static bool Finish { get; private set; }   // ��ǥ ���� ����

    private void Awake()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        IsJumping = false;
        Getitem = false;  // �ʱ�ȭ
        Finish = false;   // �ʱ�ȭ
    }

    private void Start()
    {
        nn = GetComponent<Animator>();

        initialcameraPosition = transform.position;

        mapCameraInitialpositions = new Vector3[]
        {
            new Vector3(0,0,-10),
            new Vector3(10,5,-10),
        };
        // FadeInEffect���� fadeTime ���� ��������
        fadeTime = fadeEffect.fadeTime;

        // ��Ȱ ������ �Ҵ���� �ʾ��� ���, �÷��̾��� ���� ��ġ�� ��Ȱ �������� ����
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            animator.SetTrigger("Jump");
            PlayerRigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            IsJumping = true;
        }
        Move();
        moveSound();
    }


    private void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * MaxSpeed * Time.deltaTime);
            animator.SetBool("Move", true);
            ismoving = true;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * MaxSpeed * Time.deltaTime);
            animator.SetBool("Move", true);
            ismoving = true;
        }
        else
        {
            animator.SetBool("Move", false);
            ismoving = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            spriterenderer.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriterenderer.flipX = true;
        }
    }
    private void moveSound()
    {
        if (ismoving)
        {
            if (!moveaudio.isPlaying)
                moveaudio.Play();
        }
        else
        {
            moveaudio.Stop();
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            IsJumping = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "item")
        {
            Getitem = true;
            nn.runtimeAnimatorController = RAC2;
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Finish = true;
        }
        else if (collision.gameObject.tag == "Water")
        {
            Debug.Log("�÷��̾ ���� �����");
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        Debug.Log("���̵� �ƿ� ����");
        // ���̵� �ƿ� ����
        fadeEffect.OnFade(FadeState.FadeOut);

        // ���̵� �ƿ��� �Ϸ�� ������ ���
        yield return new WaitForSeconds(fadeTime);

        // �÷��̾� ��ġ�� ��Ȱ �������� ����
        transform.position = respawnPoint.position;
        PlayerRigid.velocity = Vector2.zero;

        if (currentMapIndex < mapCameraInitialpositions.Length)
        {
            Camera.main.transform.position = mapCameraInitialpositions[currentMapIndex];
        }
        else
        {
            Debug.LogError("�� �ε����� �ʱ�ȭ�� ī�޶� ��ġ �迭�� ������ ����ϴ�.");
        }

        Debug.Log("���̵� �� ����");
        // ���̵� �� ����
        fadeEffect.OnFade(FadeState.FadeIn);
    }
}



