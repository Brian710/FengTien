using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TakeTubeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Finish_Tube;
    [SerializeField] private Transform Target_Transform;
    private SuctionController Suction;
    public bool Check_Start, Check_Finish;
    private void Start()
    {
        Check_Start = false;
        Check_Finish = false;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).OnGoalStateChange += OnCheckNasogastricTubeChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange += OnFeedMedsChange;
    }
    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).OnGoalStateChange -= OnCheckNasogastricTubeChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.FeedMeds).OnGoalStateChange -= OnFeedMedsChange;
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
        else if (other.GetComponent<SuctionController>())
        {
            if(Suction.Suction_Up && Suction.Suction_Down)
            {
                QuestManager.Instance.AddQuestCurrentAmount(Goal.Type.CheckNasogastricTube);
                Debug.LogError("確認鼻胃管在胃中");
            }
        }
        else if (other.GetComponentInParent<VivePoseTracker>() && Check_Finish)
        {
            Debug.LogError("餵藥");
        }
    }
    private void OnCheckNasogastricTubeChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                Check_Finish = false;
                break;
            case Goal.State.CURRENT:
                Check_Finish = false;
                break;
            case Goal.State.DONE:
                Check_Finish = true;
                break;
        }
    }
    private void OnFeedMedsChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                break;
            case Goal.State.CURRENT:
                break;
            case Goal.State.DONE:
                break;
        }
    }
}
