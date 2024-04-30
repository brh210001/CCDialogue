using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator;

    [SerializeField][Tooltip("Radius size where NPC will see player")]
    private float _rangeOfAwareness;

    private Queue<string> sentances;

    // Start is called before the first frame update
    void Start()
    {
        sentances = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentances.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentances.Enqueue(sentence);
        }

        DisplayNextSentance();
    }

    public void DisplayNextSentance()
    {
        if (sentances.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentance = sentances.Dequeue();
        dialogueText.text = sentance;
        StopAllCoroutines();
        StartCoroutine(TypeSentance(sentance));
    }

    IEnumerator TypeSentance (string sentance)
    {
        dialogueText.text = "";
        foreach (char letter in sentance.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        Debug.Log("End of conversation");
    }

}
