using HTC.UnityPlugin.Vive;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class RayEvent : UnityEvent<bool> { }
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField]
    private Transform cam;

    public Transform Cam
    {
        get { return cam; }
    }

    [SerializeField]
    private Transform target;

    public Transform Target
    {
        get { return target; }
    }

    [SerializeField]
    private GameObject RightUIRay;
    [SerializeField]
    private GameObject LeftUIRay;

    [SerializeField]
    private ControllerButton UIRayActivationBtn;

    public bool EnableRightRay { get; set; } = true;

    public bool EnableLeftRay { get; set; } = true;


    [SerializeField]
    private GameObject RightTeleportRay;
    [SerializeField]
    private GameObject LeftTeleportRay;

    public bool EnableRightTeleport { get; set; } = true;

    public bool EnableLeftTeleport { get; set; } = true;

    [SerializeField]
    private HandAnimManager RightHand;

    [SerializeField]
    private HandAnimManager LeftHand;

    [SerializeField]
    private ControllerButton HandAnimControlBtn;

    [SerializeField]
    private ControllerButton teleportActivationBtn;

    [SerializeField]
    private Animator TransAnim;
   
    private bool transAnimisDone = false;
    public bool TransAnimisDone { get { return transAnimisDone; } }

    [SerializeField]
    private Animator CompletedAnim;

    [SerializeField]
    private Text logText;

    [SerializeField]
    private Transform initPos;

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
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (TransAnim)
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
            TransAnim.SetBool("End", value);
            yield return new WaitForSeconds(1.5f);
            transAnimisDone = true;
        }
    }

    public void QuestStepCompleted()
    {
        if (CompletedAnim == null)
        {
            Debug.Log("CompletedAnim is null");
            return;
        }
         
        CompletedAnim.SetTrigger("Done");
    }


    public IEnumerator ChangePos(Transform value)
    {
        while (!TransAnimisDone)
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        transform.position = value.position;
        transform.rotation = value.rotation;
        StartCoroutine(TransAnimPlaytoEnd(false));
    }


}
