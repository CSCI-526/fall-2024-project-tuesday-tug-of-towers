using UnityEngine;
using UnityEngine.UI;
using System.Reflection;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class DisplayVariable : MonoBehaviour
{
    public enum InfoType { SYSTEM_INFO, RESOURCE_INFO, STATISTIC_INFO};
    public InfoType infoType;

    [Tooltip("Only required when InfoType is set to RESOURCE_INFO. Specifies which resource to display.")]
    public string variableName;

    private GameVariables gameVariables;
    private Text displayText;
    private TextMeshProUGUI displayTMP;

    void Start()
    {
        gameVariables = GameObject.Find("Variables").GetComponent<GameVariables>();
        displayText = GetComponent<Text>();
        displayTMP = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (infoType == InfoType.SYSTEM_INFO || infoType == InfoType.RESOURCE_INFO || infoType ==  InfoType.STATISTIC_INFO)
        {
            KeyValuePair<Info, FieldInfo>? variable = gameVariables.GetVariable(variableName);
            try 
            {
                if (displayText != null) { displayText.text = variable.Value.Value.GetValue(variable.Value.Key).ToString(); }
                else if (displayTMP != null) { displayTMP.text = variable.Value.Value.GetValue(variable.Value.Key).ToString(); }

            }
            catch { Debug.Log($"Display: Invalid Variable {variableName}"); }
        }
        //else if (infoType == InfoType.STATISTIC_INFO)
        //{
        //    string newText = "";
        //    FieldInfo[] fields = gameVariables.statisticsInfo.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
        //    foreach (FieldInfo field in fields)
        //        newText += $"{field.Name}: {field.GetValue(gameVariables.statisticsInfo)}\n";
        //    displayText.text = newText;
        //}
    }
}
