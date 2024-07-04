using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeScene : MonoBehaviour
{
    setting setting;
    public Image FadeImage;
    public Text FadeText;
    public float FadeAlpha = 1.0f;

    // Start is called before the first frame update


    private void Awake()
    {
        FadeAlpha = 1.0f;
    }


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

            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeAlpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, FadeAlpha);

            yield return new WaitForSeconds(0.03f);
           
        }
    }
}
