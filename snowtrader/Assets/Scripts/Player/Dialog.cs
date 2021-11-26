 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private List<Collider2D> collisions;
    private GameObject dialogTarget;
    private NPCController.NPC targetNPCInfo;

    private TMP_Text guiSpeakerName;
    private Image guiSpeakerImage;
    private TMP_Text guiDialog;

    private Queue<string> sentences;

    private bool _isSpeaking;
    public bool IsSpeaking
    {
        get => _isSpeaking;
        set
        {
            _isSpeaking = value;
            OnPlayerSpeaking?.Invoke(_isSpeaking);
        }
    }

    [SerializeField] private bool canSpeak = true;

    public delegate void PlayerSpeaking(bool value);
    public static event PlayerSpeaking OnPlayerSpeaking;

    public delegate void PlayerTrading(NPCController.NPC targetInfo);
    public static event PlayerTrading OnPlayerTrading;

    public delegate void PlayerNearSpeaker(bool val);
    public static event PlayerNearSpeaker OnPlayerNearSpeaker;

    private void Awake()
    {
        guiSpeakerName = GameObject.Find(HierarchyAddrs.dialogBoxCharacterName).GetComponent<TMP_Text>();
        guiSpeakerImage = GameObject.Find(HierarchyAddrs.dialogBoxCharacterImage).GetComponent<Image>();
        guiDialog = GameObject.Find(HierarchyAddrs.dialogBoxDialog).GetComponent<TMP_Text>();
        collisions = new List<Collider2D>();
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Interactable") || collision.CompareTag("Loom")) && !collisions.Contains(collision))
        {
            collisions.Add(collision);
            OnPlayerNearSpeaker?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.CompareTag("Interactable") || collision.CompareTag("Loom")))
        {
            collisions.Remove(collision);
            if (collisions.Count == 0)
            {
                OnPlayerNearSpeaker?.Invoke(false);
            }
        }
    }

    private void Update()
    {
        if (InputManager.gamepad.buttonWest.wasPressedThisFrame && collisions.Count > 0 && !IsSpeaking && canSpeak)
        {
            dialogTarget = collisions[0].gameObject;
            IsSpeaking = true;
            canSpeak = false;
            StartDialog();
        }
        else if (InputManager.gamepad.buttonWest.wasPressedThisFrame && IsSpeaking)
        {
            DisplayNextSentence();
        }
    }

    private void StartDialog()
    {
        targetNPCInfo = dialogTarget.GetComponent<NPCController>().NPCInfo;

        List<List<string>> dialogChoices = DialogManager.GetDialog(targetNPCInfo.dialogType);
        var idx = Random.Range(0, dialogChoices.Count);
        sentences = new Queue<string>(dialogChoices[idx]);

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        // Sentence is like "Speaker|Text Goes Here...".  Split this on |.
        string txt = sentences.Dequeue();
        string[] txtSplit = txt.Split('|');
        string speaker = txtSplit[0];
        string sentence = txtSplit[1];

        guiSpeakerImage.sprite = Resources.Load<Sprite>(GetCharacterImage(speaker));
        guiSpeakerName.text = speaker;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        guiDialog.text = "";
        yield return new WaitForSeconds(0.1f);
        foreach (char letter in sentence.ToCharArray())
        {
            guiDialog.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private string GetCharacterImage(string speaker) => "Images/Characters/Icons/" + speaker;

    private void EndDialog()
    {
        IsSpeaking = false;
        canSpeak = true;
        
        if (!string.IsNullOrWhiteSpace(targetNPCInfo.inventoryType))
        {
            OnPlayerTrading?.Invoke(targetNPCInfo);
        }
    }

    private void OnEnable()
    {
        OnPlayerTrading += HandlePlayerTrading;
        Trade.OnTradeMenuEnd += HandleTradeMenuEnd;
    }

    private void OnDisable()
    {
        OnPlayerTrading -= HandlePlayerTrading;
        Trade.OnTradeMenuEnd -= HandleTradeMenuEnd;
    }

    private void HandleTradeMenuEnd()
    {
        canSpeak = true;
    }

    private void HandlePlayerTrading(NPCController.NPC _)
    {
        canSpeak = false;
    }
}
