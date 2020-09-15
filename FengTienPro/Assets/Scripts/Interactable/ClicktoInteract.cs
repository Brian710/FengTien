using HTC.UnityPlugin.ColliderEvent;
using System.Collections.Generic;
using UnityEngine;

public class ClicktoInteract :MonoBehaviour  ,IColliderEventClickHandler   , IColliderEventPressEnterHandler ,IColliderEventPressExitHandler
{
    public IObjControllerBase IObj { get; set; }
    [SerializeField]
    protected ColliderButtonEventData.InputButton m_activeButton = ColliderButtonEventData.InputButton.Trigger;

    protected HashSet<ColliderButtonEventData> pressingEvents = new HashSet<ColliderButtonEventData>();

    public ColliderButtonEventData.InputButton activeButton { get { return m_activeButton; } set { m_activeButton = value; } }

    public virtual void OnColliderEventClick(ColliderButtonEventData eventData)
    {
        if (pressingEvents.Contains(eventData) && pressingEvents.Count == 1)
        {
            if (IObj.goalType != Goal.Type.None && QuestManager.Instance.GetQuestGoalByType(IObj.goalType).state == Goal.State.CURRENT)
                QuestManager.Instance.AddQuestCurrentAmount(IObj.goalType);
            else
            {
                QuestManager.Instance.MinusQuestScore(1);
                IObj.ShowError();
            }
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
