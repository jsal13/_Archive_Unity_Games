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

    public override void SetValue(string variableName, float floatValue)
    {
        switch (variableName)
        {
            case "$attackPower":
                PlayerManager.Instance.AttackPower = (int)floatValue;
                break;
            case "$magicPower":
                PlayerManager.Instance.MagicPower = (int)floatValue;
                break;
            case "$gold":
                PlayerManager.Instance.Gold = (int)floatValue;
                break;
            default:
                break;
        }
    }

    public override void SetValue(string variableName, bool boolValue)
    {
        throw new System.NotImplementedException();
    }

    public override bool TryGetValue<T>(string variableName, out T result)
    {
        switch (variableName)
        {
            case "$gold":
                result = (T)(object)PlayerManager.Instance.Gold;
                return true;
            case "$attackPower":
                result = (T)(object)PlayerManager.Instance.AttackPower;
                return true;
            case "$magicPower":
                result = (T)(object)PlayerManager.Instance.MagicPower;
                return true;
            default:
                break;
        }
        result = (T)(object)0;
        return false;
    }
}