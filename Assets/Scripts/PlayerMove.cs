using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerMove : MonoBehaviour
{
    public float MaxSpeed;
    public float JumpPower;
    public Transform respawnPoint; // 부활 지점
    private Animator animator;

    public FadeInEffect fadeEffect; // FadeInEffect 스크립트 참조
    private float fadeTime; // FadeInEffect의 fadeTime 변수를 저장하기 위한 변수

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

        initialcameraPosition = transform.position;

        mapCameraInitialpositions = new Vector3[]
        {
            new Vector3(0,0,-10),
            new Vector3(10,5,-10),
        };
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
            Debug.Log("플레이어가 물에 닿았음");
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

        // 플레이어 위치를 부활 지점으로 설정
        transform.position = respawnPoint.position;
        PlayerRigid.velocity = Vector2.zero;

        if (currentMapIndex < mapCameraInitialpositions.Length)
        {
            Camera.main.transform.position = mapCameraInitialpositions[currentMapIndex];
        }
        else
        {
            Debug.LogError("맵 인덱스가 초기화된 카메라 위치 배열의 범위를 벗어납니다.");
        }

        Debug.Log("페이드 인 시작");
        // 페이드 인 시작
        fadeEffect.OnFade(FadeState.FadeIn);
    }
}



