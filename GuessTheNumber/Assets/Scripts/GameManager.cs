using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject menuView;
    public GameObject gameplayView;
    public GameObject AIGameView;
    public GameObject MenuAi;
    public GameObject inputFiled;
    public GameObject aiButtons;
    public Button playButton;
    public Button playAiButton;
    public Button lessButton;
    public Button correctButton;
    public Button moreButton;
    public TextMeshProUGUI responseAiText;
    public TextMeshProUGUI responsAIMenuText;
    public TextMeshProUGUI responseText;
    public TMP_InputField inputAiText;
    public TMP_InputField inputText;
    private int randomNumber;
    private int guesses;
    private int aiGuess;
    private int max;
    private int min;
    public int minNumber;
    public int maxNumber;


    // Start is called before the first frame update
    void Start()
    {
        Button playBtn = playButton.GetComponent<Button>();
        Button playAiBtn = playAiButton.GetComponent<Button>();
        Button lessBtn = lessButton.GetComponent<Button>();
        Button correctBtn = correctButton.GetComponent<Button>();
        Button moreBtn = moreButton.GetComponent<Button>();

        playBtn.onClick.AddListener(StartGame);
        playAiBtn.onClick.AddListener(PrepareAiGame);
        lessBtn.onClick.AddListener(Less);
        correctBtn.onClick.AddListener(Correct);
        moreBtn.onClick.AddListener(More);

        inputAiText.onEndEdit.AddListener(StartAiGame);
        inputText.onEndEdit.AddListener(GetInput);

    }

    private void StartGame()
    {
        menuView.SetActive(false);
        gameplayView.SetActive(true);
        inputFiled.SetActive(true);
        AIGameView.SetActive(false);
        responseText.text = "Guess the number form " + minNumber + " - " + maxNumber;
        randomNumber = Random.Range(minNumber, maxNumber);
        guesses = 0;
    }

    private void PrepareAiGame()
    {
        max = 100;
        min = 1;
        menuView.SetActive(false);
        MenuAi.SetActive(true);
        AIGameView.SetActive(false);
        gameplayView.SetActive(false);
        responsAIMenuText.text = "Choose number 1-100";
        inputAiText.text = "";

    }

    public void GetInput(string input)
    {
        Debug.Log("You Entered " + input);
        if (int.TryParse(input, out int number))
        {
            guesses++;
            if (randomNumber == number)
            {
                responseText.text = "Congratulation, this is correct number. It took you " + guesses + " guesses";
                inputFiled.SetActive(false);
                menuView.SetActive(true);
            }
            else if (randomNumber > number)
            {
                responseText.text = "My number is higher";
            }
            else if (randomNumber < number)
            {
                responseText.text = "My number is lower";
            }

            inputText.text = "";

        }

    }

    private void Less()
    {
        max = aiGuess;
        AiController();
    }

    private void More()
    {
        min = aiGuess + 2;
        AiController();
    }

    private void Correct()
    {
        responseAiText.text = "your number is " + aiGuess;
        inputFiled.SetActive(false);
        menuView.SetActive(true);
        aiButtons.SetActive(false);
    }

    public void StartAiGame(string input)
    {
        if (int.TryParse(input, out int number))
        {
            if (number <= 100 && number >= 1)
            {
                MenuAi.SetActive(false);
                AIGameView.SetActive(true);
                aiButtons.SetActive(true);
                AiController();
            }
        }

    }


    public void AiController()
    {
        aiGuess = (int)Mathf.Ceil((max + min - 1) / 2);
        responseAiText.text = "is your number " + aiGuess;
    }

}
