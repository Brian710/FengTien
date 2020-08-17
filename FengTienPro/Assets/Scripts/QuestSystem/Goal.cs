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
        //Talk
        TalkCanv,
        //WashHand
        Watch,
        Tap,
        Soap,
        WashHandCanv,
        Tissue,
        //WashFood
        WashObj,
        //CutFood
        CutFish,
        CutVeg,
        //CookFood
        InputCookMat,
        CookFood,
        TasteFood,
        //FeedFood
        PutOnBib,
        TakeBowl,
        FeedFood,
        DrinkWater,
        //CleanKit
        WashStuff,
        ThrowWaste,
        CleanDesk,
        //FeedMeds
        GrindMeds,
        MixWater,
        CheckNasogastricTube,
        FeedMsds, //3 times
        FinishFeedMeds,
        //CleanMeds
        TissueClean,
        WaterClean,
    
    }
    [Serializable]
    public enum Status { WAITING,CURRENT,DONE}

    public Type type;
    public Status status;
    public int currentAmount;
    public int requiredAmount;
}
