using UnityEngine;

public class SoupBowlController :ClicktoPosBase
{
    [SerializeField]
    private GameObject On;

    [SerializeField]
    private Transform oriParent;
    [SerializeField]
    private Vector3 oriPos;
    [SerializeField]
    private Quaternion oriRot;

    [SerializeField]
    private Vector3 aliPos;
    [SerializeField]
    private Vector3 aliRot;

    bool gotObj;

    private void Awake()
    {
        oriPos = transform.localPosition;
        oriRot = transform.localRotation;
        aliPos = new Vector3(0.011f, -0.054f, -0.05f);
        aliRot = new Vector3(0, 90, 90);
       
    }
    public override void Set()
    {
        base.Set();
        On.SetActive(true);
        gotObj = false;
    }

    public override void GotoTargetPos()
    {
        if (!gotObj)
        {
            if (moveObj == null || targetPos == null)
                return;

            moveObj.transform.SetParent(targetPos, false);
            moveObj.transform.position = aliPos;
            Quaternion Rot = moveObj.transform.rotation;
            Rot.eulerAngles = aliRot;
            moveObj.transform.rotation = Rot;
            gotObj = true;
        }
        else
        {
            if (moveObj == null || targetPos == null)
                return;

            moveObj.transform.SetParent(oriParent, false);
            moveObj.transform.position = oriPos;
            moveObj.transform.rotation = oriRot;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SpoonController spoon = other.gameObject.GetComponent<SpoonController>();
        if (spoon != null && !spoon.IfHaveMat())
        {
            spoon.HaveMat(true);
        }
    }


}
