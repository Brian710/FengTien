using HTC.UnityPlugin.Vive;
using MinYanGame.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Teleportable))]
public class TeleportManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem defaultParticle;

    [SerializeField]
    private Teleportable TP;

    public TeleportType teleportName;

    private void Start()
    {
        TP.target = PlayerController.instance.Target;
        TP.pivot = PlayerController.instance.Cam;

        if (teleportName.ToString().Contains("level_") || teleportName == TeleportType.entrance || teleportName == TeleportType.sofa || teleportName == TeleportType.normal)
        {
            defaultParticle.Play(true);
            TP.enabled = true;
        }

        if (GameController.Instance.mode != MainMode.Train)
        {
            defaultParticle.Play(true);
            TP.enabled = true;
        }

        if (teleportName == TeleportType.tutorial)
        {
            defaultParticle.Play(false);
            TP.enabled = false;
        }
    }

    private void OnEnable()
    {
        TP.OnAfterTeleport += TP_OnAfterTeleport;
        QuestGiver.OnQuestAcceptListener += ShowTeleport;
        QuestGiver.OnQuestCompleteListener += ShowTeleport;
    }
    private void OnDisable()
    {
        TP.OnAfterTeleport -= TP_OnAfterTeleport;
        QuestGiver.OnQuestAcceptListener -= ShowTeleport;
        QuestGiver.OnQuestCompleteListener -= ShowTeleport;
    }

    private void TP_OnAfterTeleport(Teleportable src, UnityEngine.EventSystems.RaycastResult hitResult, float delay)
    {
        switch (teleportName)
        {
            case TeleportType.level_life:
                break;
            case TeleportType.level_heimlich:
                break;
            case TeleportType.level_feed:
                GameController.Instance.level = Levels.Feed;
                GameController.Instance.gameState = GameState.MainInit;
                break;
        }

    }

    public void ShowTeleport(bool value)
    {
        if (defaultParticle && value)
        {
            TP.enabled = value;
            defaultParticle.Play(true);
        }
        else
        { 
            defaultParticle.Stop(true);
            TP.enabled = value;
        }
    }
}
