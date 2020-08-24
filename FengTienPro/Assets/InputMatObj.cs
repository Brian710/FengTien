using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMatObj : InteracObjBase
{
    [SerializeField]
    private GameObject On;
    public override void Set()
    {
        base.Set();
        if(!On.activeSelf)
            HaveMats(true);
    }

    public void HaveMats(bool value)
    {
        if(On)
            On.SetActive(value);
    }

    public bool IfHaveMats()
    {
        if (On)
            return On.activeSelf;
            
        return true;
    }
}
