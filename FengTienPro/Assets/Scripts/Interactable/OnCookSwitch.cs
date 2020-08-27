using HTC.UnityPlugin.ColliderEvent;
using System.Collections;
using UnityEngine;

public class OnCookSwitch : MonoBehaviour
  , IColliderEventHoverEnterHandler
{
    public CookPotController potController;
    public bool CookEnabled = false;

    public Renderer render;

    private bool m_gravityEnabled;

    public void SetCookEnabled(bool value)
    {
        // change the apperence the switch
        render.material.color = value ? Color.green : Color.red;
        potController.CookCoutdown(value);

        m_gravityEnabled = value;
    }
    private void Awake()
    {
        m_gravityEnabled = false;
    }
    private void Start()
    {
        SetCookEnabled(CookEnabled);
    }

    public void OnColliderEventHoverEnter(ColliderHoverEventData eventData)
    {
        SetCookEnabled(!m_gravityEnabled);
    }
}