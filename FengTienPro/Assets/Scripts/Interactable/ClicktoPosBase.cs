using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoPosBase : InteracObjBase
    ,IColliderEventClickHandler
    ,IColliderEventPressEnterHandler
    ,IColliderEventPressExitHandler
{
    public GameObject moveObj;
    public Transform targetPos;
    [SerializeField]
    private ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;

    protected Vector3 ObjOriginPosition;
    protected Quaternion ObjOriginRotation;
    private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

    public ColliderButtonEventData.InputButton activeButton { get { return m_activeButton; } set { m_activeButton = value; } }

    public virtual void GotoTargetPos()
    {
        if (moveObj == null || targetPos == null)
            return;
        moveObj.transform.SetParent(targetPos, false);
        moveObj.transform.localPosition = Vector3.zero;
        moveObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void OnColliderEventClick(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
        {
            GotoTargetPos();
            base.InteractInvoke(true);
        }
    }

    public void OnColliderEventPressEnter(ColliderButtonEventData eventData)
    {
        if (eventData.button == m_activeButton && eventData.clickingHandlers.Contains(gameObject) && pressingEvents.Add(eventData) && pressingEvents.Count == 1)
        {
        }
    }

    public void OnColliderEventPressExit(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Remove(eventData) && pressingEvents.Count == 0)
        {
        }
    }
}