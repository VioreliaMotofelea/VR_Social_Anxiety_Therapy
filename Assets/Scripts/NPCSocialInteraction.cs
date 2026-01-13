using UnityEngine;
using TMPro;

public class NPCSocialInteraction : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public bool interactionActive = false;

    [Range(1, 7)]
    public int anxietyLevel = 1;

    void Start()
    {
        if (dialogueText != null)
            dialogueText.gameObject.SetActive(false);
    }

    public void SetAnxietyLevel(int level)
    {
        anxietyLevel = level;
    }

    public void StartInteraction()
    {
        interactionActive = true;
        UpdateDialogue();
    }

    public void StopInteraction()
    {
        interactionActive = false;

        if (dialogueText != null)
            dialogueText.gameObject.SetActive(false);
    }

    void UpdateDialogue()
    {
        if (!interactionActive || dialogueText == null) return;

        dialogueText.gameObject.SetActive(true);

        if (anxietyLevel <= 2)
            dialogueText.text = "";
        else if (anxietyLevel <= 4)
            dialogueText.text = "Hello.";
        else if (anxietyLevel <= 6)
            dialogueText.text = "Are you okay?";
        else
            dialogueText.text = "Why are you so quiet?";
    }
}
