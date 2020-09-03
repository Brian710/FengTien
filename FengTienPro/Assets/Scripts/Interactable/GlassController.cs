using HTC.UnityPlugin.Vive;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    [SerializeField]    private Animator glassAnim;
    public BasicGrabbable grabFunc;

    private void Start()
    {
        doFull(true);
    }

    public void doFull(bool value)
    {
        glassAnim.SetBool("full", value);
        grabFunc.enabled = value;
    }
    public bool isFull()
    {
        return glassAnim.GetBool("full");
    }
    
}
