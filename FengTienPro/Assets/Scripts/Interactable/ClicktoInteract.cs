using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoInteract :MonoBehaviour  ,IColliderEventClickHandler   , IColliderEventPressEnterHandler ,IColliderEventPressExitHandler
{
    public IObjControllerBase Iobj { get; set; }
    [SerializeField]
    private ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;

    private HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

    public ColliderButtonEventData.InputButton activeButton { get { return m_activeButton; } set { m_activeButton = value; } }

    public virtual void OnColliderEventClick(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
        {
            QuestManager.Instance.AddQuestCurrentAmount(Iobj.goalType);
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
