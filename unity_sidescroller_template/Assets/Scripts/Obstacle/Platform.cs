using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        virtual protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                PlayerManager.player.transform.SetParent(transform);
            }
        }

        virtual protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                PlayerManager.player.transform.parent = null;
            }
        }
    }
}
