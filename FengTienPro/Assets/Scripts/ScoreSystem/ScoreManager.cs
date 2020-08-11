using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Transform Content;
    public GameObject Item;

    public List<ScoreItem> itemDatas;

    private GameController gController;

    public void Start()
    {
        gController = GameController.Instance;
        Set();
    }

    public void Set()
    {
        if (gController.questList.Count == 0)
            return;

        if (Content.childCount != 0)
        {
            foreach (ScoreItem obj in Content.GetComponentsInChildren<ScoreItem>())
            {
                Destroy(obj.gameObject);
            }
        }

        foreach (QuestRecord questRe in gController.questList)
        {
            GameObject newItem = Instantiate(Item,Content);
            newItem.GetComponent<ScoreItem>().name.text = questRe.QuestName.ToString();
            newItem.GetComponent<ScoreItem>().score.text = questRe.QuestScore.ToString();
            itemDatas.Add(newItem.GetComponent<ScoreItem>());
        }
    }
}
