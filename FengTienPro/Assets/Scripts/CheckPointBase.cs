using UnityEngine;
using UnityEngine.Events;

public class CheckPointBase : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem particle;
    [SerializeField]
    protected float force = 9.8f;
    [SerializeField]
    protected UnityEvent onTriggerEnter;

    public virtual void Start()
    {
        if (particle.isPlaying)
            particle.Stop();
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
        if (value) particle.Play(true);
        else particle.Stop(true);
    }
}
