using System.Collections;
using UnityEngine;

public class TapController : IObjControllerBase
{
    [SerializeField]
    private Renderer lightMat;
    [SerializeField]
    private ParticleSystem FX;
    [SerializeField]
    private bool StepCompleted;

    private Coroutine _coroutine;
    private MaterialPropertyBlock _propBlock;
    [SerializeField]
    private GameObject washobj;
    private IWashable _IWashable;

    public override void Start()
    {
        hover.enabled = false;
        _propBlock = new MaterialPropertyBlock();
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.Tap).OnGoalStateChange += OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange += OnGoalStateChange;
    }

    public override void OnDestroy()
    {
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.Tap).OnGoalStateChange -= OnGoalStateChange;
        QuestManager.Instance.GetQuestGoalByType(Goal.Type.WashObj).OnGoalStateChange -= OnGoalStateChange;
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
        Debug.LogWarning(other.gameObject.name);
        _IWashable = other.gameObject.GetComponentInParent<IWashable>();
        washobj = _IWashable.Obj();
        if (_IWashable != null)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(CountDownSecond(_IWashable.WashTime()));
        }
     }

    public void OnTriggerExit(Collider other)
    {
        TapOn(false);
        SetLightColor(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _IWashable = null;
        washobj = null;
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
            if (_IWashable != null)
                _IWashable.IsWashed(true);
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
        lightMat.GetPropertyBlock(_propBlock);
        Color color = value ? Color.green : Color.red;
        _propBlock.SetColor("_EmissionColor", color);

        lightMat.SetPropertyBlock(_propBlock);
    }
}
