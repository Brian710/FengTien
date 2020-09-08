using UnityEngine;

public class CleanDeskController : IObjControllerBase
{
    [SerializeField] private Collider colli;
    [SerializeField] private GameObject Rag;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.CleanDesk;
    }
    public override void Start()
    {
        base.Start();
    }
    protected override void SetWaitingState()
    {
        base.SetWaitingState();
        colli.enabled = false;
    }

    protected override void SetCurrentState()
    {
        colli.enabled = true;
    }
    protected override void SetDoneState()
    {
        colli.enabled = false;
        Rag.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<GetRagController>())
        {
<<<<<<< Updated upstream
            RagIn = true;
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
=======
            if (FindObjectOfType<GetRagController>().RagInHand == true)
            {
                QuestManager.Instance.AddQuestCurrentAmount(goalType);
            }
>>>>>>> Stashed changes
        }
    }
}
