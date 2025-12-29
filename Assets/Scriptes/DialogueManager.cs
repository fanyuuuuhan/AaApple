using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialogueUI;   // 對話框 Panel
    public Text dialogueText;       // Legacy Text
    public float typeSpeed = 0.05f;

    private string[] lines;
    private int index;
    private PlayerScript playerMove;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        dialogueUI.SetActive(false);

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p) playerMove = p.GetComponent<PlayerScript>();
    }

    public void StartDialogue(string[] dialogueLines)
    {
        lines = dialogueLines;
        index = 0;

        dialogueUI.SetActive(true);
        if (playerMove) playerMove.canMove = false;

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";

        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // 打完一句停一下，自動下一句
        yield return new WaitForSeconds(0.4f);

        NextLine();
    }

    void NextLine()
    {
        index++;

        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);

        if (playerMove)
            playerMove.canMove = true;
    }

    // 舊版 ShowDialogue / HideDialogue 用不到了
    public void ShowDialogue(string s) { }
    public void HideDialogue() { }
}