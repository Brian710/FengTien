using HTC.UnityPlugin.Vive;
using UnityEngine;

public class GlassController : MonoBehaviour
{
    [SerializeField]    private Animator glassAnim;

    private void Start()
    {
        doFull(true);
    }

    public void doFull(bool value)
    {
        glassAnim.SetBool("full", value);
    }
    public bool isFull()
    {
        return glassAnim.GetBool("full");
    }
    
}
