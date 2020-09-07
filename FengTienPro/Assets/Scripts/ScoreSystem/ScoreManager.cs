using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform Content;
    [SerializeField] private GameObject Item;
    [SerializeField] private List<ScoreItem> itemDatas;
    [SerializeField] private GameController gController;
    public void Start()
    {
        gController = GameController.Instance;
        gController.gameScoreInit += Set;
        gController.gameStartInit += Clear;
    }

    private void OnDestroy()
    {
        gController.gameScoreInit -= Set;
        gController.gameStartInit -= Clear;
    }


    public void Clear()
    {
        itemDatas.Clear();

        if (Content.childCount != 0)
        {
            foreach (ScoreItem obj in Content.GetComponentsInChildren<ScoreItem>())
            {
                Destroy(obj.gameObject);
            }
        }
    }

    public void Set()
    {
        if (itemDatas.Count > 0)
            Clear();

        foreach (QuestRecord questRe in gController.questList)
        {
            GameObject newItem = Instantiate(Item, Content);
            newItem.GetComponent<ScoreItem>().item_name.text = GetMultiTextbyGoaltype(questRe.GoalsName);
            newItem.GetComponent<ScoreItem>().item_score.text = questRe.doneRight ? "V": "X";
            itemDatas.Add(newItem.GetComponent<ScoreItem>());
        }
    }
    
    private string GetMultiTextbyGoaltype(Goal.Type goal)
    {
        string currentString ="goal_" + goal.ToString();
        for (int i = 0; i < gController.MultiLang.dataArray.Length; i++)
        {
            if ( currentString == gController.MultiLang.dataArray[i].Id)
            {
                currentString = gController.language == Language.Chinese ? gController.MultiLang.dataArray[i].Chinese : gController.MultiLang.dataArray[i].English;
            }
        }
        return currentString;
    }
}
