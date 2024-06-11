using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Diagnostics;

public enum FadeState { FadeIn = 0, FadeOut, FadeInOut, FadeLoop }
public class FadeInEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    public float fadeTime; // fadeSpeed ���� 10�̸� 1�� (���� Ŭ���� ����)

    [SerializeField]
    private AnimationCurve fadeCurve; // ���̵� ȿ���� ����Ǵ� ���� ���� ��� ������ ����
    private Image image;  //���̵� ȿ���� ���Ǵ� ���� ���� �̹���
    private FadeState fadeState; // ���̵� ȿ�� ����

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnFade(FadeState State)
    {
        fadeState = State;

        switch (fadeState)
        {
            case FadeState.FadeIn: // Fade In. ����� ���İ��� 1���� 0���� (ȭ���� ���� �������)
                StartCoroutine(Fade(1, 0));
                break;
            case FadeState.FadeOut: //Fade Out. ����� ���İ��� 0����1�� (ȭ���� ���� ��ο�����)
                StartCoroutine(Fade(0, 1));
                break;
            case FadeState.FadeInOut: //Fade ȿ���� In -> Out 1ȸ �ݺ��Ѵ�
            case FadeState.FadeLoop: //Fade ȿ���� In -> Out ���� �ݺ��Ѵ�
                StartCoroutine(FadeInOut());
                break;
        }
    }

    private IEnumerator FadeInOut()
    {
        while (true)
        {
            // �ڷ�ƾ ���ο��� �ڷ�ƾ �Լ��� ȣ���ϸ� �ش� �ڷ�ƾ �Լ��� ����Ǿ�� ���� ���� ����
            yield return StartCoroutine(Fade(1, 0));  // Fade In

            yield return StartCoroutine(Fade(0, 1));  //Fade Out

            //1ȸ�� ����ϴ� ������ �� break;
            if (fadeState == FadeState.FadeInOut)
            {
                break;
            }
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            // fadeTime���� ����� fadeTime �ð� ����
            // percent ���� 0���� 1�� �����ϵ��� ��
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // ���İ��� start���� end���� fadeTime �ð� ���� ��ȭ��Ų��
            Color color = image.color;
            //color.a = Mathf.Lerp(start,end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;

            yield return null;
        }
    }

}
