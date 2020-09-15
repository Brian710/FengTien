using UnityEngine;

public class MainSceneTP : TeleportControllerBase
{
    [SerializeField] private Quest.Name qName_TP;

    public override void Start()
    {
        target = PlayerController.Instance.Target;
        pivot = PlayerController.Instance.Cam;

        if (qName_TP == Quest.Name.None)
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
        GetComponent<Collider>().enabled = false;
        //OnQuestChange(QuestManager.Instance.currentQuest.qName, QuestManager.Instance.currentQuest.state);
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
        if(qName == Quest.Name.Talk && state == Quest.State.CHOOSABLE && qName_TP == Quest.Name.None)
            QuestManager.Instance.lineCreator.AddList(transform, 0);
        
        switch (state)
        {
            case Quest.State.WAITING:
                ShowTeleport(false);
                break;
            case Quest.State.CHOOSABLE:
                if (qName_TP == qName ||qName_TP == Quest.Name.None)
                {
                    ShowTeleport(true);
                    QuestManager.Instance.lineCreator.AddList(transform);
                    QuestManager.Instance.lineCreator.CreatLine();
                }
                break;
            case Quest.State.CURRENT:
                if (qName_TP == Quest.Name.None)
                    ShowTeleport(false);
                break;
            case Quest.State.DONE:
                if (qName_TP == qName)
                    ShowTeleport(false);
                    QuestManager.Instance.lineCreator.AddList(transform);
                break;
        }
    }
}
