using UnityEngine;

public class GlassController : InteracObjBase
{
    [SerializeField]
    private Animator glassAnim;

    public override void Set()
    {
        base.Set();
        glassAnim.SetBool("full", false);
    }

    private void LateUpdate()
    {
        if (!glassAnim.GetBool("full"))
            return;

        if (isPour())
            glassAnim.SetBool("full", false);
    }

    public bool isFull()
    {
        return glassAnim.GetBool("full");
    }

    public bool isPour()
    {
        return (Mathf.Abs(transform.rotation.x) >= 0.2 || Mathf.Abs(transform.rotation.z) >= 0.2);
    }
}
