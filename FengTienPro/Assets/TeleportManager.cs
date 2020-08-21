using System.Collections.Generic;
using UnityEngine;
public class TeleportManager : MonoBehaviour
{
    #region singleton

    public static TeleportManager Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        firstInit = true;
        teleportList = new Dictionary<Quest.Name, MainSceneTP>();
    }
    #endregion

    [SerializeField]
    private bool firstInit;
    [SerializeField]
    private List<MainSceneTP> teleportControllers;

    private Dictionary<Quest.Name, MainSceneTP> teleportList;
    
    private void Start()
    {
        Set();
        firstInit = false;
    }


    private void OnEnable()
    {
        if (teleportControllers.Count <= 4 || firstInit)
            return;
       
        Set();
    }

    private void Set()
    {
        teleportList.Clear();

        foreach (MainSceneTP TPB in teleportControllers)
        {
            teleportList.Add(TPB.questName , TPB);
            if (TPB.questName == Quest.Name.Talk || TPB.questName == Quest.Name.None)
            {
                TPB.ShowTeleport(true);
            }
            else
            {
                TPB.ShowTeleport(false);
            }
        }
        PrintTPLISET();
    }
    private void PrintTPLISET()
    {
        foreach (var tps in teleportList)
        {
            Debug.LogWarning($"tps.key: {tps.Key} ; tps.value{tps.Value}");
        }
    }
    public void ShowTPbyGoal(Quest.Name type)
    {
        teleportList[type].ShowTeleport(true);
    }
    public void HideTPbyGoal(Quest.Name type)
    {
        teleportList[type].ShowTeleport(false);
    }
}
