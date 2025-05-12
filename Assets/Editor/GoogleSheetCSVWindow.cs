
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
                UpdateDataTableCSV(sheetURL, path);
            }
        }

        GUILayout.Label("");
        LoadDataTableGUI(stringTableURL, LocalizationUtility.defaultStringTableName);

        LoadDataTableGUI(animalDataTableURL, Utils.AnimalTableName);
        GUILayout.BeginHorizontal();
        GUILayout.Label("AnimalStat Scriptable");
        if (GUILayout.Button("CreateScriptable"))
        {
            CreateAnimalStatScriptableData();
        }
        GUILayout.EndHorizontal();

        LoadDataTableGUI(mapObjectDataTableURL, Utils.MapObjectsTableName);
        LoadDataTableGUI(itemDataTableURL, Utils.ItemTableName);
        LoadDataTableGUI(coinDataTableURL, Utils.RewardItemsTableName);

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
        LoadDataTableGUI(ingameLevelDataTableURL, Utils.InGameLevelExperienceValueTableName);
        LoadDataTableGUI(passiveEffectDataTableURL, Utils.PassiveEffectTableName);
        LoadDataTableGUI(playerLevelTableURL, Utils.PlayerLevelTableName);
        LoadDataTableGUI(enforceAnimalTableURL, Utils.EnforceAnimalTableName);
    }

    private void UpdateDataTableCSV(string url, string path)
    {
        GoogleSheetManager.Load(url, path);
    }

    private string stringTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=776232144#gid=776232144";

    private string animalDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1280379651#gid=1280379651";
    private string mapObjectDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=0#gid=0";
    private string itemDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=100321918#gid=100321918";
    private string coinDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=759094955#gid=759094955";
    private string attackSkillDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=420672475#gid=420672475";
    private string supportSkillDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=831221530#gid=831221530";
    private string additionalStatusEffectDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1871340178#gid=1871340178";
    private string ingameLevelDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=2005332401#gid=2005332401";
    private string passiveEffectDataTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=819605194#gid=819605194";
    private string playerLevelTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1878574882#gid=1878574882";
    private string enforceAnimalTableURL = "https://docs.google.com/spreadsheets/d/1lgeY8ZIuS4VGB0Ii2VdqcRd126eV1GDp4h0aw2hoVBA/edit?gid=1330250473#gid=1330250473";

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

    private void CreateAnimalStatScriptableData()
    {
        var animalStatDataTable = new AnimalDataTable();
        animalStatDataTable.Load(Utils.AnimalTableName);

        string dataPath = "Assets/Resources/ScriptableData/AnimalStat/{0}.asset";
        string dataFileNameFormat = "Animal_{0}";
        foreach (var key in animalStatDataTable.Keys)
        {
            var data = animalStatDataTable.Get(key);
            string dataFileName = string.Format(dataFileNameFormat, key);

            AnimalStatData scriptableData = AssetDatabase.LoadAssetAtPath<AnimalStatData>(string.Format(dataPath, dataFileName));
            //AssetDatabase.DeleteAsset(string.Format(dataPath, dataFileName));
            if (scriptableData == null)
            {
                //해당 데이터 ScriptableObject생성
                scriptableData = ScriptableObject.CreateInstance<AnimalStatData>();
                scriptableData.SetData(data);
                AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, dataFileName));
            }
            else
            {
                scriptableData.SetData(data);
            }
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(scriptableData);
        }
        AssetDatabase.Refresh();
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

            AttackSkillData scriptableData = AssetDatabase.LoadAssetAtPath<AttackSkillData>(string.Format(dataPath, dataFileName));
            //AssetDatabase.DeleteAsset(string.Format(dataPath, dataFileName));
            if (scriptableData == null)
            {
                //해당 데이터 ScriptableObject생성
                scriptableData = ScriptableObject.CreateInstance<AttackSkillData>();
                scriptableData.SetData(data);
                AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, dataFileName));
            }
            else
            {
                scriptableData.SetData(data);
            }
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(scriptableData);
        }
        AssetDatabase.Refresh();
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

            SupportSkillData scriptableData = AssetDatabase.LoadAssetAtPath<SupportSkillData>(string.Format(dataPath, dataFileName));
            //AssetDatabase.DeleteAsset(string.Format(dataPath, dataFileName));
            if (scriptableData == null)
            {
                //해당 데이터 ScriptableObject생성
                scriptableData = ScriptableObject.CreateInstance<SupportSkillData>();
                scriptableData.SetData(data);
                AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, dataFileName));
            }
            else
            {
                scriptableData.SetData(data);
            }
            AssetDatabase.SaveAssets();
            EditorUtility.SetDirty(scriptableData);
        }
        AssetDatabase.Refresh();
    }

}


