using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;
using UnityEngine;

public class LevelSceneTP : TeleportControllerBase
{
    [SerializeField]    private Levels level;

    private void OnEnable()
    {
        defaultFX.Play(true);
        isActive = true;
    }
    private void OnDisable()
    {
        defaultFX.Play(false);
        isActive = false;
    }

    public override void Start()
    {
        target = PlayerController.Instance.Target;
        pivot = PlayerController.Instance.Cam;
    }

    public override void TP_OnAfterTeleport(Teleportable src, RaycastResult hitResult, float delay)
    {
        base.TP_OnAfterTeleport(src, hitResult, delay);
        //just chang level for noe nothing matter
        GameController.Instance.level = level;
    }
}
