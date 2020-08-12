
public class MainSceneTP : TeleportManager
{
    public override void Start()
    {
        base.Start();
        if (GameController.Instance.mode != MainMode.Train)
        {
            defaultFX.Play(true);
            isActive = true;
        }
    }
    public override void OnEnable()
    {
        base.OnEnable();
        QuestGiver.OnQuestAcceptListener += ShowTeleport;
        QuestGiver.OnQuestCompleteListener += ShowTeleport;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        QuestGiver.OnQuestAcceptListener -= ShowTeleport;
        QuestGiver.OnQuestCompleteListener -= ShowTeleport;
    }
}
