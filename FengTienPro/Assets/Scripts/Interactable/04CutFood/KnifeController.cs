using HTC.UnityPlugin.Vive;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private GameObject ChildObj;
    [SerializeField] private Quest.Name qName;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    
    public  void Awake()
    {
        ChildObj.SetActive(false);
    }
    public  void Start()
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
                break;
            case Quest.State.CURRENT:
                ChildObj.SetActive(true);
                viveGrabFunc.enabled = true;
                break;
            case Quest.State.DONE:
                ChildObj.SetActive(false);
                viveGrabFunc.enabled = false;
                break;
        }
    }
}
