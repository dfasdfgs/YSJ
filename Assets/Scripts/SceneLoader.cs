using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string stage;
    public FadeInEffect fadeEffect;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "pickupitem")
        {

            SceneLoad();

        }
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene(stage);
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

}
