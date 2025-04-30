using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurvedWorld : MonoBehaviour
{
    //public float curvature;
    //public float trimming;

    public float maxHorizonCurvatureValue;
    public float maxVirticalCurvatureValue;

    public AmazingAssets.CurvedWorld.CurvedWorldController curvedWorldController;

    private void Awake()
    {
        //Shader.SetGlobalFloat("_Curvature", 0);
        //Shader.SetGlobalFloat("_Trimming", 0);
    }



    private void Start()
    {
        //Shader.EnableKeyword("CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE");
    }

    private void Update()
    {
        UpdateShaderValue();
    }

    [ContextMenu("Update Shader Value")]
    public void UpdateShaderValue()
    {
        curvedWorldController.SetBendHorizontalSize(Mathf.Sin(Time.time/5f) * maxHorizonCurvatureValue);
        //curvedWorldController.SetBendVerticalSize(Mathf.Sin(Time.time/5f)*maxVirticalCurvatureValue);
    }

    //[ContextMenu("X+")]
    //public void SetPositiveX()
    //{
    //    Shader.DisableKeyword("CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Z_POSITIVE");
    //    Shader.EnableKeyword("CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE");
    //}
    //[ContextMenu("Z+")]
    //public void SetPositiveZ()
    //{
    //    Shader.DisableKeyword("CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE");
    //    Shader.EnableKeyword("CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_Y_POSITIVE");
    //}
}
