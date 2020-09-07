using UnityEngine;
using System.Collections.Generic;
using System;
using MinYan.Lang;

public class GameController : MonoBehaviour
{
    #region Properties
    [SerializeField]
    private bool _isTest;
    [SerializeField]
    private GameState _gameState;
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

    [SerializeField]    private Language _language;
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

    [SerializeField]    private MainMode _mode;
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

    [SerializeField]    private Levels _level;
    public Levels level
    {
        get { return _level; }
        set { _level = value; }
    }

    [SerializeField]    private muitiLang multiL;
    public muitiLang MultiLang => multiL;

    [SerializeField]    private PlayerController _currentPlayer;

    public PlayerController currentPlayer => _currentPlayer;

    [SerializeField]    private int _score;
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

    [SerializeField]    private Quest _quest;

    public Quest quest
    {
        get { return _quest; }
        set
        {
            _quest = value;
            OnGameQuestChange();
        }
    }
    public QuestGoal currentGoal => _quest.GetCurrentGoal();

    private HashSet<QuestRecord> _questList;
    public HashSet<QuestRecord> questList 
    {
        get 
        {
            return _questList;
        }
    }

    public void AddtoRecord(List<QuestGoal> goals)
    {
        if (goals.Count <= 0)
            return;

        QuestRecord record = new QuestRecord();
        foreach (QuestGoal g in goals)
        {
            record.GoalsName = g.type;
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
    protected virtual void SingletonInit()
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

    protected virtual void Awake()
    {
        SingletonInit();
        _gameState = GameState.LogoInit;
        _language = Language.Chinese;
        _mode = MainMode.Train;
        _level = Levels.none;
        _score = 0;
        _quest = null;
        _questList = new HashSet<QuestRecord>();
    }
    private void Start()
    {
        if (_currentPlayer == null)
            _currentPlayer = PlayerController.Instance;
        if (multiL == null)
            multiL = Resources.Load<muitiLang>("ExcelFiles/muitiLang");
        OnLanguageChange();
        UpdateLog();
    }

    #region Methods
    public event Action gameStartInit, gameMainInit, gamTutoInit, gameLevelnit,gameScoreInit;

    private void OnGameStateChange()
    {
        switch (_gameState)
        {
            case GameState.StartInit:
                _mode = MainMode.Train;
                _level = Levels.none;
                _quest = null;
                if(_questList.Count > 0)
                    _questList.Clear();

                gameStartInit?.Invoke();
                break;
            case GameState.MainInit:
                gameMainInit?.Invoke();
                break;
            case GameState.TutoInit:
                gamTutoInit?.Invoke();
                break;
            case GameState.LevelInit:
                gameLevelnit?.Invoke();
                break;
            case GameState.ScoreInit:
                gameScoreInit?.Invoke();
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
            gameState = _gameState;
            UpdateLog();
        }
    }
#endif
    private void UpdateLog()
    {
        if (_currentPlayer && _isTest)
        {
            //string questName = "";
            //string goaltype = "";
            //if (gameState == GameState.MainInit)
            //{
            //    if (_quest != null) { questName = _quest.qName.ToString(); }
            //    if (currentGoal != null) { goaltype = currentGoal.type.ToString(); }
            //}
            string log = $"State: {_gameState} \n"
                             + $"Lang: {_language} \n"
                             //+ $"Quest: {questName} Goal: {goaltype}\n"
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
