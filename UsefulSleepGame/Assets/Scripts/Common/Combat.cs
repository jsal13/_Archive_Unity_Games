using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public static IEnumerator FlashRed(SpriteRenderer sprite, float duration)
    {
        float t = 0;
        while (t < duration / 4)
        {
            t += Time.deltaTime;
            sprite.color = Color.Lerp(Color.white, Color.red, t);
            yield return null;
        }

        t = 0;
        while (t < 3 * duration / 4)
        {
            t += Time.deltaTime;
            sprite.color = Color.Lerp(Color.red, Color.white, 2 * t / duration);
            yield return null;
        }
        sprite.color = Color.white;
    }


}