using UnityEngine;
using System.Collections.Generic;

public class MainSceneTP : TeleportControllerBase
{
    public override void Start()
    {
        target = PlayerController.Instance.Target;
        pivot = PlayerController.Instance.Cam;

        if (qName_TP == Quest.Name.None || qName_TP == Quest.Name.Entrance)
        {
            foreach (Quest q in QuestManager.Instance.quests)
            {
                q.OnQuestChange += OnQuestChange;
            }
        }
        else
        {
            QuestManager.Instance.GetQuestByName(qName_TP).OnQuestChange += OnQuestChange;
        }

        OnQuestChange(QuestManager.Instance.currentQuest.qName, QuestManager.Instance.currentQuest.state);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();

        if (qName_TP == Quest.Name.None || qName_TP == Quest.Name.Entrance)
        {
            foreach (Quest q in QuestManager.Instance.quests)
            {
                q.OnQuestChange -= OnQuestChange;
            }
        }
        else
        { 
            QuestManager.Instance.GetQuestByName(qName_TP).OnQuestChange -= OnQuestChange;
        }
    }

    private void OnQuestChange(Quest.Name qName, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                ShowTeleport(false);
                break;
            case Quest.State.CHOOSABLE:
                if (qName_TP == qName ||qName_TP == Quest.Name.None || qName_TP == Quest.Name.Entrance)
                {
                    Debug.LogWarning(qName_TP.ToString() + "is choosable");
                    ShowTeleport(true);
                }
                break;
            case Quest.State.CURRENT:
                if (qName_TP == Quest.Name.None || qName_TP == Quest.Name.Entrance)
                    ShowTeleport(false);
                break;
            case Quest.State.DONE:
                if (qName_TP == qName)
                    ShowTeleport(false);
                break;
        }
    }
}
