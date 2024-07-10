using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Threading;

public class PlayerMove : MonoBehaviour
{
    public string stage;
    public GameManager GameManager;

    public float MaxSpeed;
    public float JumpPower;
    public Transform respawnPoint; // 부활 지점
    private Animator animator;

    public FadeInEffect fadeEffect; // FadeInEffect 스크립트 참조
    private float fadeTime; // FadeInEffect의 fadeTime 변수를 저장하기 위한 변수

    private bool IsJumping;
    private Rigidbody2D PlayerRigid;
    public bool ismoving = false;

    public Vector3[] mapCameraInitialpositions;
    private SpriteRenderer spriterenderer;

    public RuntimeAnimatorController RAC2;
    public Animator nn;
    public AudioSource moveaudio;
    public GameObject itemflip;

    public static bool Getitem { get; private set; }  // 아이템 획득 상태
    public static bool Finish { get; private set; }   // 목표 도착 상태

    private void Awake()
    {
        PlayerRigid = GetComponent<Rigidbody2D>();
        IsJumping = false;
        Getitem = false;  // 초기화
        Finish = false;   // 초기화
    }

    private void Start()
    {
        nn = GetComponent<Animator>();


        // FadeInEffect에서 fadeTime 변수 가져오기
        fadeTime = fadeEffect.fadeTime;

        // 부활 지점이 할당되지 않았을 경우, 플레이어의 현재 위치를 부활 지점으로 설정
        if (respawnPoint == null)
        {
            respawnPoint = transform;
        }
        animator = GetComponent<Animator>();
        spriterenderer = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        Jump();
        Move();
        moveSound();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !IsJumping)
        {
            animator.SetTrigger("Jump");
            PlayerRigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            IsJumping = true;
            moveaudio.mute = true;
        }
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
            ItemSet();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            spriterenderer.flipX = true;
            ItemGet();
        }
    }
    private void moveSound()
    {
        if (ismoving)
        {
            if (!moveaudio.isPlaying)
            {
                moveaudio.Play();
            }
        }
        else
        {
            moveaudio.Stop();
        }
    }

    private void ItemGet()
    {
        itemflip.transform.localPosition = new Vector3(-0.57f, -0.125f, 1f);
    }
    private void ItemSet()
    {
        itemflip.transform.localPosition = new Vector3(0.574f, -0.125f, 1f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {

            IsJumping = false;
            moveaudio.mute = false;
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
            Debug.Log("플레이어가 물에 닿았음");
            StartCoroutine(HandlePlayerDeath());
        }
        else if (collision.gameObject.tag == "monster")
        {
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        Debug.Log("페이드 아웃 시작");
        // 페이드 아웃 시작
        fadeEffect.OnFade(FadeState.FadeOut);

        // 페이드 아웃이 완료될 때까지 대기
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(stage);

        Debug.Log("페이드 인 시작");
        // 페이드 인 시작
        fadeEffect.OnFade(FadeState.FadeIn);
    }
}



