using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpawnObjOnTriggerExit : MonoBehaviour
{
    public List<GameObject> OriTargetlist;
    public float delay = 1.0f;

    private Dictionary<GameObject, Vector3> originPositionlist;
    private Dictionary<GameObject, Quaternion> originRotationlist;


    private void Awake()
    {
        if (OriTargetlist.Count <= 0)
            return;
        originPositionlist = new Dictionary<GameObject, Vector3>();
        originRotationlist = new Dictionary<GameObject, Quaternion>();

        foreach (GameObject obj in OriTargetlist)
        {
            originPositionlist.Add(obj, obj.transform.localPosition);
            originRotationlist.Add(obj, obj.transform.localRotation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject obj in OriTargetlist)
        {
            if (other.gameObject == obj)
            {
                //StopAllCoroutines();
                StartCoroutine(ReLocTarget(other.gameObject));
            }
        }
    }

    private IEnumerator ReLocTarget(GameObject obj)
    {
        yield return new WaitForSeconds(delay);

        obj.transform.localPosition = originPositionlist[obj];
        obj.transform.localRotation = originRotationlist[obj];
    }

    public void ForcetoReset(GameObject other)
    {
        foreach (GameObject obj in OriTargetlist)
        {
            if (other == obj)
            {
                //StopAllCoroutines();
                StartCoroutine(ReLocTarget(other));
            }
        }
    }
}
