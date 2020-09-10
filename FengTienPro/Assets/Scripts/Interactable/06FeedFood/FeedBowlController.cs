using UnityEngine;

public class FeedBowlController : IObjControllerBase
{
    [SerializeField]    private ClicktoInteract ClickInteract;
    [SerializeField]    private Transform startParent;
    [SerializeField]    private Transform targetParent;

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.TakeBowl;
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
        transform.localPosition = new Vector3(-0.007f, 0.001f, -0.015f);
        transform.localRotation = Quaternion.Euler(-34.54f, -91.30701f, 0.591f);
        PlayerController.Instance.LeftHand.HandAnimChange(HandAnim.Bowl);
        ClickInteract.enabled = false;
        base.SetDoneState();
    }
    private void OnTriggerEnter(Collider other)
    {
        SpoonController spoon = other.gameObject.GetComponent<SpoonController>();
        if (spoon)
        {
            Debug.LogError("湯匙盛稀飯");
            if (!spoon.IfHaveMat())
            {
                Debug.LogError("盛到了");
                spoon.GetMat(true);
            }
        }
    }
}
