using UnityEngine;

public class LabDoorQuiz : MonoBehaviour
{
    public QuizManager quizManager;
    public string labName = "EVS Lab";

    [TextArea]
    public string question =
        "Which action helps reduce environmental pollution?";

    public string[] answers =
    {
        "Burning plastic",
        "Recycling waste",
        "Dumping waste in rivers",
        "Leaving lights on"
    };

    public int correctAnswer = 1;

    [TextArea]
    public string explanation =
        "Recycling helps reduce waste and conserve natural resources.";

    private bool playerNearby;
    private bool quizStarted;

    void Update()
{
    if (playerNearby && Input.GetKeyDown(KeyCode.E))
    {
        Debug.Log("E key detected near door!");

        if (!quizStarted)
        {
            quizStarted = true;
            Debug.Log("Opening quiz now!");

            if (quizManager != null)
            {
                quizManager.ShowQuestion(
                    question,
                    answers,
                    correctAnswer,
                    explanation,
                    gameObject
                );
            }
            else
            {
                Debug.LogError("QuizManager is not assigned!");
            }
        }
    }
}

   void OnTriggerEnter(Collider other)
{
    Debug.Log("Something entered the door trigger: " + other.name);

    if (other.CompareTag("Player"))
    {
        playerNearby = true;
        Debug.Log("Player detected near door!");
    }
}

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}