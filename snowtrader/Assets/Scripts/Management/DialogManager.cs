using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public static class DialogManager
{
    private static string json;
    private static Dictionary<string, List<List<string>>> dialogDict;

    private static void LoadData()
    {
        json = Resources.Load<TextAsset>("Data/Dialogs").text;
        dialogDict = JsonConvert.DeserializeObject<Dictionary<string, List<List<string>>>>(json);
    }

    public static List<List<string>> GetDialog(string targetName)
    {
        if (dialogDict == null) LoadData();
        return dialogDict[targetName];
    }
}