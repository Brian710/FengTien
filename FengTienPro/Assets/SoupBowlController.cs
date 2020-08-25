using UnityEngine;

public class SoupBowlController : IObjControllerBase
{
    //[SerializeField]
    //private GameObject On;

    //[SerializeField]
    //private Transform oriParent;
    //[SerializeField]
    //private ClicktoPosBase ClickInteract;
    //[SerializeField]
    //private Transform startParent;
    //[SerializeField]
    //private Transform targetParent;

    //bool gotObj;

    //public override void Start()
    //{
    //    base.Start();
    //    ClickInteract.targetParent = targetParent;
    //    ClickInteract.Iobj = this;
    //}
    //private void Awake()
    //{
    //    //oriPos = transform.localPosition;
    //    //oriRot = transform.localRotation;
    //    aliPos = new Vector3(0.011f, -0.054f, -0.05f);
    //    aliRot = new Vector3(0, 90, 90);
       
    //}
    ////public override void Set()
    ////{
    ////    base.Set();
    ////    On.SetActive(true);
    ////    gotObj = false;
    ////}

    //public override void GotoTargetPos()
    //{
    //    if (!gotObj)
    //    {
    //        if (startParent == null || targetParent == null)
    //            return;

    //        startParent.transform.SetParent(targetParent, false);
    //        startParent.transform.position = aliPos;
    //        Quaternion Rot = startParent.transform.rotation;
    //        Rot.eulerAngles = aliRot;
    //        startParent.transform.rotation = Rot;
    //        gotObj = true;
    //    }
    //    else
    //    {
    //        if (startParent == null || targetParent == null)
    //            return;

    //        startParent.transform.SetParent(oriParent, false);
    //        startParent.transform.position = oriPos;
    //        startParent.transform.rotation = oriRot;
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    SpoonController spoon = other.gameObject.GetComponent<SpoonController>();
    //    if (spoon != null && !spoon.IfHaveMat())
    //    {
    //        spoon.HaveMat(true);
    //    }
    //}


}
