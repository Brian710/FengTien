using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimMainTP : MonoBehaviour
{

    public LineCreator lineCreator;
    public List<Transform> TPs;
    // Start is called before the first frame update
    void Start()
    {
        if (TPs.Count >= 2)
        {
            foreach (Transform tp in TPs)
            {
                tp.position+= Vector3.up*0.3f;
                lineCreator.AddList(tp);
            }
            lineCreator.CreatLine();

            StartCoroutine(Stop());
        }
    }


    IEnumerator Stop()
    {
        yield return new WaitForSeconds(30f);
        lineCreator.ClearLineList();
    }
}
