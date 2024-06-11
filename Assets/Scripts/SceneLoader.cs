using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            if (fadeEffect != null)
            {
                StartCoroutine(HandlePlayerRespawn());
                SceneLoad();
            }
        }
    }

    private void SceneLoad()
    {
        SceneManager.LoadScene(stage);
    }

    private IEnumerator HandlePlayerRespawn()
    {
        // 페이드 아웃 효과 시작
        fadeEffect.OnFade(FadeState.FadeOut);

        // 페이드 아웃이 완료될 때까지 대기
        yield return new WaitForSeconds(fadeEffect.fadeTime);

        // 플레이어가 부활할 때 페이드 인 효과 적용
        if (fadeEffect != null)
        {
            // 페이드 인 효과 시작
            fadeEffect.OnFade(FadeState.FadeIn);

            // 페이드 인이 완료될 때까지 대기
            yield return new WaitForSeconds(fadeEffect.fadeTime);
        }
    }

}
