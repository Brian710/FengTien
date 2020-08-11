using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using UnityEngine;
using MinYanGame.Core;

public class ClicktoPosBase : InteractableObjBase
    ,IColliderEventClickHandler
    ,IColliderEventPressEnterHandler
    ,IColliderEventPressExitHandler
{
    public GameObject moveObj;
    public Transform targetPos;
    [SerializeField]
    private ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;

    private Vector3 ObjOriginPosition;
    private Quaternion ObjOriginRotation;
    private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

    public ColliderButtonEventData.InputButton activeButton { get { return m_activeButton; } set { m_activeButton = value; } }

    public void GotoTargetPos()
    {
        if (moveObj && targetPos)
        {
            moveObj.transform.position = targetPos.position;
            moveObj.transform.rotation = targetPos.rotation;
            moveObj.transform.SetParent(targetPos);
        }
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