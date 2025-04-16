using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedWorld : MonoBehaviour
{
    public float curvature;
    public float trimming;
    public Vector3 direction= Vector3.right;

    private void Awake()
    {
        Shader.SetGlobalFloat("_Curvature", 0);
        Shader.SetGlobalFloat("_Trimming", 0);
    }

    private void Start()
    {
        UpdateShaderValue();
    }

    [ContextMenu("Update Shader Value")]
    public void UpdateShaderValue()
    {
        Shader.SetGlobalVector("_CurvatureDirection", Vector3.right);
        Shader.SetGlobalFloat("_Curvature", curvature);
        Shader.SetGlobalFloat("_Trimming", trimming);
    }

    public void UpdateShaderDirection(Vector3 direction)
    {
        Shader.SetGlobalVector("_CurvatureDirection", direction);
    }

   
    private void Update()
    {
        ////var value = curvature * Mathf.Sin(Time.time);
        //Shader.SetGlobalFloat("_Curvature", curvature * Mathf.Sin(Time.time));
        //Shader.SetGlobalFloat("_Trimming", trimming);

        UpdateShaderDirection(direction);
    }
}
