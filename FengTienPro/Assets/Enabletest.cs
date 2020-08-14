using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabletest : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log(name + "awake");       
    }
    void Start()
    {
        Debug.Log(name+"start");

    }

    private void OnEnable()
    {
        Debug.Log(name+"enable");
    }
}
