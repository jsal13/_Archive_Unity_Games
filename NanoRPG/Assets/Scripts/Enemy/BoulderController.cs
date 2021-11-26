using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderController : MonoBehaviour, IProjectile
{
    public int AttackPower { get; set; } = 1;
    [SerializeField] private int speed = 50;
    [SerializeField] private float timeBeforeDisappear = 5f;
    [SerializeField] private float currentTime = 0f;
    [SerializeField] private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir;
    }

    void Update()
    {
        if (currentTime < timeBeforeDisappear)
        {
            currentTime += Time.deltaTime;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerManager.Instance.Health -= AttackPower;
            StartCoroutine(CombatFunctions.Instance.FlashHurt(other.gameObject));
            Destroy(this.gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Obstruction"))
        {
            Destroy(this.gameObject);
        }
    }
}
