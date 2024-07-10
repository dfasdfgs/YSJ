using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //���� ���� class�� ������ �� �ֵ��� ����. 
public class Dialogue
{
    [TextArea]//���� ���� ���� �� �� �� �ְ� ����
    public string dialogue;
}
public class dialog : MonoBehaviour
{
    [SerializeField] private Text txt_Dialogue; // �ؽ�Ʈ�� �����ϱ� ���� ����

    private bool isDialogue = false; //��ȭ�� ���������� �˷��� ����
    private int count = 0; //��簡 �󸶳� ����ƴ��� �˷��� ����
    public GameObject NC;
    
    

    [SerializeField] private Dialogue[] dialogue;


    private void Start()
    {
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        ONOFF(true); //��ȭ�� ���۵�
        count = 0;
        NextDialogue(); //ȣ����ڸ��� ��簡 ����� �� �ֵ��� 
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
        count++; //���� ���� cg�� �������� 
    }


    // Update is called once per frame
    void Update()
    {
        //spacebar ���� ������ ��簡 ����ǵ���. 
        if (isDialogue) //Ȱ��ȭ�� �Ǿ��� ���� ��簡 ����ǵ���
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //��ȭ�� ���� �˾ƾ���.
                if (count < dialogue.Length) NextDialogue(); //���� ��簡 �����
                else ONOFF(false); //��簡 ����
            }
        }

    }
}