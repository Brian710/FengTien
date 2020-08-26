using HTC.UnityPlugin.Vive;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    [SerializeField]
    private Animator glassAnim;

    public BasicGrabbable grabFunc;

    private void LateUpdate()
    {
        if (!glassAnim.GetBool("full"))
            return;

        if (isPour())
        {
            glassAnim.SetBool("full", false);
        }
    }
    public void doFull()
    {
        glassAnim.SetBool("full", true);
        grabFunc.enabled = true;
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
