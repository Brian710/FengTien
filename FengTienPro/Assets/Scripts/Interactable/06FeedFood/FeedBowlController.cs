using UnityEngine;

public class FeedBowlController : IObjControllerBase
{
    [SerializeField]
    private ClicktoInteract ClickInteract;
    [SerializeField]
    private Transform startParent;
    [SerializeField]
    private Transform targetParent;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.TakeBowl;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.Iobj = this;
    }
    protected override void SetWaitingState()
    {
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        ClickInteract.enabled = true;
        transform.SetParent(startParent, false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        transform.SetParent(targetParent, false);
        transform.localPosition = new Vector3(-0.0353f, 0.3171f, 0.0243f);
        transform.localRotation = Quaternion.Euler(0, -90, 0);
        PlayerController.Instance.LeftHand.HandAnimChange(HandAnim.Bowl);
        ClickInteract.enabled = false;
        base.SetDoneState();
    }
    private void OnTriggerEnter(Collider other)
    {
        SpoonController spoon = other.gameObject.GetComponent<SpoonController>();
        if (spoon != null && !spoon.IfHaveMat())
        {
            spoon.GetMat(true);
        }
    }
}
