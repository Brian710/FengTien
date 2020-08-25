using HTC.UnityPlugin.ColliderEvent;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class IObjControllerBase : MonoBehaviour
{
    #region properties
    public Goal.Type goalType { get; protected set; }
    public InteractHover hover;
    public GameObject ChildObj;
    protected bool isWaitState;

    private string takeSound;
    private string dropSound;
    private string interactSound;
    #endregion
    public virtual void Awake()
    {
        hover.InteractColor = new Color(0, .74f, .74f, 1);
        hover.hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public virtual void Start()
    {
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange += OnGoalStateChange;
        SetInterObjActive(false);
    }
    public virtual void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(goalType).OnGoalStateChange -= OnGoalStateChange;
    }
    public virtual void SetInterObjActive(bool value)
    {
        ChildObj.SetActive(value);
        if (!isWaitState && value)
            SetWaitingState();
    }
    public virtual void InteractInvoke(bool value)
    {
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        hover.ShowHintColor(false);
    }

    private void OnGoalStateChange(Goal.Type type, Goal.State state)
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
        isWaitState = true;
    }
    protected virtual void SetCurrentState()
    {
        isWaitState = false;
    }
    protected virtual void SetDoneState()
    {
        isWaitState = false;
    }

    #region Default Func
    public void ShowError()
    {
        hover.outline.enabled = true;
        StartCoroutine(ShowErrorCoro());
    }

    private IEnumerator ShowErrorCoro()
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
    #endregion
}
