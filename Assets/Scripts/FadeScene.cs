using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    public Image FadeImage;
    public Text FadeText;
    private float FadeAlpha = 1.0f;
    private AudioSource backGroundAudio;

    // Start is called before the first frame update
    void Start()
    {
        backGroundAudio = GetComponent<AudioSource>();
        FadeImage.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }
    
    private IEnumerator Fade()
    {
        while(FadeAlpha >= 0)
        {
            FadeAlpha -= 0.03f;

            FadeImage.color = new Color(FadeImage.color.r,FadeImage.color.g,FadeImage.color.b, FadeAlpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, FadeAlpha);

            yield return new WaitForSeconds(0.02f);
        }
        backGroundAudio.Play();
    }
}