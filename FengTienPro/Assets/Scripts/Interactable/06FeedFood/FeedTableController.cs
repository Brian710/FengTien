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

    private void OnQuestChange(Quest.Name name, Quest.State state)
    {
        switch (state)
        {
            case Quest.State.WAITING:
                childObj.SetActive(false);
                break;
            case Quest.State.CHOOSABLE:
                break;
            case Quest.State.CURRENT:
                childObj.SetActive(true);
                break;
            case Quest.State.DONE:
                childObj.SetActive(false);
                break;
        }
    }
}
