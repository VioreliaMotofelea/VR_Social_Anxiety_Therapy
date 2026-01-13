using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnxietyUIController : MonoBehaviour
{
    public Slider anxietySlider;
    public TextMeshProUGUI valueText;
    public NPCGazeController npcGaze;
    public NPCDistanceController npcDistance;
    public NPCSocialInteraction npcSocial;

    private float anxietyStart;
    private float anxietyMax;
    private float anxietyEnd;
    private bool exposureActive = false;

    void Start()
    {
        anxietySlider.onValueChanged.AddListener(OnAnxietyChanged);
        UpdateValue(anxietySlider.value);
    }

    public void OnAnxietyChanged(float value)
    {
        int level = Mathf.RoundToInt(value);
        UpdateValue(level);

        if (npcGaze != null)
        {
            npcGaze.SetAnxietyLevel(level);
            npcDistance.SetAnxietyLevel(level);
            npcSocial.SetAnxietyLevel(level);
        }

        if (exposureActive && value > anxietyMax)
        {
            anxietyMax = value;
        }
    }

    void UpdateValue(float value)
    {
        valueText.text = value.ToString();
    }

    public void StartExposure()
    {
        anxietyStart = anxietySlider.value;
        anxietyMax = anxietyStart;
        exposureActive = true;

        anxietySlider.interactable = true;

        Debug.Log("Exposure START | Anxiety: " + anxietyStart);

        if (npcGaze != null)
        {
            npcGaze.StartGaze();
            npcDistance.StartDistance();
            npcSocial.StartInteraction();
        }
    }

    public void RepeatLevel()
    {
        Debug.Log("Repeat Level");
        StartExposure();
    }

    public void NextLevel()
    {
        if (!exposureActive)
        {
            Debug.Log("Exposure not started yet!");
            return;
        }

        anxietyEnd = anxietySlider.value;
        exposureActive = false;
        anxietySlider.interactable = true;

        Debug.Log(
            $"Exposure END | START:{anxietyStart} MAX:{anxietyMax} END:{anxietyEnd}"
        );

        if (npcGaze != null)
        {
            npcGaze.StopGaze();
            npcDistance.StopDistance();
            npcSocial.StopInteraction();
        }
    }
}
