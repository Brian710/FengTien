using HTC.UnityPlugin.Vive;
using UnityEngine;

public class PestleController : IObjControllerBase, IGrabbable
{
    [SerializeField]    private HandAnim _handAnim;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.GrindMeds;
    }
    protected override void SetWaitingState()
    {
<<<<<<< Updated upstream
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
        hover.enabled = false;
        //GetComponent<Collider>().enabled = false;
        OnQuestChange(QuestManager.Instance.currentQuest.qName, QuestManager.Instance.currentQuest.state);
=======
        viveGrabFunc.enabled = false;
        base.SetWaitingState();

>>>>>>> Stashed changes
    }

    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
    }

    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
        ChildObj.transform.position = position;
        ChildObj.transform.rotation = rotation;
    }
}
