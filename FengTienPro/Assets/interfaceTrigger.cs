using UnityEngine;

public class interfaceTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.GetComponent<IWashable>());
    }
}
