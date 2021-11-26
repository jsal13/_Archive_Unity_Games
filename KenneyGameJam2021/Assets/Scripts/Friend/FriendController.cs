using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendController : MonoBehaviour
{
    [SerializeField] private FriendInfo friendInfo;
    private int healthRestore;
    private int attackAdd;
    private int goldAdd;

    private void Awake()
    {
        healthRestore = friendInfo.healthRestore;
        attackAdd = friendInfo.attackAdd;
        goldAdd = friendInfo.goldAdd;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Should the user have to tap a button or something?
        if (other.gameObject.name == "Player")
        {
            PlayerManager.Instance.Health += healthRestore;
            PlayerManager.Instance.AttackPower += attackAdd;
            PlayerManager.Instance.Gold += goldAdd;
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

}

