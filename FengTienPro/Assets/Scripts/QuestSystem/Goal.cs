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
        InputRice,
        InputWater,
        InputFish,
        InputVeg,
        InputSalt,
        CookFood,
        TasteFood,
        //FeedFood
        PutOnBib,
        TakeBowl,
        GetTemper,
        TakeSpoon,
        FeedFood,
        FeedWater,
        //CleanKit
        WashStuff,
        ThrowWaste,
        CleanDesk,
        //FeedMeds
        MedsTalk,
        GrindMeds,
        PourPowder,
        MixWater,
        CheckNasogastricTube,
        FeedMeds, //3 times
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
