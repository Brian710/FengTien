using UnityEngine;

public class SpoonController : IObjControllerBase
{
    [SerializeField]    private ClicktoInteract ClickInteract;
    [SerializeField]    private Transform startParent;
    [SerializeField]    private Transform targetParent;
    [SerializeField]    private GameObject On;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.TakeSpoon;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.IObj = this;
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        transform.SetParent(startParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        ClickInteract.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        transform.SetParent(targetParent, false);
        ChildObj.transform.localPosition = new Vector3(-0.00149f, 0.0306f, 0.0397000f);
        ChildObj.transform.localRotation = Quaternion.Euler(20.53f, 180f, 0);
        PlayerController.Instance.RightHand.HandAnimChange(HandAnim.Spoon);
        PlayerController.Instance.EnableRightRay = true;
        ClickInteract.enabled = false;
        base.SetDoneState();
    }

    
    public bool IfHaveMat()
    {
        return On.activeSelf;
    }

    public void GetMat(bool value)
    {
        On.SetActive(value);
    }

}
