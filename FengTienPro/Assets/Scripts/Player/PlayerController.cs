using HTC.UnityPlugin.Vive;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class RayEvent : UnityEvent<bool> { }
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [SerializeField]    private Transform cam;

    public Transform Cam => cam;

    [SerializeField]    private Transform target;

    public Transform Target => target;

    [SerializeField]    private GameObject RightUIRay;
    [SerializeField]    private GameObject LeftUIRay;
    [SerializeField]    private ControllerButton UIRayActivationBtn;

    public bool EnableRightRay { get; set; } = true;

    public bool EnableLeftRay { get; set; } = true;

    [SerializeField]    private GameObject RightTeleportRay;
    [SerializeField]    private GameObject LeftTeleportRay;

    public bool EnableRightTeleport { get; set; } = true;
    public bool EnableLeftTeleport { get; set; } = true;

    public HandAnimManager RightHand;
    public HandAnimManager LeftHand;

    [SerializeField]    private ControllerButton HandAnimControlBtn;
    [SerializeField]    private ControllerButton teleportActivationBtn;
    [SerializeField]    private Animator TransAnim;
    [SerializeField]    private bool transAnimisDone;
    public bool TransAnimisDone => transAnimisDone;

    [SerializeField]    private Animator GoalCompletedAnim;
    [SerializeField]    private Animator QuestCompletedAnim;
    [SerializeField]    private Text logText;

    public void AllRayActivity(bool value)
    {
        EnableRightTeleport = value;
        EnableLeftTeleport = value;
        EnableLeftRay = value;
        EnableRightRay = value;
    }

    public void Showlog(string log)
    {
        if (logText == null)
            return;

        logText.text = log;
    }

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
    private void Start()
    {
            StartCoroutine(TransAnimPlaytoEnd(false));
    }
    private void LateUpdate()
    {
        if (RightTeleportRay)
        {
            RightTeleportRay.SetActive(EnableRightTeleport && ViveInput.GetPressEx(HandRole.RightHand, teleportActivationBtn));
        }

        if (LeftTeleportRay)
        {
            LeftTeleportRay.SetActive(EnableLeftTeleport && ViveInput.GetPressEx(HandRole.LeftHand, teleportActivationBtn));
        }

        if (RightUIRay)
        {
            RightUIRay.SetActive(EnableRightRay && !ViveInput.GetPressEx(HandRole.RightHand, teleportActivationBtn));
        }

        if (LeftUIRay)
        {
            LeftUIRay.SetActive(EnableLeftRay && !ViveInput.GetPressEx(HandRole.LeftHand, teleportActivationBtn));
        }

        if (RightHand)
        {
            RightHand.NormalAnimUpdate(ViveInput.GetTriggerValue(HandRole.RightHand));
        }

        if (LeftHand)
        {
            LeftHand.NormalAnimUpdate(ViveInput.GetTriggerValue(HandRole.LeftHand));
        }
    }
#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        if (Application.isPlaying) UpdateActivity();
    }
#endif
    public void UpdateActivity()
    { 
    }
    public IEnumerator TransAnimPlaytoEnd(bool value)
    {
        transAnimisDone = false;
        if (TransAnim)
        {
            Debug.LogWarning("Playertrans");
            TransAnim.SetTrigger("End");
            yield return new WaitForSeconds(2.5f);
            transAnimisDone = true;
        }
    }

    public void QuestGoalCompleted()
    {
        if (GoalCompletedAnim == null)
        {
            Debug.Log("CompletedAnim is null");
            return;
        }
        GoalCompletedAnim.SetTrigger("Done");
        AudioManager.Instance.Play("StepFinish");
    }

    public void QuestCompleted()
    {
        if (QuestCompletedAnim == null)
        {
            Debug.Log("QuestCompletedAnim is null");
            return;
        }

        QuestCompletedAnim.SetTrigger("Done");
        AudioManager.Instance.Play("QuestCompleted");
    }

    public IEnumerator ChangePos(Transform value)
    {
        StartCoroutine(TransAnimPlaytoEnd(false));
        yield return new WaitForSeconds(1.1f);
        transform.position = value.position;
        transform.rotation = value.rotation;
        while (!TransAnimisDone)
        {
            yield return null;
        }
    }
}
