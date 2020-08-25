using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallVirtualTest : MonoBehaviour
{
    [SerializeField]
    VirtualObj virtualObj;
    [SerializeField]
    VirtualObj virtualObjTwo;
    // Start is called before the first frame update
    void Start()
    {
        virtualObj.VirtualObjVOFunc();
        virtualObjTwo.VirtualObjVOFunc();
    }
}
