using UnityEngine;

public class NPCDistanceController : MonoBehaviour
{
    public Transform target;          // Main Camera
    public bool distanceActive = false;

    [Range(1, 7)]
    public int anxietyLevel = 1;

    public float moveSpeed = 1.2f;
    public float minDistance = 0.8f;  // cât de aproape maxim

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (!distanceActive || target == null) return;

        float desiredDistance = GetDistanceFromAnxiety();
        float currentDistance = Vector3.Distance(transform.position, target.position);

        if (currentDistance > desiredDistance)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            direction.y = 0;

            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    float GetDistanceFromAnxiety()
    {
        switch (anxietyLevel)
        {
            case 1:
            case 2:
                return Vector3.Distance(startPosition, target.position);

            case 3:
            case 4:
                return 2.5f;

            case 5:
            case 6:
                return 1.5f;

            case 7:
                return minDistance;

            default:
                return 3f;
        }
    }

    public void SetAnxietyLevel(int level)
    {
        anxietyLevel = level;
    }

    public void StartDistance()
    {
        distanceActive = true;
        Debug.Log("NPC distance behavior started");
    }

    public void StopDistance()
    {
        distanceActive = false;
        transform.position = startPosition;
        Debug.Log("NPC distance behavior stopped");
    }
}
