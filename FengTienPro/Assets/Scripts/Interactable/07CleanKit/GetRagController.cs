using UnityEngine;

public class GetRagController : IObjControllerBase
{
    [SerializeField] private ClicktoInteract ClickInteract;
    [SerializeField] private GameObject GetRag;
    [SerializeField] private Goal.Type type;
    [SerializeField] private Transform startParent;
    [SerializeField] private Transform targetParent;
    private bool ToWipe;
    public override void Awake()
    {
        base.Awake();
        ToWipe = false;
        ChildObj.SetActive(false);
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public override void Start()
    {
        ClickInteract.IObj = this;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CleanDesk).OnGoalStateChange += OnGoalStateChange;
        hover.enabled = false;
        base.Start();
    }

    public  override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CleanDesk).OnGoalStateChange -= OnGoalStateChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null) 
        {
            ToWipe = true;
            if (ToWipe)
            {
                GetRag.SetActive(true);
                transform.SetParent(targetParent, false);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                ClickInteract.enabled = true;
                base.SetCurrentState();
            }
        }
    }
    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                SetWaitingState();
                break;
            case Goal.State.CURRENT:
                SetCurrentState();
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                GetRag.SetActive(false);
                break;
        }
    }
}
