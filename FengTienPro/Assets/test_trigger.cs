using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_trigger : MonoBehaviour
{
    List<IWashable> washables;

    private void Awake()
    {
        washables = new List<IWashable>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<IWashable>() != null)
        {
            washables.Add(other.GetComponentInParent<IWashable>());
        }
        printList();
    }

    private void OnTriggerExit(Collider other)
    {
        washables.Clear();
    }

    void printList()
    {
        foreach (IWashable obj in washables)
        {
            Debug.Log(obj.Obj().name);
        }
    }
}
