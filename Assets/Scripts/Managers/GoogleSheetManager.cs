
#if UNITY_EDITOR

using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode]
public static class GoogleSheetManager
{

    public static readonly string googleSheetURLFront = "https://docs.google.com/spreadsheets/d/";
    public static readonly string sheetCSVFormat = googleSheetURLFront + "{0}/export?format=csv&gid={1}";
    public static readonly string gidKey = "#gid=";


    public static void Load<T>(string sheetURL, string path)
    {
        string csvURL = ConvertGoogleSheetURLToCSVURL(sheetURL);
        EditorCoroutineUtility.StartCoroutineOwnerless(LoadGoogleSheet<T>(csvURL, path));
    }

    public static void Load(string sheetURL, string path)
    {
        string csvURL = ConvertGoogleSheetURLToCSVURL(sheetURL);
        EditorCoroutineUtility.StartCoroutineOwnerless(LoadGoogleSheet(csvURL, path));
    }

    public static string ConvertGoogleSheetURLToCSVURL(string sheetURL)
    {
        int idLastIndex = sheetURL.IndexOf('/', googleSheetURLFront.Length);
        string id = sheetURL.Substring(googleSheetURLFront.Length, idLastIndex - googleSheetURLFront.Length);
        int gidLastIndex = sheetURL.LastIndexOf(gidKey);
        string gid = sheetURL.Substring(gidLastIndex + gidKey.Length);

        return string.Format(sheetCSVFormat, id, gid);
    }

    public static IEnumerator LoadGoogleSheet<T>(string sheetURL, string path)//, DataTable table)
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Err" + www.error);
        }
        else
        {
            string csv = www.downloadHandler.text;
            var list = DataTable.LoadCSV<T>(csv);
            DataTable.SaveCSV<T>(list, path);
        }
    }
    public static IEnumerator LoadGoogleSheet(string sheetURL, string path)//, DataTable table)
    {
        UnityWebRequest www = UnityWebRequest.Get(sheetURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Err" + www.error);
        }
        else
        {
            string csv = www.downloadHandler.text;
            DataTable.SaveCSV(csv, path);
        }
    }
}
#endif