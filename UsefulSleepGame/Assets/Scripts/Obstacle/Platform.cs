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
                other.transform.SetParent(transform);
                PlayerManager.isOnPlatform = true;
            }
        }

        virtual protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.name == "Player")
            {
                other.transform.parent = null;
                PlayerManager.isOnPlatform = false;
            }
        }
    }
}
