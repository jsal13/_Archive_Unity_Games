using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    [Serializable]
    public class AttackProperties
    {
        public float slashRate = 10f;
        public float slashSpeed = 0.005f;
    }

    [SerializeField] private AttackProperties attackProperties;

    [HideInInspector] private SpriteRenderer swordSprite;
    [HideInInspector] private GameObject swordObj;
    [HideInInspector] private AttackPointController attackPointController;
    [SerializeField] private List<Collider2D> attackRangeObjects;

    [SerializeField] private InputAction attackAction;



    private bool isSlashing;

    private void OnEnable()
    {
        attackAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
    }

    private void Awake()
    {
        attackAction.performed += OnAttack;

        swordSprite = transform.Find("SwordSlash").GetComponent<SpriteRenderer>();
        attackPointController = transform.Find("AttackPoint").gameObject.GetComponent<AttackPointController>();
        swordSprite.color = new Color(1, 1, 1, 0);  // Make sword invisible.
    }

    private void OnAttack(InputAction.CallbackContext _)
    {
        if (!isSlashing)
        {
            StartCoroutine(AnimateSlash());
            StartCoroutine(ProcessAttack());
        }
    }

    IEnumerator ProcessAttack()
    {
        yield return new WaitForSeconds(attackProperties.slashSpeed);
        // Attack enemies, etc.
        foreach (GameObject other in attackPointController.objsInRange)
        {
            Debug.Log($"{other.name}: \"Ouch!\"");
        }
        yield return new WaitForSeconds(attackProperties.slashRate);
    }

    IEnumerator AnimateSlash()
    {
        isSlashing = true;
        float t = 0;
        while (t < attackProperties.slashSpeed)
        {
            t += Time.deltaTime;
            swordSprite.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), t / attackProperties.slashSpeed);
            yield return null;
        }

        t = 0;
        while (t * attackProperties.slashRate < 1)
        {
            t += Time.deltaTime;
            swordSprite.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), t * attackProperties.slashRate);
            yield return null;
        }

        isSlashing = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
