using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public static StartSceneManager instance;

    public Transform StartScenePos;
    public Transform TutoScenePos;
    public Transform LevelScenePos;

    [SerializeField]
    private PlayerController player;

    public GameObject logo;

    public GameObject menuCanv;
    [SerializeField]
    private Animator transition;
    [SerializeField]
    private Button enterBtn;
    [SerializeField]
    private Button langSwitchBtn;
    [SerializeField]
    private Button tutoBtn;
    [SerializeField]
    private Button exitBtn;

    private void Start()
    {
        if (player == null)
            player = PlayerController.Instance;
        StartCoroutine(GameInit());
    }

    private IEnumerator GameInit()
    {
        yield return new WaitForSeconds(3f);
        logo.GetComponent<Animator>().SetBool("End", true);
        yield return new WaitForSeconds(1f);
        MenuCanvSet();
    }

    private void MenuCanvSet()
    {
        menuCanv.SetActive(true);
        StartSceneActive(true);
        exitBtn.onClick.AddListener(ExitBtn);
        tutoBtn.onClick.AddListener(TutoBtn);
        enterBtn.onClick.AddListener(EnterBtn);
        langSwitchBtn.onClick.AddListener(LangBtn);
    }

    private void ExitBtn()
    {
        StartSceneActive(false);
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void TutoBtn()
    {
        //StartSceneActive(false);
        GameController.Instance.gameState = GameState.TutoInit;
        ChangePosByState();
    }

    private void LangBtn()
    {
        Language lang = GameController.Instance.language;
        GameController.Instance.language = lang == Language.Chinese ? Language.English : Language.Chinese;
    }

    private void EnterBtn()
    {
        //StartSceneActive(false);
        GameController.Instance.gameState = GameState.LevelInit;
        ChangePosByState();
    }

    private void ChangePosByState()
    {
        //StartCoroutine(player.TransAnimPlaytoEnd(true));
        Transform temp = StartScenePos;
        switch (GameController.Instance.gameState)
        {
            case GameState.LevelInit:
                temp = LevelScenePos;
                break;
            case GameState.TutoInit:
                temp = TutoScenePos;
                break;
        }
        
        StartCoroutine(player.ChangePos(temp));
    }

    public void ChangePostoStart()
    {
        GameController.Instance.gameState = GameState.StartInit;
        //StartCoroutine(player.TransAnimPlaytoEnd(true));
        if(StartScenePos)
            StartCoroutine(player.ChangePos(StartScenePos));
    }

    public void StartSceneActive(bool value)
    {
        StartCoroutine(SetCanvVisiable(value));
    }

    private IEnumerator SetCanvVisiable(bool value)
    {
        transition.SetBool("End", !value);
        yield return new WaitForSeconds(1f);
    }
}
