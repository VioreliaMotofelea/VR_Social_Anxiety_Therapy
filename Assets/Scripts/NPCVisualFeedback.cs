using UnityEngine;

public class NPCVisualFeedback : MonoBehaviour
{
    public Renderer npcRenderer;

    public Color calmColor = Color.gray;
    public Color neutralColor = new Color(0.6f, 0.6f, 0.6f);
    public Color uneasyColor = Color.yellow;
    public Color anxiousColor = new Color(1f, 0.5f, 0f);
    public Color extremeColor = Color.red;

    public void UpdateVisual(int anxietyLevel)
    {
        if (npcRenderer == null) return;

        Color targetColor = calmColor;

        if (anxietyLevel <= 2)
            targetColor = calmColor;
        else if (anxietyLevel <= 4)
            targetColor = neutralColor;
        else if (anxietyLevel == 5)
            targetColor = uneasyColor;
        else if (anxietyLevel == 6)
            targetColor = anxiousColor;
        else
            targetColor = extremeColor;

        npcRenderer.material.color = targetColor;
    }
}
