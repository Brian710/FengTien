using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_obj : MonoBehaviour, IWashable
{
    public bool IsWashed()
    {
        throw new System.NotImplementedException();
    }

    public GameObject Obj()
    {
        return this.gameObject;
    }

    public void SetWashed(bool value)
    {
        throw new System.NotImplementedException();
    }

    public int WashTime()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
