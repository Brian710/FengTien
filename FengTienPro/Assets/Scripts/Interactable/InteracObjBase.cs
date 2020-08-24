using HTC.UnityPlugin.ColliderEvent;
using HTC.UnityPlugin.Vive;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class InteracObjBase : MonoBehaviour , IColliderEventHoverEnterHandler, IColliderEventHoverExitHandler
{
    public Goal.Type goalType { get; private set; }
    public QuickOutline outline { get; private set; }

    [SerializeField]
    private Color InteractColor;
    [SerializeField]
    public Color hintColor; // quick outline default hint color

    private string takeSound;
    private string dropSound;
    private string interactSound;

    public UnityEvent afteInteract;
    public virtual void Awake()
    {
        outline = GetComponent<QuickOutline>();
        if (outline == null) 
        {
            outline = gameObject.AddComponent(typeof(QuickOutline)) as QuickOutline;
        }
        InteractColor = new Color(0, .74f, .74f, 1);
        hintColor = new Color(1, 0.8f, .28f, 1);
    }
    public virtual void Start()
    {
        outline.OutlineColor = hintColor;
        outline.enabled = false;
        QuestManager.Instance.CurrentQuest.GetCurrentGoal().OnGoalStateChange += OnGoalStateChange;
    }
    public virtual void OnDestroy()
    {
        QuestManager.Instance.CurrentQuest.GetCurrentGoal().OnGoalStateChange -= OnGoalStateChange;
    }

    public virtual void InteractInvoke(bool value)
    {
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        ShowHintColor(false);
        afteInteract.Invoke();
    }

    public virtual void OnGoalStateChange(Goal.Type type, Goal.State state)
    { 
    }

    #region Default Func
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData) => ShowInteractColor(true);

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData) => ShowInteractColor(false);

    public void ShowInteractColor(bool value) => ShowOutline(value, InteractColor);

    public void ShowHintColor(bool value) => ShowOutline(value, hintColor);

    public void ShowOutline(bool value, Color color)
    {
        outline.enabled = value;
        if (outline.enabled)
            outline.OutlineColor = color;
    }

    public void ShowError()
    {
        outline.enabled = true;
        StartCoroutine(ShowErrorCoro());
    }

    private IEnumerator ShowErrorCoro()
    {
        outline.OutlineMode = QuickOutline.Mode.OutlineAndSilhouette;
        int i = 0;
        while(i<3)
        {
            outline.OutlineColor = Color.red;
            yield return new WaitForSeconds(.3f);
            outline.OutlineColor = new Color(0,0,0,0);
            yield return new WaitForSeconds(.3f);
            i++;
        }
        outline.OutlineMode = QuickOutline.Mode.OutlineAll;
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
