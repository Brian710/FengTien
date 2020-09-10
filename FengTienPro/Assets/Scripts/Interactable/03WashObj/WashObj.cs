using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WashObj : IObjControllerBase, IWashable, IGrabbable
{
    [SerializeField]    private bool isWashed;
    [SerializeField]    private int washTime;
    [SerializeField]    private Goal.Type type;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public int WashTime() => washTime;

    public bool IsWashed() => isWashed;
    public bool isDry;
    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }

    public override void Start()
    {
        if (viveGrabFunc == null)
            _viveGrabFunc = GetComponentInChildren<BasicGrabbable>();

        base.Start();
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public void SetWashed(bool value)
    {
        isWashed = value;
        //Show UI Completed
        if(isWashed)    PlayerController.Instance.QuestGoalCompleted();
    }
    
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        SetWashed(false);
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
        isDry = false;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
        base.SetDoneState();
    }
}
