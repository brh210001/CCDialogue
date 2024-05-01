using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [Header("Assign Game Objects")]
    [SerializeField]
    private TextMeshProUGUI dialogueText;

    [SerializeField]
    private TextMeshProUGUI nameText;

    [SerializeField] 
    private GameObject exclamation;

    [SerializeField] private Image dialogueImage;
    
    [SerializeField] private Animator animator;
    public GameObject player;

    public GameObject npc;
    
    [Header("Input Dialogue")]
    [SerializeField]
    [TextArea(3, 10)]
    [Tooltip("Type your NPC dialogue here: Max of 158 characters; including spaces. Click the plus to add more lines.")]
    private string[] lines;

    [Header("Customize Dialogue")]
    [SerializeField]
    [Tooltip("This is how fast the dialogue will type; Negatives go faster, Positives go slower.")]
    private float textSpeed;
    
    [SerializeField]
    [Tooltip("Type your NPC name here.")]
    private string NPCName;

    [SerializeField][Tooltip("Set color of dialogue box")] 
    private Color textBoxColor;

    [SerializeField][Tooltip("Thid is how far they player must be before NPC notices them")]
    private float rangeOfAwareness;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = NPCName;
        textBoxColor.a = 1;
        dialogueImage.color = textBoxColor;
        dialogueText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position,
        npc.transform.position) <= rangeOfAwareness)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogueText.text = string.Empty;
                StartDialogue();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (dialogueText.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = lines[index];
                }
            }
            exclamation.SetActive(true);
        }
        else
        {
            exclamation.SetActive(false);
        }
    }

    void StartDialogue()
    {
        animator.SetBool("IsOpen", true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char line in lines[index].ToCharArray())
        {
            dialogueText.text += line;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            animator.SetBool("IsOpen", false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeOfAwareness);
    }
}
