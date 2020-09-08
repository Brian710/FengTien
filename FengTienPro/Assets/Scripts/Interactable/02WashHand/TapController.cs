using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapController : IObjControllerBase
{
    [SerializeField]    private Renderer lightMat;
    [SerializeField]    private ParticleSystem FX;
    [SerializeField]    private bool StepCompleted;
    [SerializeField]    private List<IWashable> _IWashList;

    private Coroutine _coroutine;
    private MaterialPropertyBlock _propBlock;
    public override void Awake()
    {
        base.Awake();
        _IWashList = new List<IWashable>();
        _propBlock = new MaterialPropertyBlock();
    }
    public override void Start()
    {
        hover.enabled = false;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.Tap).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.TapClean).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashStuff).OnGoalStateChange += OnGoalStateChange;
    }

    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.Tap).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.TapClean).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashStuff).OnGoalStateChange -= OnGoalStateChange;
    }

    private void OnGoalStateChange(Goal.Type type, Goal.State state)
    {
        switch (state)
        {
            case Goal.State.WAITING:
                SetWaitingState();
                hover.enabled = false;
                break;
            case Goal.State.CURRENT:
                SetCurrentState();
                hover.enabled = true;
                hover.ShowHintColor(GameController.Instance.mode == MainMode.Train);
                break;
            case Goal.State.DONE:
                SetDoneState();
                hover.enabled = false;
                break;
        }
    }
    protected override void SetWaitingState()
    {
        if (FX.isPlaying)            FX.Stop(true);
        if (lightMat)            SetLightColor(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        TapOn(true);
        SetLightColor(true);
        
        if (other.GetComponentInParent<IWashable>() == null)
            return;

        _IWashList.Add(other.GetComponentInParent<IWashable>());
        if (_IWashList.Count > 0)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CountDownSecond(_IWashList[0].WashTime()));
        }

        PrintIWashList();
     }

    private void PrintIWashList()
    {
        if (_IWashList.Count <= 0)
            return;
        //foreach (IWashable obj in _IWashList)
        //{
        //    Debug.LogError(obj.Obj().name);
        //}
    }

    public void OnTriggerExit(Collider other)
    {
        TapOn(false);
        SetLightColor(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _IWashList.Clear();
    }

    public void TapOn(bool value)
    {
        if (FX)
        {
            if (value)
                FX.Play(true);
            else
                FX.Stop(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (StepCompleted)
        {
            QuestManager.Instance.AddQuestCurrentAmount(goalType);
            if (_IWashList.Count > 0)
            {
                foreach (IWashable washable in _IWashList)
                {
                    washable.SetWashed(true);
                }
            }
            StepCompleted = false;
        }
    }

    private IEnumerator CountDownSecond(int max)
    {
        int i = 0;
        while (i <= max)
        {
            yield return 1f;
            i++;
        }
        StepCompleted = true;
    }

    private void SetLightColor(bool value)
    {
        lightMat.material.color = value ? Color.green : Color.red;
    }
}
