using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //직접 만든 class에 접근할 수 있도록 해줌. 
public class Dialogue
{
    [TextArea]//한줄 말고 여러 줄 쓸 수 있게 해줌
    public string dialogue;
}
public class dialog : MonoBehaviour
{
    [SerializeField] private Text txt_Dialogue; // 텍스트를 제어하기 위한 변수

    private bool isDialogue = false; //대화가 진행중인지 알려줄 변수
    private int count = 0; //대사가 얼마나 진행됐는지 알려줄 변수
    public GameObject NC;
    
    

    [SerializeField] private Dialogue[] dialogue;


    private void Start()
    {
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        ONOFF(true); //대화가 시작됨
        count = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
        NC.SetActive(true);
    }

    private void ONOFF(bool _flag)
    {
        txt_Dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
        NC.SetActive(false);
    }

    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].dialogue;
        count++; //다음 대사와 cg가 나오도록 
    }


    // Update is called once per frame
    void Update()
    {
        //spacebar 누를 때마다 대사가 진행되도록. 
        if (isDialogue) //활성화가 되었을 때만 대사가 진행되도록
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //대화의 끝을 알아야함.
                if (count < dialogue.Length) NextDialogue(); //다음 대사가 진행됨
                else ONOFF(false); //대사가 끝남
            }
        }

    }
}