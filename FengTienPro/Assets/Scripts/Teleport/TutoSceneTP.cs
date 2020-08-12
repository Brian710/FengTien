
public class TutoSceneTP : TeleportManager
{
    public override void Start()
    {
        target = PlayerController.instance.Target;
        pivot = PlayerController.instance.Cam;
        defaultFX.Play(true);
        isActive = true;
    }
}
