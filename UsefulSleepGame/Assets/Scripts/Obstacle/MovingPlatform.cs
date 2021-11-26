using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

namespace Platforms
{
    [Serializable]
    public class MovementProperties
    {
        public float speed = 40f;
        public float initialPause = 0;
    }

    public class MovingPlatform : Platform
    {
        public MovementProperties movementProperties;

        [SerializeField] private List<Vector3> localWaypoints;
        private List<Vector3> waypoints;

        private int currentIDX = 0;

        private void Awake()
        {
            waypoints = localWaypoints.Select(x => transform.TransformPoint(x)).ToList();
        }

        private void Start()
        {
            StartCoroutine(Move());
        }

        private int GetNextWaypoint(int idx)
        {
            return (idx + 1) % waypoints.Count;
        }

        IEnumerator Move()
        {
            yield return new WaitForSeconds(movementProperties.initialPause);

            while (true)
            {
                float dist = Vector3.Distance(waypoints[currentIDX], waypoints[GetNextWaypoint(currentIDX)]);
                float duration = dist / movementProperties.speed;

                Tween platformTween = transform.DOMove(waypoints[GetNextWaypoint(currentIDX)], duration).SetEase(Ease.InOutCubic);
                yield return platformTween.WaitForCompletion();

                currentIDX = GetNextWaypoint(currentIDX);
                yield return null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 1, 0, 0.75F);
            for (int idx = 0; idx < localWaypoints.Count; idx += 1)
            {
                Gizmos.DrawLine(transform.TransformPoint(localWaypoints[idx]), transform.TransformPoint(localWaypoints[(idx + 1) % localWaypoints.Count]));
                Gizmos.DrawSphere(transform.TransformPoint(localWaypoints[idx]), 1f);
            }
        }
    }
}
