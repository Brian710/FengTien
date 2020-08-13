using HTC.UnityPlugin.ColliderEvent;
using UnityEngine;
using UnityEngine.Events;
public class InteractableObjBase : MonoBehaviour
        , IColliderEventHoverEnterHandler
        , IColliderEventHoverExitHandler
{
    public Goal.Type goalType;
    [SerializeField]
    protected QuickOutline outline;

    public Color InteractColor = new Color(0, .74f, .74f, 1);
    public Color hintColor = new Color(1, 0.8f, .28f, 1); // quick outline default hint color

    [SerializeField]
    public UnityEvent afteInteract;

    private void Start()
    {
        if (outline)
        {
            Debug.LogWarning("outline Start");
            outline.OutlineColor = hintColor;
            outline.enabled = false;
        }
    }

    private void OnEnable()
    {
        Set();
    }

    private void OnDisable()
    {
        
    }

    public virtual void Set()
    { 
    
    }
    
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        ShowInteractColor(true);
    }

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData)
    {
        ShowInteractColor(false);
    }

    public virtual void ShowInteractColor(bool value)
    {
        ShowOutline(value, InteractColor);
    }

    public virtual void ShowHintColor(bool value)
    {
        if (GameController.Instance.mode == MainMode.Exam)
            return;

        ShowOutline(value, hintColor);
    }

    public virtual void ShowOutline(bool value, Color color)
    {
        outline.enabled = value;
        if (outline.enabled)
            outline.OutlineColor = color;
    }

    public virtual void InteractInvoke(bool value)
    {
        QuestManager.Instance.AddQuestCurrentAmount(goalType);
        afteInteract.Invoke();
    }
}
