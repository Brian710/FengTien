using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using MinYanGame.Core;

public class GameController : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private bool _isTest;
    [SerializeField]
    private GameState _gameState = GameState.LogoInit;
    public GameState gameState
    {
        get { return _gameState; }
        set
        {
            if (_gameState == value) return;
            else
            {
                _gameState = value;
                OnGameStateChange();
            }
        }
    }

    [SerializeField]
    private Language _language = Language.Chinese;
    public Language language
    {
        get { return _language; }
        set
        {
            if (_language == value) return;
            else
            {
                _language = value;
                OnLanguageChange();
            }
        }
    }

    [SerializeField]
    private MainMode _mode = MainMode.Train;
    public MainMode mode
    {
        get { return _mode; }
        set
        {
            if (_mode == value) return;
            else
            {
                _mode = value;
                OnMainModeChange();
            }
        }
    }

    [SerializeField]
    private Levels _level = Levels.none;
    public Levels level
    {
        get { return _level; }
        set { _level = value; }
    }

    [SerializeField]
    private muitiLang multiL;
    public muitiLang MultiLang
    {
        get
        {
            if (multiL == null)
                multiL = Resources.Load<muitiLang>("ExcelFiles/muitiLang");
            return multiL;
        }
    }

    [SerializeField]
    private PlayerController _currentPlayer = null;

    public PlayerController currentPlayer
    {
        get
        {
            if (_currentPlayer == null)
                _currentPlayer = PlayerController.instance;

            return _currentPlayer;
        }
    }

    [SerializeField]
    private int _score = 0;
    public int score
    {
        get { return _score; }
        set
        {
            if (value <= 0)
                _score = 0;
            else if (value >= 100)
                _score = 100;
            else
                _score = value;

            UpdateLog();
        }
    }

    [SerializeField]
    private Quest _quest;

    public Quest quest
    {
        get { return _quest; }
        set
        {
            _quest = value;
            OnGameQuestChange();
        }
    }

    public QuestGoal currentGoal
    {
        get 
        {
            if (_quest == null)
                return null;
            else
                return _quest.GetCurrentGoal();
        }
    }


    private HashSet<QuestRecord> _questList = new HashSet<QuestRecord>();


    public HashSet<QuestRecord> questList { get { return _questList; } }

    public void AddtoRecord(List<QuestGoal> goals)
    {
        if (goals.Count <= 0)
            return;

        QuestRecord record = new QuestRecord();
        foreach (QuestGoal g in goals)
        {
            record.QuestName = g.type;
            record.doneRight = g.doItRight;
            _questList.Add(record);
        }
    }

    #endregion

    #region singleton
    /// <summary>
    /// The instance.
    /// </summary>
    public static GameController Instance;
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
    private void Start()
    {
        if (_currentPlayer == null)
            _currentPlayer = PlayerController.instance;
        if (multiL == null)
            multiL = Resources.Load<muitiLang>("ExcelFiles/muitiLang");
        OnLanguageChange();
        UpdateLog();
    }
    #region Methods

    private void OnGameStateChange()
    {
        switch (_gameState)
        {
            case GameState.StartInit:
                mode = MainMode.Train;
                level = Levels.none;
                quest = null;
                questList.Clear();
                break;
        }

        UpdateLog();
    }
    
    private void OnGameQuestChange()
    {
        UpdateLog();
    }
    
    private void OnLanguageChange()
    {
        foreach (updatingMultiText upText in FindObjectsOfType<updatingMultiText>())
        {
            upText.Set();
        }
        foreach (updatingMultiImg upImg in FindObjectsOfType<updatingMultiImg>())
        {
            upImg.Set();
        }
        UpdateLog();
    }

    private void OnMainModeChange()
    {
        UpdateLog();
    }

    public void UnLoadSubController()
    {
        foreach (Transform sub in GetComponentsInChildren<Transform>())
        {
            Destroy(sub);
        }
    }

#if UNITY_EDITOR

    protected virtual void OnValidate()
    {
        if (Application.isPlaying)
        {
            UpdateLog();
            gameState = _gameState;
        }
    }

#endif
    private void UpdateLog()
    {
        if (_currentPlayer && !_isTest)
        {
            string log = $"State: {_gameState} \n"
                             + $"Lang: {_language} \n"
                             + $"Quest: {_quest.questName} Goal: {currentGoal.goal.type}\n"
                             + $"Level: { _level} \n"
                             + $"Score: {_score} \n"
                             + $"PlayerData \n"
                             + $"UIRay: R_{_currentPlayer.EnableRightRay} L_{_currentPlayer.EnableLeftRay}\n"
                             + $"TPRay: R_{_currentPlayer.EnableRightTeleport} L_{_currentPlayer.EnableLeftTeleport}\n";
            _currentPlayer.Showlog(log);
        }

    }
    #endregion

    /// <summary>
    /// Functions
    /// </summary>
    //public void StringToScene(string scenename)
    //{
    //    if (currentPlayer)
    //        StartCoroutine(currentPlayer.TransAnimPlaytoEnd(true));

    //    StartCoroutine(LoadScene(scenename));
    //}

    //public IEnumerator LoadScene(string scenename)
    //{
    //    Debug.LogWarning("LoadSceneInit");
    //    AsyncOperation operation = SceneManager.LoadSceneAsync(scenename);
    //    operation.allowSceneActivation = false;
    //    while (operation.progress < .9f)
    //    {
    //        yield return null;
    //    }
    //    if (!_currentPlayer.TransAnimisDone)
    //    {
    //        yield return null;
    //    }
    //    operation.allowSceneActivation = true;
    //    yield return new WaitForSeconds(.5f);
    //    currentPlayer.InitPos();
    //    Debug.LogWarning("LoadSceneEnd");
    //}
}
