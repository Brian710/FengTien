using HTC.UnityPlugin.Vive;
using UnityEngine;

public class TakeTubeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Finish_Tube;
    [SerializeField] private Transform Target_Transform;
    private bool GetTube;
    private void Start()
    {
        GetTube = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<VivePoseTracker>() && !GetTube)
        {
            other.gameObject.SetActive(false);
            Finish_Tube.SetActive(true);
            Finish_Tube.transform.SetParent(Target_Transform, false);
            Finish_Tube.transform.localPosition = new Vector3(0, -0.025f, 0);
            Finish_Tube.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            GetTube = true;
        }
    }
}
