using UnityEngine;

public class TutoCheckPoint : CheckPointBase
{
    public override void Start()
    {
        ShowParticle(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        TutoObj tutoObj = other.gameObject.GetComponent<TutoObj>();

        if (tutoObj == null)
        {
            onTriggerEnter.Invoke();
        }
    }


    public override void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody)
            other.attachedRigidbody.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }
}
