using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private bool firstInit;

    public Transform Content;
    public GameObject Item;
    public List<ScoreItem> itemDatas;
    private GameController gController;
    private void Awake()
    {
        firstInit = true;
    }
    public void Start()
    {
        gController = GameController.Instance;
        Set();
        firstInit = false;
    }

    private void OnEnable()
    {
        if (firstInit|| gController.questList == null)
            return;

        Set();
    }

    public void Set()
    {
        if (Content.childCount != 0)
        {
            foreach (ScoreItem obj in Content.GetComponentsInChildren<ScoreItem>())
            {
                Destroy(obj.gameObject);
            }
        }

        foreach (QuestRecord questRe in gController.questList)
        {
            GameObject newItem = Instantiate(Item, Content);
            newItem.GetComponent<ScoreItem>().name.text = questRe.QuestName.ToString();
            newItem.GetComponent<ScoreItem>().score.text = questRe.doneRight ? "V": "X";
            itemDatas.Add(newItem.GetComponent<ScoreItem>());
        }
    }
}
