using UnityEngine;
public class HotWaterController : MonoBehaviour
{
    [SerializeField]    private ParticleSystem partSys;
    [SerializeField]    private GlassController glassController;
    [SerializeField]    private InteractHover hover;

    private void Start()
    {
        hover.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        hover.ShowInteractColor(true);
        if(glassController == null)
            glassController = other.GetComponent<GlassController>();

        if (glassController != null&& !glassController.isFull())
        {
            glassController.doFull(true);
            ParticlePlay(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        ParticlePlay(false);
        hover.ShowInteractColor(false);
    }

    public void ParticlePlay(bool value)
    {
        if (value)
        {
            partSys.Play(true);
            AudioManager.Instance.Play("Water_fall");
        }
        else
        {
            partSys.Stop(true);
        }
    }
}
