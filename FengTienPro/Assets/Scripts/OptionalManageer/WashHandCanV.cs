using UnityEngine;
public class WashHandCanV : OptionalSystemBase
{
    [SerializeField]    private ShowHandWash Anim;
    public override void OpenCanv(bool value)
    {
        base.OpenCanv(value);
        Anim.ShowAnimation(value);
    }
}
