using HTC.UnityPlugin.Vive;
using System.Collections;
using UnityEngine;

public class InputMatObj : IObjControllerBase, IGrabbable
{
    [SerializeField] private GameObject On;
    [SerializeField] private Goal.Type type;
    [SerializeField] private GameObject Veg;
    [SerializeField] private GameObject Fish;
    public BasicGrabbable viveGrabFunc => _viveGrabFunc;
    public HandAnim handAnim => _handAnim;
    public override void Awake()
    {
        base.Awake();
        goalType = type;
    }

    protected override void SetWaitingState()
    {
        viveGrabFunc.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        HaveMats(true);
        viveGrabFunc.enabled = true;
        Veg.SetActive(false);
        Fish.SetActive(false);

    }
    protected override void SetDoneState()
    {
        HaveMats(false);
        viveGrabFunc.enabled = false;
        ChildObj.transform.position = position;
        ChildObj.transform.rotation = rotation;
    }

    public void HaveMats(bool value)
    {
        if(On)
            On.SetActive(value);
    }

    public bool IfHaveMats()
    {
        return On.activeSelf;
    }
}
