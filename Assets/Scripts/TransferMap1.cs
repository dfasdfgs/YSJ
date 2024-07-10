using UnityEngine;

public class TransferMap1 : MonoBehaviour
{
    public Transform tta;
    public Transform ttarget;
    public Transform a;
    public Transform arget;


    public GameObject tthePlayer;
    public GameObject ttheCamera;
    public bool rhdqhdlqps = true;



    // 박스 콜라이더에 닿는 순간 이벤트 발생
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "Player")
        {
            if(rhdqhdlqps == true)
            {
                ttheCamera.transform.position = a.transform.position;
                tthePlayer.transform.position = arget.transform.position;
                rhdqhdlqps = false;
            }
            else
            {
                ttheCamera.transform.position = tta.transform.position;
                tthePlayer.transform.position = ttarget.transform.position;
            }
        }
    }


}
