using UnityEngine;
using HTC.UnityPlugin.ColliderEvent;

public class InteractHover : MonoBehaviour ,IColliderEventHoverEnterHandler, IColliderEventHoverExitHandler
{
    public QuickOutline outline;
    public Color InteractColor { get; set; }
    public Color hintColor { get; set; } // quick outline default hint color

    private void Awake()
    {
        outline.GetComponent<QuickOutline>();
    }

    private void OnEnable()
    {
        outline.enabled = true;
    }
    private void OnDisable()
    {
        outline.enabled = false;
    }
    public void ShowOutline(bool value, Color color)
    {
        outline.enabled = value;
        if (outline.enabled)
            outline.OutlineColor = color;
    }

    public void ShowInteractColor(bool value) => ShowOutline(value, InteractColor);
    public void ShowHintColor(bool value) => ShowOutline(value, hintColor);
    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData) => ShowInteractColor(true);

    public void OnColliderEventHoverExit(ColliderHoverEventData eventData) => ShowInteractColor(false);
}
