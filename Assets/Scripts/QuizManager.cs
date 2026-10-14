using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject infoPanel;
    public TMP_Text questionText;
    public Button[] options;
    public TMP_Text infoText;
    public Button continueButton;

    private int correctAnswer;
    private GameObject currentDoor;
    private bool answerCorrect;

    void Start()
    {
        quizPanel.SetActive(false);
        infoPanel.SetActive(false);
        Time.timeScale = 1f;

        continueButton.onClick.RemoveListener(CloseInfo);
        continueButton.onClick.AddListener(CloseInfo);
    }

    public void ShowQuestion(
        string question,
        string[] answers,
        int correctIndex,
        string info,
        GameObject door)
    {
        if (answers == null || answers.Length != options.Length ||
            correctIndex < 0 || correctIndex >= answers.Length)
        {
            Debug.LogError("Check the quiz answers and correct index.");
            return;
        }

        currentDoor = door;
        correctAnswer = correctIndex;
        answerCorrect = false;

        questionText.text = question;
        infoText.text = info;

        infoPanel.SetActive(false);
        quizPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        for (int i = 0; i < options.Length; i++)
        {
            int index = i;
            options[i].GetComponentInChildren<TMP_Text>().text = answers[i];
            options[i].onClick.RemoveAllListeners();
            options[i].onClick.AddListener(() => CheckAnswer(index));
            options[i].interactable = true;
        }
    }

    void CheckAnswer(int index)
    {
        if (index == correctAnswer)
        {
            answerCorrect = true;
            quizPanel.SetActive(false);
            infoPanel.SetActive(true);

            foreach (Button option in options)
                option.interactable = false;
        }
        else
        {
            Debug.Log("Wrong answer! Try again.");
        }
    }
    public void CloseInfo()
{
    Debug.Log("Continue button clicked!");

    if (!answerCorrect)
    {
        Debug.LogWarning("Answer has not been marked correct.");
        return;
    }

    if (currentDoor != null)
        currentDoor.SetActive(false);

    infoPanel.SetActive(false);
    quizPanel.SetActive(false);

    answerCorrect = false;
    currentDoor = null;

    Time.timeScale = 1f;
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;

    Debug.Log("Tour resumed successfully!");
}
}