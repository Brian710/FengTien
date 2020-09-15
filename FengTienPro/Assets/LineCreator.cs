using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public LineRenderer line;
    public List<Transform> TFList;
    public Material mat;
    public float deltatime;
    public bool Clear;
    private void Awake()
    {
        TFList = new List<Transform>();
    }
    private void Start()
    {
        if (line != null)
            line = GetComponent<LineRenderer>();
    }
    public void CreatLine()
    {
        if (TFList.Count < 2)
            return;

        Clear = false;
        line.enabled = true;
        line.material = mat;
        line.startWidth = .5f;
        line.endWidth = .5f;
        line.textureMode = LineTextureMode.Tile;
        //printList();
        if (TFList.Count <= 2)
        {
            line.positionCount = TFList.Count;
            Vector3 pos = new Vector3();
            pos = TFList[0].position;
            line.SetPosition(1, pos += Vector3.up * 0.3f);
            pos = TFList[1].position;
            line.SetPosition(0, pos += Vector3.up * 0.3f);
            //Debug.LogError("Count two");
        }
        else 
        {
            if (Vector3.Distance(TFList[0].position, TFList[TFList.Count - 1].position) <= Vector3.Distance(TFList[0].position, TFList[1].position))
                TFList.Remove(TFList[1]);

            line.positionCount = TFList.Count;
            for (int i = 0; i < TFList.Count; i++)
            {
                Vector3 pos = new Vector3();
                pos = TFList[i].position;
                line.SetPosition(i, pos += Vector3.up * 0.3f);
            }
        }
        StartCoroutine(LoopLine(deltatime));
    }

    public void AddList(Transform MainScene)
    {
        if (TFList.Contains(MainScene))
        {
            //Debug.LogError("return" + MainScene.name);
            return;
        }

        //Debug.LogError($"Add {MainScene.name}");
        if (MainScene.name.Contains("Normal")&& TFList.Count >= 1)
            TFList.Insert(1, MainScene);
        else
            TFList.Add(MainScene);
    }

    public void AddList(Transform MainScene, int ForceIndex)
    {
        if (TFList.Contains(MainScene))
        {
            TFList.Remove(MainScene);
        }

        TFList.Insert(ForceIndex, MainScene);
    }

    private void printList()
    {
        foreach (Transform tf in TFList)
        {
            Debug.LogError($"Add {tf.name}");
        }
    }

    private IEnumerator LoopLine(float deltatime)
    {
        Vector2 offesetX = new Vector2(0, 0);
        while (!Clear)
        {
            yield return new WaitForSeconds(deltatime);
            mat.SetTextureOffset("_MainTex", offesetX += Vector2.left*0.3f);
        }
    }

    public void ClearLineList()
    {
        if (TFList.Count > 0||!Clear)
        {
            Clear = true;
            TFList.Clear();
            StopAllCoroutines();
            line.enabled = false;
        }
    }
}
