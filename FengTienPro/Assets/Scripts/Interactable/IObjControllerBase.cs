using HTC.UnityPlugin.Vive;
using System.Collections;
using UnityEngine;
public class IObjControllerBase : MonoBehaviour
{
    #region properties
    public Goal.Type goalType { get; protected set; }
    public InteractHover hover;
    public GameObject ChildObj;
    protected bool isWaitState;
    
    [SerializeField]    private string takeSound;
    [SerializeField]    private string dropSound;
    [SerializeField]    private string interactSound;

    protected Vector3 position;
    protected Quaternion rotation;
    [SerializeField]    protected BasicGrabbable _viveGrabFunc;
    [SerializeField]    protected  HandAnim _handAnim;
    #endregion
    public virtual void Awake()
    {
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
        position = ChildObj.transform.position;
        rotation = ChildObj.transform.rotation;
    }
    public virtual void Start()
    {
        if(goalType != Goal.Type.None)
            QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange += OnGoalStateChange;

        if (_viveGrabFunc && _handAnim != HandAnim.Normal)
        {
            _viveGrabFunc.afterGrabberGrabbed += GrabFunc_afterGrabberGrabbed;
            _viveGrabFunc.beforeGrabberReleased += GrabFunc_beforeGrabberReleased;
        }
        SetChildObjActive(false);
    }
    public virtual void OnDestroy()
    {
        if (goalType != Goal.Type.None)
            QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange -= OnGoalStateChange;

        if (_viveGrabFunc && _handAnim != HandAnim.Normal)
        {
            _viveGrabFunc.afterGrabberGrabbed -= GrabFunc_afterGrabberGrabbed;
            _viveGrabFunc.beforeGrabberReleased -= GrabFunc_beforeGrabberReleased;
        }
    }
    public virtual void SetChildObjActive(bool value)
    {
        ChildObj.SetActive(value);
        if (value)
            SetWaitingState();
    }

    protected void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        if (type != goalType)
            return;

        switch (state)
        {
            case Goal.State.WAITING:
                SetWaitingState();
                hover.enabled =false;
                break;
            case Goal.State.CURRENT:
                SetCurrentState();
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                SetDoneState();
                hover.enabled = false;
                break;
        }
    }
    protected virtual void SetWaitingState()
    {
        ChildObj.transform.position = position;
        ChildObj.transform.rotation = rotation;
    }
    protected virtual void SetCurrentState()
    {
    }
    protected virtual void SetDoneState()
    {
    }

    #region Default Func
    public void ShowError()
    {
        hover.outline.enabled = true;
        StartCoroutine(ShowErrorCoro());
    }

    protected IEnumerator ShowErrorCoro()
    {
        hover.outline.OutlineMode = QuickOutline.Mode.OutlineAndSilhouette;
        int i = 0;
        while (i < 3)
        {
            hover.outline.OutlineColor = Color.red;
            yield return new WaitForSeconds(.3f);
            hover.outline.OutlineColor = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(.3f);
            i++;
        }
        hover.outline.OutlineMode = QuickOutline.Mode.OutlineAll;
    }

    public void PlayTakeSound()
    {
        if (takeSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(takeSound);
    }
    public void PlayInteractSound()
    {
        if (interactSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(interactSound);
    }
    public void PlayDropSound()
    {
        if (dropSound != "")
        {
            return;
        }
        AudioManager.Instance.Play(dropSound);
    }

    public void GrabFunc_beforeGrabberReleased()
    {
        if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            PlayerController.Instance.EnableRightRay = true;
            PlayerController.Instance.RightHand.HandAnimChange(HandAnim.Normal);
        }
        else
        {
            PlayerController.Instance.EnableLeftRay = true;
            PlayerController.Instance.LeftHand.HandAnimChange(HandAnim.Normal);
        }
    }

    public void GrabFunc_afterGrabberGrabbed()
    {
        if (ViveInput.GetPressEx(HandRole.RightHand, ControllerButton.Trigger))
        {
            PlayerController.Instance.EnableRightRay = false;
            PlayerController.Instance.RightHand.HandAnimChange(_handAnim);
        }
        else
        {
            PlayerController.Instance.EnableLeftRay = false;
            PlayerController.Instance.LeftHand.HandAnimChange(_handAnim);
        }

        PlayTakeSound();
        hover.ShowInteractColor(false);
    }
    #endregion
}
