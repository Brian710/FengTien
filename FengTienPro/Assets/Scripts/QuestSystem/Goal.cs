using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Goal", menuName = "Goal")]
public class Goal : ScriptableObject
{
    [Serializable]
    public enum Type 
    {
        None,

        Tuto,
        
        TalkCanv,

        Watch,
        Tap,
        Soap,
        WashHandCanv,
        Tissue,
        
        WashObj,
        CutFish,
        CutVeg,
        InputCookMat,
        CookFood,
        TasteFood,
    }
    [Serializable]
    public enum Status { WAITING,CURRENT,DONE}

    public Type type;
    public Status status;
    public int currentAmount;
    public int requiredAmount;
}
