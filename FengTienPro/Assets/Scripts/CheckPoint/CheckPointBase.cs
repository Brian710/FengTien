using UnityEngine;
using UnityEngine.Events;

public class CheckPointBase : MonoBehaviour
{
    [SerializeField]    protected ParticleSystem FX;
    [SerializeField]    protected UnityEvent onTriggerEnter;

    public virtual void Start()
    {
        ShowParticle(false);
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
