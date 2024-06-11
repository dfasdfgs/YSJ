using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaterDeath : MonoBehaviour
{
    public Transform respawnPoint; // ��Ȱ ����
    public FadeInEffect fadeEffect; // ���̵� ȿ��

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾� ���� ó��
            PlayerDie(other.gameObject);
        }
    }

    void PlayerDie(GameObject player)
    {
        // �÷��̾� ��ġ �ʱ�ȭ
        player.transform.position = respawnPoint.position;
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        // ���̵� �ƿ� ȿ�� ����
        if (fadeEffect != null)
        {
            StartCoroutine(HandlePlayerRespawn());
            SceneLoad();
        }
        
    }

    private IEnumerator HandlePlayerRespawn()
    {
        // ���̵� �ƿ� ȿ�� ����
        fadeEffect.OnFade(FadeState.FadeOut);

        // ���̵� �ƿ��� �Ϸ�� ������ ���
        yield return new WaitForSeconds(fadeEffect.fadeTime);

        // �÷��̾ ��Ȱ�� �� ���̵� �� ȿ�� ����
        if (fadeEffect != null)
        {
            // ���̵� �� ȿ�� ����
            fadeEffect.OnFade(FadeState.FadeIn);

            // ���̵� ���� �Ϸ�� ������ ���
            yield return new WaitForSeconds(fadeEffect.fadeTime);
        }
    }
            

    private static void SceneLoad()
    {
        SceneManager.LoadScene("Stage");
        Debug.Log("Stage");
    }

}

