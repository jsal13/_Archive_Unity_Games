using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public sealed class CombatFunctions
{
    private static readonly Lazy<CombatFunctions> lazy = new Lazy<CombatFunctions>(() => new CombatFunctions());
    public static CombatFunctions Instance { get { return lazy.Value; } }
    private CombatFunctions() { }

    public IEnumerator FlashHurt(GameObject target, Color? originalColor = null, Color? hitColor = null)
    {
        SpriteRenderer sprite = target.GetComponent<SpriteRenderer>();

        float t = 0;
        float flashSpeed = 16f;

        while (t <= 1)
        {
            t += Time.deltaTime * flashSpeed;
            sprite.color = Color.Lerp(originalColor ?? Color.white, hitColor ?? Color.red, t);
            yield return null;
        }

        t = 0;
        while (t <= 1)
        {
            t += Time.deltaTime * flashSpeed;
            sprite.color = Color.Lerp(hitColor ?? Color.red, originalColor ?? Color.white, t);
            yield return null;
        }

        sprite.color = originalColor ?? Color.white;
        yield return null;
    }
}