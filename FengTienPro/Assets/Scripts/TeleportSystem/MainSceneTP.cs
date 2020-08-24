﻿public class MainSceneTP : TeleportControllerBase
{

    public override void Start()
    {
        base.Start();
        QuestManager.Instance.CurrentQuest.OnQuestChange += OnQuestChange;
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        QuestManager.Instance.CurrentQuest.OnQuestChange -= OnQuestChange;
    }

    private void OnQuestChange(Quest.Name qName, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                break;
            case Quest.State.CHOOSABLE:
                if (qName_TP == qName || qName_TP == Quest.Name.None || qName_TP == Quest.Name.Entrance)
                    ShowTeleport(true);
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
