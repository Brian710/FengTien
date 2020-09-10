using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public List<LineRenderer> lineList;
    public Transform NormalTP;
    public Transform TestTransform;
    public Material mat;
    public float deltatime;
    public bool Done;
    private void Awake()
    {
        lineList = new List<LineRenderer>();
    }
    // Start is called before the first frame update

    private void Start()
    {
        //test
        CreatLine(this.transform.position, TestTransform.position);
    }
    public void CreatLine(Vector3 StartPos, Vector3 EndPos)
    {
        LineRenderer newline = new GameObject("Line").AddComponent<LineRenderer>();
        newline.material = mat;
        newline.startWidth = 1f;
        newline.endWidth = 1f;
        StartCoroutine(LoopLine(newline, StartPos, EndPos, deltatime));
        lineList.Add(newline);
    }

    public void CreatLine(bool IsStart, Vector3 Pos)
    {
        LineRenderer newline = new GameObject("Line").AddComponent<LineRenderer>();
        newline.material = mat;
        newline.startWidth = 1f;
        newline.endWidth = 1f;
        if(IsStart)
            StartCoroutine(LoopLine(newline, Pos, NormalTP.position, deltatime));
        else
            StartCoroutine(LoopLine(newline, NormalTP.position, Pos, deltatime));
        lineList.Add(newline);
    }

    private IEnumerator LoopLine(LineRenderer line,Vector3 StartPos, Vector3 EndPos,float deltatime)
    {
        Vector3 pos = StartPos;
        Vector3 delta = (EndPos - StartPos)*0.1f;

        while (Done)
        {
            yield return new WaitForSeconds(deltatime);
            if (Vector3.Distance(EndPos, pos) <= 0.5)
                pos = StartPos;

            Debug.LogWarning($"EndPos: {EndPos}, Pos: {pos}");
            line.SetPosition(0, pos += delta * Time.deltaTime);
            line.SetPosition(1, pos += delta * 1f);
        }
    }

    public void ClearLineList()
    {
        if (lineList.Count > 0||!Done)
        {
            Done = true;
            foreach (LineRenderer l in lineList)
            {
                Destroy(l.gameObject);
            }
            lineList.Clear();
            StopAllCoroutines();
        }
    }
}
