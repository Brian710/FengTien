using HTC.UnityPlugin.Vive;
using UnityEngine;

public class PestleController : IObjControllerBase, IGrabbable
{
    [SerializeField]    private BasicGrabbable _viveGrabFunc;
    [SerializeField]    private HandAnim _handAnim;
    [SerializeField]    private Quest.Name qName;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public override void Awake()
    {
        base.Awake();
        ChildObj.SetActive(false);
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public override void Start()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
        hover.enabled = false;
        //GetComponent<Collider>().enabled = false;
        OnQuestChange(QuestManager.Instance.currentQuest.qName, QuestManager.Instance.currentQuest.state);
    }

    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange -= OnQuestChange;
    }
    private void OnQuestChange(Quest.Name qName, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                base.SetWaitingState();
                hover.enabled = false;
                break;
            case Quest.State.CURRENT:
                ChildObj.SetActive(true);
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Quest.State.DONE:
                hover.enabled = false;
                break;
        }
    }
}
