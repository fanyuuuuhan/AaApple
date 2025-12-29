using UnityEngine;

public class NPCScript : MonoBehaviour
{
    [TextArea]
    public string[] dialogueLines;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.HideDialogue();
            triggered = false;
        }
    }
}

