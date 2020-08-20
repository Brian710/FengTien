public class MainSceneTP : TeleportControllerBase
{
    public override void Start()
    {
        base.Start();
        if (GameController.Instance.mode != MainMode.Train)
        {
            defaultFX.Play(true);
            isActive = true;
        }
        QuestGiver.OnQuestAcceptListener += ShowTeleport;
        QuestGiver.OnQuestCompleteListener += ShowTeleport;
    }
}
