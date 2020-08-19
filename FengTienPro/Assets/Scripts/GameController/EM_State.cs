using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    //Logo
    LogoInit = 0,
    //TutorialScene
    TutoInit = 10,
    //StartScene
    StartInit = 20 ,
    //LevelScene
    LevelInit = 30,
    LevelSelmode = 31,
    //MainScene
    MainInit = 40,
    MainNormal = 41,
    MainFeedTalk = 42,
    MainFeedWashHand = 43,
    MainFeedCook = 44,
    MainFeedFood = 45,
    MainFeedClean = 46,
    MainFeedMeds = 47,
    MainFinal = 49,
    //
    ScoreInit = 99,
}
public enum Language
{
    Chinese = 0,
    English = 1,
}
public enum MainMode
{
    Train = 0,
    Exam = 1,
}
public enum Levels
{
    none = 0,
    Life = 1,
    Heimlich = 2,
    Feed = 3,
}
public enum GameQuest 
{
    None,
    Talk,
    TalkEnd,
    WashHand,
    WashHandSoap,
    WashHandWet,
    WashHandCanv,
    WashHandClear,
    WashHandFinal,
    WashHandEnd,
    CookClean,
    CookCleanTake,
    CookCleanWash,
    CookCleanPut,
    CookCleanEnd,
    FeedFood,
    FeedFoodEnd,
    Clean,
    CleanEnd,
    FeedMeds,
    FeedMedsEnd,
}
public enum InteractObjType 
{
    Interact,
    Grab,
    GotoPos,
    AnimTrigger,
}
public enum ObjActMode
{
    openFaucet,
    closeFaucet,
    pressHandwash,
    touchedwater,
}
[System.Serializable]
public enum HandAnim
{
    Normal,
    Knife,
    SoupLandle,
    Board,
    Spoon,
    Bowl,
}
public enum TeleportType
{ 
    tutorial,
    level_life,
    level_heimlich,
    level_feed,
    normal,
    entrance,
    sofa,
    washhand,
    cook,
    bed,
}