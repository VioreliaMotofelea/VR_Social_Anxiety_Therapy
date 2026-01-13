using UnityEngine;

public class NPCGazeController : MonoBehaviour
{
    [Header("References")]
    public Transform target;   // Main Camera
    public Transform head;
    public Transform body;

    public NPCVisualFeedback visualFeedback;

    [Header("Anxiety")]
    [Range(1, 7)]
    public int anxietyLevel = 1;

    public bool gazeActive = false;

    private float headRotationSpeed = 0f;
    private float bodyRotationSpeed = 0f;

    void Update()
    {
        if (!gazeActive || target == null) return;

        UpdateBehaviorFromAnxiety();

        Vector3 lookDirection = target.position - head.position;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude < 0.01f) return;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        // Head rotation (always primary)
        if (headRotationSpeed > 0f)
        {
            head.rotation = Quaternion.Slerp(
                head.rotation,
                lookRotation,
                Time.deltaTime * headRotationSpeed
            );
        }

        // Body rotation (only at higher anxiety)
        if (body != null && bodyRotationSpeed > 0f)
        {
            body.rotation = Quaternion.Slerp(
                body.rotation,
                lookRotation,
                Time.deltaTime * bodyRotationSpeed
            );
        }

        Debug.DrawRay(
            head.position,
            lookDirection.normalized * 2f,
            Color.red
        );
    }

    void UpdateBehaviorFromAnxiety()
    {
        switch (anxietyLevel)
        {
            case 1:
                headRotationSpeed = 0f;
                bodyRotationSpeed = 0f;
                break;

            case 2:
                headRotationSpeed = 0.8f;
                bodyRotationSpeed = 0f;
                break;

            case 3:
                headRotationSpeed = 1.5f;
                bodyRotationSpeed = 0f;
                break;

            case 4:
                headRotationSpeed = 2.5f;
                bodyRotationSpeed = 0f;
                break;

            case 5:
                headRotationSpeed = 4f;
                bodyRotationSpeed = 0f;
                break;

            case 6:
                headRotationSpeed = 5f;
                bodyRotationSpeed = 1.5f;
                break;

            case 7:
                headRotationSpeed = 6f;
                bodyRotationSpeed = 3f;
                break;
        }
    }

    public void SetAnxietyLevel(int level)
    {
        anxietyLevel = level;
        UpdateBehaviorFromAnxiety();
        
        if (visualFeedback != null)
        {
            visualFeedback.UpdateVisual(level);
        }

        Debug.Log("NPC Anxiety Level set to: " + level);
    }

    public void StartGaze()
    {
        gazeActive = true;
        Debug.Log("NPC gaze started");
    }

    public void StopGaze()
    {
        gazeActive = false;
        Debug.Log("NPC gaze stopped");
    }
}
