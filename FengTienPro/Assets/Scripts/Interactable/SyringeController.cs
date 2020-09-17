using HTC.UnityPlugin.Vive;
using UnityEngine;

public class SyringeController : MonoBehaviour
{
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private GameObject ChildObj;
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
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).OnGoalStateChange += OnCheckNasogastricTubeChange;
    }

    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.CheckNasogastricTube).OnGoalStateChange -= OnCheckNasogastricTubeChange;
    }
    
    protected void OnCheckNasogastricTubeChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                ChildObj.SetActive(false);
                viveGrabFunc.enabled = false;
                hover.enabled = false;
                break;
            case Goal.State.CURRENT:
                ChildObj.SetActive(true);
                viveGrabFunc.enabled = true;
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                ChildObj.SetActive(false);
                viveGrabFunc.enabled = false;
                hover.enabled = false;
                break;
        }
    }
}
