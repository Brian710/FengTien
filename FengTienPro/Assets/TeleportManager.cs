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
    }
    #endregion

    [SerializeField]
    private bool firstInit = true;
    [SerializeField]
    private List<MainSceneTP> teleportControllers;
    private Dictionary<Quest.Name, MainSceneTP> teleportList;
    
    private void Start()
    {
        teleportControllers = new List<MainSceneTP>();
        firstInit = true;
        Set();
    }


    private void OnEnable()
    {
        if (teleportControllers.Count <= 4 || firstInit)
            return;
       
        Set();
    }

    private void Set()
    {
        foreach (MainSceneTP TPB in teleportControllers)
        {
            teleportList.Add(TPB.questName , TPB);
            if (TPB.questName == Quest.Name.Talk || TPB.questName == Quest.Name.None)
            {
                TPB.gameObject.SetActive(true);
                TPB.ShowTeleport(true);
            }
            else
            {
                TPB.gameObject.SetActive(false);
                TPB.ShowTeleport(false);
            }
        
        }
    }

    public void ShowTPbyGoal(Quest.Name type)
    {
        teleportList[type].gameObject.SetActive(true);
        teleportList[type].ShowTeleport(true);
    }
    public void HideTPbyGoal(Quest.Name type)
    {
        teleportList[type].ShowTeleport(false);
        teleportList[type].gameObject.SetActive(false);
    }
}
