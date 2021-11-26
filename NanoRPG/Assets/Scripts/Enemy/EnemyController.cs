using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IAttackable
{
    public EnemyInfo enemyInfo;
    private SpriteRenderer sprite;
    private Color originalColor;
    private List<Vector3> directions;
    private bool isWalking;
    private int obstructionBitMask;
    public int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            if (_currentHealth <= 0)
            {
                OnEnemyDeath?.Invoke(transform.position, enemyInfo.goldValue);
                Destroy(gameObject);
            }
        }
    }

    [SerializeField] private Color hitColor = Color.red;

    public delegate void EnemyDeath(Vector3 pos, int goldValue);
    public static EnemyDeath OnEnemyDeath;

    private void Awake()
    {

        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        directions = new List<Vector3>() {
            Vector3.left,
            Vector3.right,
            Vector3.up,
            Vector3.down
        };

        obstructionBitMask = GameManager.layerDict["Obstruction"] | GameManager.layerDict["Enemy"] | GameManager.layerDict["Character"];
    }

    private void Start()
    {
        isWalking = false;
        CurrentHealth = enemyInfo.health;
    }

    private void Update()
    {
        if (!isWalking && PlayerManager.Instance.CanMove)
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        // Initial pause.
        isWalking = true;
        yield return new WaitForSeconds(2 * enemyInfo.movementPause);
        int numSteps = Random.Range(1, 5);

        for (int idx = 0; idx <= numSteps; idx += 1)
        {
            int randDirIdx = Random.Range(0, 4);
            Vector3 startPos = transform.position;
            Vector3 targetPos = startPos + (16 * directions[randDirIdx]);
            float t = 0;

            if (!IsObstructed((targetPos - startPos).normalized))
            {
                while (Vector2.Distance(targetPos, transform.position) != 0)
                {
                    t += Time.deltaTime;
                    transform.position = Vector2.Lerp(startPos, targetPos, Mathf.Clamp(t * 1, 0, 1));
                    yield return null;
                }
            }
        }
        isWalking = false;
        yield return null;
    }

    private bool IsObstructed(Vector3 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, 16, obstructionBitMask);
        return hits.Length > 0;
    }

    public void decreaseHealth(int value)
    {
        StartCoroutine(CombatFunctions.Instance.FlashHurt(gameObject));
        CurrentHealth -= value;
    }
}

