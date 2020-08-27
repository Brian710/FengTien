using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookPotController : IObjControllerBase
{
    [SerializeField]
    private List<GameObject> gameObjects;

    [SerializeField]
    private List<InputMatObj> FoodMats;

    [SerializeField]
    private GameObject Ladle;

    [SerializeField]
    private Animator CookAnim;

    [SerializeField]
    private Animator CookUI;

    [SerializeField]
    private ParticleSystem CookUIDone;

    private Coroutine _coutdownCoro;
    private int timer;

    List<Vector3> FoodMatPos = new List<Vector3>();
    List<Quaternion> FoodMatRot = new List<Quaternion>();

    public override void Awake()
    {
        base.Awake();
        goalType = Goal.Type.CookFood;
    }

    public override void Start()
    {
        base.Start();
        foreach (IObjControllerBase trans in FoodMats)
        {
            FoodMatPos.Add(trans.transform.position);
            FoodMatRot.Add(trans.transform.rotation);
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    protected override void SetWaitingState()
    {
        //base.SetWaitingState();
        timer = 0;
        CookUIDone.Stop();
        Ladle.SetActive(false);

        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }
    protected override void SetCurrentState()
    {
        Ladle.SetActive(true);
    }
    protected override void SetDoneState()
    {
        Ladle.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Mathf.Abs(other.transform.rotation.x) <= 0.2 || other.GetComponentInParent<InputMatObj>() == null)
        {
            return;
        }

        QuestManager.Instance.AddQuestCurrentAmount(other.GetComponentInParent<InputMatObj>().goalType);
        switch (other.GetComponentInParent<InputMatObj>().goalType)
        {
            case Goal.Type.InputRice:
                gameObjects[0].SetActive(true);
                break;
            case Goal.Type.InputWater:
                gameObjects[1].SetActive(true);
                break;
            case Goal.Type.InputFish:
                gameObjects[2].SetActive(true);
                break;
            case Goal.Type.InputVeg:
                gameObjects[3].SetActive(true);
                break;
        }
    }

    public void CookCoutdown(bool value)
    {
        if (value)
        {
            CookAnim.SetBool("On", value);
            if (_coutdownCoro != null)
                StopCoroutine(_coutdownCoro);

            _coutdownCoro = StartCoroutine(CoundownTimer(10, 15));
        }
        else
        {
            if (_coutdownCoro == null)
                return;

            if (10 <= timer && timer <= 15)
            {
                CookUIDone.Play();
                QuestManager.Instance.AddQuestCurrentAmount(goalType);
            }

            CookAnim.SetBool("On", value);

            if (_coutdownCoro != null)
                StopCoroutine(_coutdownCoro);
            
            if(timer > 15)
                QuestManager.Instance.ReopenQuestGiver();
        }
    }

    IEnumerator CoundownTimer(int cooked, int overcook)
    {
        while (timer <= cooked)
        {
            yield return new WaitForSeconds(1f);
            CookUI.SetFloat("CookTime", timer / cooked);
            timer++;
        }
        while (timer <= overcook)
        {
            yield return new WaitForSeconds(1f);
            CookUI.SetFloat("CookTime", timer / cooked);
            timer++;
        }
        CookCoutdown(false);
    }
}
