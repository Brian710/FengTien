using UnityEngine;

public class FeedTableController : MonoBehaviour
{
    [SerializeField]
    private Quest.Name qName;
    [SerializeField]
    private GameObject childObj;
    void Start()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange += OnQuestChange;
        childObj.SetActive(false);
    }
    private void OnDestroy()
    {
        QuestManager.Instance.GetQuestByName(qName).OnQuestChange -= OnQuestChange;
    }

    private void OnQuestChange(Quest.Name name, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                childObj.SetActive(false);
                break;
            case Quest.State.CHOOSABLE:
                childObj.SetActive(true);
                break;
            case Quest.State.CURRENT:
                break;
            case Quest.State.DONE:
                childObj.SetActive(false);
                break;
        }
    }
}
