using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        FadeObject.SetActive(true);
        StartCoroutine(Fade());

        if (FadeObject == true)
        {
            Debug.Log("ƒ—¡¸"); 
        }
    }

    private IEnumerator Fade()
    {
        while (FadeAlpha >= 0)
        {
            FadeAlpha -= 0.01f;
            //Debug.Log("≈∏¿Ã∏” Ω√¿€");

            FadeImage.color = new Color(FadeImage.color.r, FadeImage.color.g, FadeImage.color.b, FadeAlpha);
            FadeText.color = new Color(FadeText.color.r, FadeText.color.g, FadeText.color.b, FadeAlpha);

            yield return new WaitForSeconds(0.02f);
           
        } 
         FadeObject.SetActive(false);
        Debug.Log("≤®¡¸");
    }
}
