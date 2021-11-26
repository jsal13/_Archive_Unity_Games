using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class LocationManager
{
    private static readonly Lazy<LocationManager> lazy = new Lazy<LocationManager>(() => new LocationManager());
    public static LocationManager Instance { get { return lazy.Value; } }
    private LocationManager() { }

    public static TransitionPositions transitionPositions = new TransitionPositions(new List<TransitionPosition>() {
        new TransitionPosition("Mimas_Io_Village", "Mimas_Main", new Vector3(-54, 80, 0)),
        new TransitionPosition("Mimas_Village_2", "Mimas_Main", new Vector3(2710, 88, 0)),
        new TransitionPosition("Mimas_Village_3", "Mimas_Main", new Vector3(-9999, -9999, -9999)),
        new TransitionPosition("Mimas_Main", "Mimas_Io_Village", new Vector3(280, -4, 0)),
        new TransitionPosition("Mimas_Main", "Mimas_Village_2", new Vector3(-326, -6, 0)),
        new TransitionPosition("Mimas_Main", "Mimas_Village_3", new Vector3(-50, 384, 0)),

    });

    public class TransitionPosition
    {
        public string sourceSceneName;
        public string targetSceneName;
        public Vector3 pos;

        public TransitionPosition(string _sourceSceneName, string _targetSceneName, Vector3 _pos)
        {
            this.sourceSceneName = _sourceSceneName;
            this.targetSceneName = _targetSceneName;
            this.pos = _pos;
        }
    }

    public class TransitionPositions
    {
        public List<TransitionPosition> tpList;

        public TransitionPositions(List<TransitionPosition> _tpList)
        {
            this.tpList = _tpList;
        }

        public Vector3 GetTransitionPosition(string sourceSceneName, string targetSceneName)
        {
            foreach (TransitionPosition tp in tpList)
            {
                if (tp.sourceSceneName == sourceSceneName && tp.targetSceneName == targetSceneName)
                {
                    return (tp.pos);
                }
            }
            Debug.LogError($"No position found for {sourceSceneName}, {targetSceneName}.");
            return new Vector3(0, 0, 0);
        }

    }

}
