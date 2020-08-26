using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iwashableStuff : MonoBehaviour ,IWashable
{
    public bool IsWashed(bool value)
    {
        throw new System.NotImplementedException();
    }

    public GameObject Obj()
    {
        return this.gameObject;
    }

    public int WashTime()
    {
        throw new System.NotImplementedException();
    }
}
