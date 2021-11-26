using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class VectorHelpers
{
    private static readonly Lazy<VectorHelpers> lazy = new Lazy<VectorHelpers>(() => new VectorHelpers());
    public static VectorHelpers Instance { get { return lazy.Value; } }
    private VectorHelpers() { }

    public delegate void Rotating(bool value, float degrees);
    public static Rotating OnRotating;

    public Vector3 VectorToIntValues(Vector3 vec, int toPlace = 0)
    {
        return new Vector3(RoundToPlace(vec.x, toPlace), RoundToPlace(vec.y, toPlace), RoundToPlace(vec.z, toPlace));
    }

    public int RoundToPlace(float num, int toPlace = 0)
    {
        int powerTen = (int)Mathf.Pow(10, toPlace);
        return (int)(powerTen * Mathf.Round(num / powerTen));
    }

    public IEnumerator RotateDegrees(Transform trans, float degrees, float speed)
    {
        OnRotating?.Invoke(true, degrees);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            trans.RotateAround(Vector3.zero, Vector3.forward, degrees * Time.deltaTime * speed);
            yield return null;
        }
        trans.localEulerAngles = VectorHelpers.Instance.VectorToIntValues(trans.localEulerAngles, 1);

        OnRotating?.Invoke(false, degrees);

    }

}