using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CamShake : MonoBehaviour
{
    public Image BImage;
    float timer;
    int waitingTime;
    float ttimer;
    int wwaitingTime;
    float tttimer;
    int wwwaitingTime;
    [SerializeField]
    private float m_roughness;      //거칠기 정도
    [SerializeField]
    private float m_magnitude;      //움직임 범위

    void Start()
    {
        timer = 0.0f;
        waitingTime = 3;
        ttimer = 0.0f;
        wwaitingTime = 17;
        tttimer = 0.0f;
        wwwaitingTime = 19;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            StartCoroutine(Shake(6f));
        }
        ttimer += Time.deltaTime;
        if (ttimer > wwaitingTime)
        {
            BImage.gameObject.SetActive(true);
        }
        tttimer += Time.deltaTime;
        if (tttimer > wwwaitingTime)
        {
            SceneManager.LoadScene("sin");
        }
    }

    IEnumerator Shake(float duration)
    {
        float halfDuration = duration / 2;
        float elapsed = 0f;
        float tick = Random.Range(-10f, 10f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime / halfDuration;

            tick += Time.deltaTime * m_roughness;
            transform.position = new Vector3(
                Mathf.PerlinNoise(tick, 0) - .5f,
                Mathf.PerlinNoise(0, tick) - .5f,
                0f) * m_magnitude * Mathf.PingPong(elapsed, halfDuration);

            yield return null;
        }
    }
}
