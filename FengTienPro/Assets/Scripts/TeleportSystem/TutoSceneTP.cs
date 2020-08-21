
public class TutoSceneTP : TeleportControllerBase
{
    public override void OnEnable()
    {
        target = PlayerController.instance.Target;
        pivot = PlayerController.instance.Cam;
        defaultFX.Play(true);
        isActive = true;
    }

    public override void Start()
    {
    }

}
