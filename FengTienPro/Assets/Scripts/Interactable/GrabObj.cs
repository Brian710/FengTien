using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MinYanGame.Core;

public class GrabObj : InteractableObjBase
{
    [SerializeField]
    private GrabObject grabData;

    public MeshFilter meshFilter;
    public MeshRenderer render;
    public MeshCollider colli;
    public Rigidbody rig;
    public BasicGrabbable grabFunc;
    private void Awake()
    {
        meshFilter.mesh = grabData.mesh;
        render.material = grabData.material;
        colli.sharedMesh = meshFilter.mesh;
        grabFunc.alignPositionOffset = grabData.alignPos;
        grabFunc.alignRotationOffset = grabData.alignRot;
        outline = gameObject.AddComponent<QuickOutline>();
    }
}
