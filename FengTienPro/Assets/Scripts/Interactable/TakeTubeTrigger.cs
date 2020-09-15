using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TakeTubeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Finish_Tube;
    [SerializeField] private Transform Target_Transform;
    public bool Check_Start, Check_Finish;
    private void Start()
    {
        Check_Start = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<VivePoseTracker>() && !Check_Start)
        {
            other.gameObject.SetActive(false);
            Finish_Tube.SetActive(true);
            Finish_Tube.transform.SetParent(Target_Transform, false);
            Finish_Tube.transform.localPosition = new Vector3(0, -0.025f, 0);
            Finish_Tube.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Check_Start = true;
            Debug.LogError("鼻胃管連接完成");
        }
        //else if (other.GetComponent<SuctionController>())
        //{
        //    if(Tube_Suction.Suction_Done)
        //    {
        //        QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CheckNasogastricTube);
        //        Debug.LogError("確認鼻胃管在胃中");
        //    }
        //}
    }
}
