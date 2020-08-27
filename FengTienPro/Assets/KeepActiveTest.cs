using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepActiveTest : MonoBehaviour
{
    [SerializeField]
    GameObject Obj;
    [SerializeField]
    AwakeEnableStartTest Comp;

    // Update is called once per frame
    private void Start()
    {
        Obj.SetActive(true);
        Comp.enabled = true;
        Comp.enabled = false;
        Obj.SetActive(false);
        Comp.enabled = true;
    }
}
