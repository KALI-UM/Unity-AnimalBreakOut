
using System.Collections.Generic;
using System.Drawing;
using UnityEditor;
using UnityEngine;


public class GoogleSheetCSVWindow : EditorWindow
{
    private string fileName;
    private string sheetURL;

    [MenuItem("Window/GoogleSheet CSV Window")]
    public static void ShowWindow()
    {
        GetWindow<GoogleSheetCSVWindow>();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("GoogleSheet URL");
        sheetURL = GUILayout.TextField(sheetURL);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Load"))
        {
            string path = EditorUtility.SaveFilePanel("Save GoogleSheet to CSV", "", "", "csv");

            if (!string.IsNullOrEmpty(path))
            {
                GoogleSheetManager.Load(sheetURL, path);
                AssetDatabase.Refresh();
            }
        }

        GUILayout.Label("");

        LoadDataTableGUI(animalDataTableURL, Utils.AnimalTableName);

        LoadDataTableGUI(itemDataTableURL, Utils.ItemTableName);

        LoadDataTableGUI(attackSkillDataTableURL, Utils.AttackSkillTableName);
        GUILayout.BeginHorizontal();
        GUILayout.Label("AttackSkill Scriptable");
        if (GUILayout.Button("CreateScriptable"))
        {
            CreateAttackSkillScriptableData();
        }
        GUILayout.EndHorizontal();


        LoadDataTableGUI(supportSkillDataTableURL, Utils.SupportSkillTableName);
        GUILayout.BeginHorizontal();
        GUILayout.Label("SupportSkill Scriptable");
        if (GUILayout.Button("CreateScriptable"))
        {
            CreateSupportSkillScriptableData();
        }
        GUILayout.EndHorizontal();


        LoadDataTableGUI(additionalStatusEffectDataTableURL, Utils.AdditionalStatusEffectTableName);

    }

    private void UpdateDataTableCSV(string url, string path)
    {
        GoogleSheetManager.Load(url, path);
        AssetDatabase.Refresh();
    }

    private string animalDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1280379651#gid=1280379651";
    private string itemDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=100321918#gid=100321918";

    private string attackSkillDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=420672475#gid=420672475";
    private string supportSkillDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=831221530#gid=831221530";
    private string additionalStatusEffectDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1871340178#gid=1871340178";

    //private void UpdateAnimalDataTable()
    //{
    //    var path = System.IO.Path.Combine(Application.dataPath, "Resources/") + string.Format(DataTable.FormatPath, Utils.AnimalTableName) + ".csv";

    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label(Utils.AnimalTableName);
    //    if (GUILayout.Button("Load"))
    //    {
    //        UpdateDataTableCSV(animalDataTableURL, path);
    //    }
    //    GUILayout.EndHorizontal();
    //}

    private void LoadDataTableGUI(string url, string dataTableFileName)
    {
        var path = System.IO.Path.Combine(Application.dataPath, "Resources/") + string.Format(DataTable.FormatPath, dataTableFileName) + ".csv";

        GUILayout.BeginHorizontal();
        GUILayout.Label(dataTableFileName);
        if (GUILayout.Button("Load"))
        {
            UpdateDataTableCSV(url, path);
        }
        GUILayout.EndHorizontal();
    }

    private void CreateAttackSkillScriptableData()
    {
        var attackSkillDataTable = new AttackSkillDataTable();
        attackSkillDataTable.Load(Utils.AttackSkillTableName);

        string dataPath = "Assets/Resources/ScriptableData/Skill/{0}.asset";
        string dataFileNameFormat = "Skill_Attack{0}";
        foreach (var key in attackSkillDataTable.Keys)
        {
            var data = attackSkillDataTable.Get(key);
            string dataFileName = string.Format(dataFileNameFormat, data.SkillID);
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(dataPath, dataFileName));

            //해당 데이터 ScriptableObject생성
            AttackSkillData scriptableData = ScriptableObject.CreateInstance<AttackSkillData>();
            scriptableData.SetData(data);

            AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, dataFileName));
            AssetDatabase.SaveAssets();
        }
    }

    private void CreateSupportSkillScriptableData()
    {
        var supportSkillDataTable = new SupportSkillDataTable();
        supportSkillDataTable.Load(Utils.SupportSkillTableName);

        string dataPath = "Assets/Resources/ScriptableData/Skill/{0}.asset";
        string dataFileNameFormat = "Skill_Support{0}";
        foreach (var key in supportSkillDataTable.Keys)
        {
            var data = supportSkillDataTable.Get(key);
            string dataFileName = string.Format(dataFileNameFormat, data.SupportID);
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(dataPath, dataFileName));

            //해당 데이터 ScriptableObject생성
            SupportSkillData scriptableData = ScriptableObject.CreateInstance<SupportSkillData>();
            scriptableData.SetData(data);

            AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, dataFileName));
            AssetDatabase.SaveAssets();
        }
    }

}

