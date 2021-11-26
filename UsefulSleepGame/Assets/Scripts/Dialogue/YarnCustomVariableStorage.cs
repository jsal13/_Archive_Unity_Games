using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using Yarn.Unity;

public class YarnCustomVariableStorage : Yarn.Unity.VariableStorageBehaviour
{
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    public override void SetValue(string variableName, string stringValue)
    {
        throw new System.NotImplementedException();
    }

    public override void SetValue(string variableName, bool boolValue)
    {
        throw new System.NotImplementedException();
    }

    public override void SetValue(string variableName, float floatValue)
    {
        switch (variableName)
        {
            case "$Coin":
                PlayerManager.Coin = (int)floatValue;
                break;
            case "$Wood":
                PlayerManager.Wood = (int)floatValue;
                break;
            case "$Stone":
                PlayerManager.Stone = (int)floatValue;
                break;
            case "$Wool":
                PlayerManager.Wool = (int)floatValue;
                break;
            default:
                break;
        }
    }

    public override bool TryGetValue<T>(string variableName, out T result)
    {
        switch (variableName)
        {
            case "$Coin":
                result = (T)(object)PlayerManager.Coin;
                return true;
            case "$Wood":
                result = (T)(object)PlayerManager.Wood;
                return true;
            case "$Stone":
                result = (T)(object)PlayerManager.Stone;
                return true;
            case "$Wool":
                result = (T)(object)PlayerManager.Wool;
                return true;
            default:
                break;
        }
        result = (T)(object)0;
        return false;
    }
}