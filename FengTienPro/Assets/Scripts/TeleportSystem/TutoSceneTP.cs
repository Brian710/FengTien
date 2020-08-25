
public class TutoSceneTP : TeleportControllerBase
{
    private void OnEnable()
    {
        target = PlayerController.Instance.Target;
        pivot = PlayerController.Instance.Cam;
        defaultFX.Play(true);
        isActive = true;
    }

    public override void Start()
    {
    }

}
