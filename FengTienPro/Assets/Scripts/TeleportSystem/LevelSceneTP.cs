using UnityEngine.EventSystems;
using HTC.UnityPlugin.Vive;


public class LevelSceneTP : TeleportControllerBase
{
    public Levels level;

    public override void TP_OnAfterTeleport(Teleportable src, RaycastResult hitResult, float delay)
    {
        base.TP_OnAfterTeleport(src, hitResult, delay);
        //just chang level for noe nothing matter
        GameController.Instance.level = level;
    }
}
