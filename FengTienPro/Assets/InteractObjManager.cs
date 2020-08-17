using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjManager : MonoBehaviour
{
    [SerializeField]
    private bool firstInit = true;
    [SerializeField]
    private List<GameObject> gameObjects;

    private void Start()
    {
        Set();
        firstInit = false;
    }
    private void OnEnable()
    {
        Set();
    }

    private void Set()
    {
        if (gameObjects.Count <= 0 || firstInit)
            return;

        int i = 0;
        foreach (GameObject obj in gameObjects)
        {
            if (i == 0)
                obj.SetActive(true);
            else
                obj.SetActive(false);
            i++;
        }
    }
}
