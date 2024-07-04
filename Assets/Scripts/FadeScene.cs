using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    public Image FadeImage;
    public Text FadeText;
    public float FadeAlpha = 1.0f;
    public GameObject FadeObject;

    // Start is called before the first frame update
    void Start()
    {
        FadeImage.gameObject.SetActive(true);
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        while (FadeAlpha >= 0)
        {
            FadeAlpha -= 0.01f;
            Debug.Log("타이머 시작");

            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeAlpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, FadeAlpha);

            yield return new WaitForSeconds(0.02f);
           

        } 
         FadeObject.SetActive(false); 
    }
}
