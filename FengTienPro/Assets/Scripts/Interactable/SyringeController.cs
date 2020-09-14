using HTC.UnityPlugin.Vive;
using UnityEngine;

public class SyringeController : MonoBehaviour
{
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private GameObject ChildObj;
    [SerializeField] private Quest.Name qName;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public InteractHover hover;
    public void Awake()
    {
        ChildObj.SetActive(false);
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public void Start()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
    }

    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
    }
    private void OnQuestChange(Quest.Name name, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                ChildObj.SetActive(false);
                viveGrabFunc.enabled = false;
                hover.enabled = false;
                break;
            case Quest.State.CURRENT:
                ChildObj.SetActive(true);
                viveGrabFunc.enabled = true;
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Quest.State.DONE:
                ChildObj.SetActive(false);
                viveGrabFunc.enabled = false;
                hover.enabled = false;
                break;
        }
    }
}
