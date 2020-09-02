using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_trigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError(other.GetComponentInParent<IWashable>().Obj().name);
    }
}
