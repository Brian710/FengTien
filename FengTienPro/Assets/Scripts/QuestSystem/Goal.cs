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
        TapClean,
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
    public enum State { WAITING,CURRENT,DONE}

    public Type type;
    public State status;
    public int currentAmount;
    public int requiredAmount;
}
