using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WasteObj : IObjControllerBase, IGrabbable
{
    [SerializeField] private GameObject Waste;

    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.ThrowWaste;
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
    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        viveGrabFunc.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        viveGrabFunc.enabled = false;
        Waste.SetActive(false);
        base.SetDoneState();
    }
}
