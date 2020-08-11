using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentController : MonoBehaviour
{
    public float SkyRotationSpeed = 1.2f;

    // Update is called once per frame
    private void Awake()
    {
    }
    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * SkyRotationSpeed);
        DynamicGI.UpdateEnvironment();
    }
}
