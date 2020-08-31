using UnityEngine;

public class SoapController : IObjControllerBase
{
    [SerializeField]
    private ClicktoInteract ClickInteract;
    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.Soap;
    }
    public override void Start()
    {
        base.Start();
        ClickInteract.Iobj = this;
        ClickInteract.enabled = false;
    }

    protected override void SetWaitingState()
    {
        ClickInteract.gameObject.SetActive(true);
        ClickInteract.enabled = false;
        base.SetWaitingState();
    }
    protected override void SetCurrentState()
    {
        ClickInteract.enabled = true;
        base.SetCurrentState();
    }
    protected override void SetDoneState()
    {
        StartCoroutine(PlayerController.Instance.RightHand.ShowBubble());
        StartCoroutine(PlayerController.Instance.LeftHand.ShowBubble());
        ClickInteract.enabled = false;
        base.SetDoneState();
    }
}
