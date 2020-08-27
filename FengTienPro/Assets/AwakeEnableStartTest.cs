using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeEnableStartTest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("OnAwake");
    }
    private void OnEnable()
    {
        Debug.Log("OnEnable");
    }
    void Start()
    {
        Debug.Log("OnStart");
    }
}
