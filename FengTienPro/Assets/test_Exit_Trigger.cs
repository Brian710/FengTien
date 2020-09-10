using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_Exit_Trigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
    }
}
