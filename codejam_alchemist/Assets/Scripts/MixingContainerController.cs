using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingContainerController : MonoBehaviour
{
    private bool isDragging;
    private Vector3 originalMixingContainerPos;

    private bool isOverBowl;
    public bool canPourIntoBowl = true;

    public delegate void OnPourIntoBowl(List<Potion> potionList);
    public static event OnPourIntoBowl PourIntoBowl;

    [System.Serializable]
    public class MixedLiquid
    {
        public GameObject go;
        public Color _color;
        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                go.GetComponent<SpriteRenderer>().color = _color;
            }
        }
        public bool isFilled;

        public MixedLiquid(GameObject go, Color color, bool isFilled)
        {
            this.go = go;
            this.Color = color;
            this.isFilled = isFilled;
        }
    }

    private MixedLiquid mixture1;
    private MixedLiquid mixture2;
    private MixedLiquid mixture3;
    public List<MixedLiquid> mixedLiquidsList;
    public List<Potion> potionList;
    public int numPouredPotions;

    private void Awake()
    {
        originalMixingContainerPos = transform.position;
        mixture1 = new MixedLiquid(transform.Find("Color1").gameObject, Color.clear, false);
        mixture2 = new MixedLiquid(transform.Find("Color2").gameObject, Color.clear, false);
        mixedLiquidsList = new List<MixedLiquid>() { mixture1, mixture2, mixture3 };
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }

    private void OnEnable()
    {
        ClickDragController.PourIntoMixingContainer += HandlePourIntoMixingContainer;
        BowlController.BowlFull += HandleBowlFull;
        LevelController.NewRelic += HandleNewRelic;
    }

    private void OnDisable()
    {
        ClickDragController.PourIntoMixingContainer -= HandlePourIntoMixingContainer;
        BowlController.BowlFull -= HandleBowlFull;
        LevelController.NewRelic -= HandleNewRelic;
    }

    private void HandleNewRelic()
    {
        ResetMixingContainerColors();
    }

    private void HandleBowlFull(bool value) => canPourIntoBowl = !value;

    private void HandlePourIntoMixingContainer(Potion potion)
    {
        if (potionList.Count < 2)
        {
            foreach (MixedLiquid ptn in mixedLiquidsList)
            {
                if (!ptn.isFilled)
                {
                    ptn.Color = potion.color;
                    ptn.isFilled = true;
                    potionList.Add(potion);
                    break;
                }
            }
        }
        else
        {
            // TODO: Make this obvious to the user.
            Debug.Log("Can't put any more potions in!");
        }
    }

    private void OnMouseOver()
    {
        // Right click to empty bowl.
        if (Input.GetMouseButtonDown(1))
        {
            ResetMixingContainerColors();
        }
    }

    public void OnMouseDown()
    {
        if (potionList.Count > 0 && potionList.Count <= 2 && !GameManager.pauseInput)
        {
            isDragging = true;
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.pauseInput)
        {
            isDragging = false;

            if (isOverBowl && canPourIntoBowl)
            {
                PourIntoBowl?.Invoke(potionList);
                ResetMixingContainerColors();
            }
            transform.position = originalMixingContainerPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bowl"))
        {
            isOverBowl = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bowl"))
        {
            isOverBowl = false;
        }
    }

    public void ResetMixingContainerColors()
    {
        mixture1 = new MixedLiquid(transform.Find("Color1").gameObject, Color.clear, false);
        mixture2 = new MixedLiquid(transform.Find("Color2").gameObject, Color.clear, false);
        mixedLiquidsList = new List<MixedLiquid>() { mixture1, mixture2 };
        potionList = new List<Potion>();
    }
}
