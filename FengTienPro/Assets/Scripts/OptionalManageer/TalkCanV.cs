public class TalkCanV : OptionalSystemBase
{
    public override void OptBtnOnclick(int index)
    {
        base.OptBtnOnclick(index);
        //do play grandpa's voice
        if (index == 1)
            AudioManager.Instance.Play("");
    }
}
