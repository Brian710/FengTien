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
    public void CreatLine()
    {
        if (TFList.Count == 0)
            return;

        Clear = false;
        line.enabled = true;
        line.material = mat;
        line.startWidth = 1f;
        line.endWidth = 1f;
        line.textureMode = LineTextureMode.Tile;
        if (Vector3.Distance(TFList[0].position, TFList[TFList.Count - 1].position) <= Vector3.Distance(TFList[0].position, TFList[1].position))
            TFList.Remove(TFList[1]);

        line.positionCount = TFList.Count;
        for (int i = 0; i < TFList.Count; i++)
        {
            line.SetPosition(i, TFList[i].position += Vector3.up * 0.3f);
        }
        StartCoroutine(LoopLine(deltatime));
    }

    public void AddList(Transform MainScene)
    {
        if (TFList.Contains(MainScene))
            return;

        TFList.Add(MainScene);
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
