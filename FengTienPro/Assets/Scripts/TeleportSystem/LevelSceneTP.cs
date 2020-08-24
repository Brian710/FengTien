using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;


public class LevelSceneTP : TeleportControllerBase
{
    public Levels level;

    private void OnEnable()
    {
        target = PlayerController.instance.Target;
        pivot = PlayerController.instance.Cam;
        defaultFX.Play(true);
        isActive = true;
    }

    public override void Start()
    {
    }

    public override void TP_OnAfterTeleport(Teleportable src, RaycastResult hitResult, float delay)
    {
        base.TP_OnAfterTeleport(src, hitResult, delay);
        //just chang level for noe nothing matter
        GameController.Instance.level = level;
    }
}
