using UnityEngine;

public class ClickDragController : MonoBehaviour
{
    public Potion potion;
    private bool isDragging;
    private Vector3 originalPos;
    private bool isOverMixingContainer;

    public delegate void OnPourIntoMixingContainer(Potion potion);
    public static event OnPourIntoMixingContainer PourIntoMixingContainer;

    private void Awake()
    {
        originalPos = transform.position;

    }

    public void OnMouseDown()
    {
        if (!GameManager.pauseInput)
        {
            isDragging = true;
        }
    }

    public void OnMouseUp()
    {
        if (!GameManager.pauseInput)
        {
            isDragging = false;

            if (isOverMixingContainer)
            {
                PourIntoMixingContainer?.Invoke(potion);
            }
            transform.position = originalPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MixingContainer"))
        {
            isOverMixingContainer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MixingContainer"))
        {
            isOverMixingContainer = false;
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
    }
}