using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using UnityEngine;

public interface Info { }

public class SystemInfo : Info
{
    public string currentTimeString = "00:01:00";
    public int pause = 0; // 0 means continue, 1 means pause
    public string pauseShow = "Paused";

}

public class ResourcesInfo : Info
{
    public int attackMoney = 100;
    public int defenseMoney = 100;
    public int defenseLife = 100;
}

public class StatisticsInfo : Info
{

}

public class GameVariables : MonoBehaviour
{
    public SystemInfo systemInfo;
    public ResourcesInfo resourcesInfo;
    public StatisticsInfo statisticsInfo;

    private GameObject systems;

    private void Start()
    {
        systemInfo = new SystemInfo();
        resourcesInfo = new ResourcesInfo();
        statisticsInfo = new StatisticsInfo();

        systems = GameObject.Find("IndependentSystems");
        systems.GetComponent<TimeSystem>().Init();
        //systems.GetComponent<MapSystem>().Init();
        //systems.GetComponent<ConstructionSystem>().Init();
    }

    // ? is guided by chatGPT
    public KeyValuePair<Info, FieldInfo>? GetVariable(string variableName)
    {
        FieldInfo field;
        field = systemInfo.GetType().GetField(variableName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null)
            return new KeyValuePair<Info, FieldInfo>(systemInfo, field);
        field = resourcesInfo.GetType().GetField(variableName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null)
            return new KeyValuePair<Info, FieldInfo>(resourcesInfo, field);
        field = statisticsInfo.GetType().GetField(variableName, BindingFlags.Public | BindingFlags.Instance);
        if (field != null)
            return new KeyValuePair<Info, FieldInfo>(statisticsInfo, field);
        return null;
    }
}
