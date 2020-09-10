using UnityEngine;

public class ManClothController : IObjControllerBase
{
    [SerializeField]    private ClicktoInteract ClickInteract;
    [SerializeField]    private Transform startParent;
    [SerializeField]    private Transform targetParent;
    [SerializeField]    private GameObject MouthTrigger;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.PutOnBib;
        MouthTrigger.SetActive(false);
    }
    public override void Start()
    {
        ClickInteract.IObj = this;
        base.Start();
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        MouthTrigger.SetActive(false);
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        transform.SetParent(startParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        ClickInteract.enabled = true;
        MouthTrigger.SetActive(false);
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        transform.SetParent(targetParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        ClickInteract.enabled = false;
        MouthTrigger.SetActive(true);
        base.SetDoneState();
    }
}
