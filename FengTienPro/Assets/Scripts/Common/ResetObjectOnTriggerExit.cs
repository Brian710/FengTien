using UnityEngine;
using System.Collections;

public class ResetObjectOnTriggerExit : MonoBehaviour
{
    public GameObject effectTarget;
    public float delay = 1.0f;

    private Vector3 originPosition;
    private Quaternion originRotation;
    private void Start()
    {
        originPosition = effectTarget.transform.localPosition;
        originRotation = effectTarget.transform.localRotation;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == effectTarget)
        {
            StopAllCoroutines();
            StartCoroutine(CopyTarget());
        }
    }

    private IEnumerator CopyTarget()
    {
        yield return new WaitForSeconds(delay);

        effectTarget.transform.localPosition = originPosition;
        effectTarget.transform.localRotation = originRotation;
    }
}
