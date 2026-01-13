using UnityEngine;

public class TherapistRobotController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        if (animator != null)
        {
            animator.Play("Robot_Wave");
        }

        Debug.Log("Hello! I will guide you through the exposure therapy.");
    }
}
