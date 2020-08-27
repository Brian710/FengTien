using UnityEngine;
using UnityEngine.Events;

public class CheckPointBase : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem FX;
    [SerializeField]
    protected float force = 9.8f;
    [SerializeField]
    protected UnityEvent onTriggerEnter;

    public virtual void Start()
    {
        if (FX.isPlaying)
            FX.Stop();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        //defualt
    }

    public virtual void OnTriggerStay(Collider other)
    {
        //default
    }

    public void ShowParticle(bool value)
    {
        if (value) FX.Play(true);
        else FX.Stop(true);
    }
}
