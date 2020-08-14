using UnityEngine;

public class GlassController : GrabObj
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

        if(Mathf.Abs(transform.rotation.x) >= 0.3 ||Mathf.Abs(transform.rotation.z) >= 0.3)
            glassAnim.SetBool("full", false);
    }
}
