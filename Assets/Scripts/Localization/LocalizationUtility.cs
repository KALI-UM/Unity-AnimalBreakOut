using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.ResourceManagement.AsyncOperations;


//"Korean (South Korea) (ko-KR)"
//"English (United States) (en-US)"


[ExecuteInEditMode]
public static class LocalizationUtility
{
    static private Coroutine coLocaleChange;
    static public string CurrentLocale
    {
        get => LocalizationSettings.SelectedLocale.LocaleName;
    }

    public const string defaultStringTableName = "StringTable";

    private static Dictionary<string, int> localeIndexTable = new();
    private static int defaultIndex = 0;

    static LocalizationUtility()
    {
        coLocaleChange = null;

        for (int index = 0; index < LocalizationSettings.AvailableLocales.Locales.Count; index++)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[index];
            localeIndexTable.Add(locale.LocaleName, index);
            Debug.Log(locale.LocaleName);
        }
        Debug.Log("현재 언어 : " + CurrentLocale);
    }

    //public static void ChangeLocale(string targetLocal)
    //{
    //    if (coLocaleChange != null || CurrentLocale == targetLocal)
    //        return;

    //    coLocaleChange = StartCoroutine(LocaleChange(GetAvaliableLocaleIndex(targetLocal)));
    //}

    private static IEnumerator LocaleChange(string targetLocal)
    {
        if (coLocaleChange != null || CurrentLocale == targetLocal)
            yield break;

        int index = GetAvaliableLocaleIndex(targetLocal);
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];

        coLocaleChange = null;
    }

    private static int GetAvaliableLocaleIndex(string targetLocal)
    {
        if (!localeIndexTable.ContainsKey(targetLocal))
        {
            Debug.Log("존재하지 않는 언어 설정입니다.");
            return defaultIndex;
        }

        return localeIndexTable[targetLocal];
    }

    public static string GetLZString(string table, string key, params object[] args)
    {
        if (int.TryParse(key, out int number))
        {
            Debug.LogError($"String Id : {number} need to Change!");
        }

        LocalizedString lzString = new LocalizedString() { TableReference = table, TableEntryReference = key };
        return GetLZString(lzString, args);
    }

    public static string GetLZString(LocalizedString lzString, params object[] args)
    {
        var stringOperation = args != null ? lzString.GetLocalizedStringAsync(args) : lzString.GetLocalizedStringAsync();

        //Async가 끝날때까지 기다린다.
        stringOperation.WaitForCompletion();
        if (stringOperation.Status == AsyncOperationStatus.Succeeded)
        {
            return stringOperation.Result;
        }
        else
        {
            Debug.LogError($"GetLZString| stringOperation fail : {lzString.TableEntryReference} is not exist in {lzString.TableEntryReference}");
            return lzString.TableEntryReference;
        }
    }
    public static void ChangeLocaleNow(string localeName)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.LocaleName == localeName)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($"Changed Language to: {localeName}");
                return;
            }
        }
        Debug.LogWarning($"Locale '{localeName}' not found!");
    }

}