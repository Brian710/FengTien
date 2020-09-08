using HTC.UnityPlugin.Vive;
using UnityEngine;

public class WasteObj : IObjControllerBase, IGrabbable
{
    [SerializeField] private BasicGrabbable _viveGrabFunc;
    [SerializeField] private HandAnim _handAnim;
    [SerializeField] private GameObject Waste;

    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public new HandAnim handAnim => _handAnim;
    public GameObject Obj() => gameObject;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.ThrowWaste;
    }
    public override void Start()
    {
        base.Start();
    }
    protected override void SetDoneState()
    {
        base.SetDoneState();
        Waste.SetActive(false);
    }
}
