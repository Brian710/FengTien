using HTC.UnityPlugin.Vive;
using UnityEngine;

public class MedsWaterController : MonoBehaviour
{
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private GameObject ChildObj;
    [SerializeField] private Quest.Name qName;
    [SerializeField] private GameObject On;    
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public InteractHover hover;
    public GlassController glass;
    public void Awake()
    {
        ChildObj.SetActive(false);
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public  void Start()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
    }
    public  void OnDestroy()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange -= OnQuestChange;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<MedsCupController>() && other.GetComponentInParent<MedsCupController>().goalType == Goal.Type.MixWater)
        {
            ChildObj.transform.position = new Vector3(0, 0, 0);
            ChildObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            glass.doFull(false);
            Debug.LogError("把水倒進藥杯");
        }
        else if (other.GetComponentInParent<TubeController>() && other.GetComponentInParent<MedsCupController>().goalType == Goal.Type.FeedMeds)
        {
            glass.doFull(false);
            Debug.LogError("把水倒進針筒");
        }
    }
    private void OnQuestChange(Quest.Name name, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                ChildObj.SetActive(false);
                glass.doFull(false);
                hover.enabled = false;
                viveGrabFunc.enabled = false;
                break;
            case Quest.State.CURRENT:
                ChildObj.SetActive(true);
                HaveMats(true);
                glass.doFull(true);
                viveGrabFunc.enabled = true;
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Quest.State.DONE:
                ChildObj.SetActive(false);
                HaveMats(false);
                glass.doFull(false);
                viveGrabFunc.enabled = false;
                hover.enabled = false;
                break;
        }
    }
    public void HaveMats(bool value)
    {
        if (On)
            On.SetActive(value);
    }
    public bool IfHaveMats()
    {
        return On.activeSelf;
    }
}
