using UnityEngine;
public class HotWaterController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem partSys;
    [SerializeField]
    private Animator glassAnim;
    [SerializeField]
    private Transform glassPos;
    [SerializeField]
    private GlassController glassController;
    [SerializeField]
    private InteractHover hover;

    private Vector3 Pos;
    private Quaternion Rot;
    private void Start()
    {
        Pos = glassPos.position;
        Rot = glassPos.rotation;
        hover.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        hover.enabled = true;
        hover.ShowInteractColor(true);
        glassController = other.GetComponent<GlassController>();
        if (glassController != null)
        {
            if (!glassController.isFull())
            {
                glassController.grabFunc.enabled = false;
                other.transform.position = Pos;
                other.transform.rotation = Rot;
                ParticlePlay();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        hover.enabled = false;
        hover.ShowInteractColor(false);
    }

    public void ParticlePlay()
    {
        if (glassAnim.GetBool("full") || partSys.isPlaying)
            return;

        partSys.Play(true);
        glassController.doFull(true);
        AudioManager.Instance.Play("Water_fall");
    }
}
