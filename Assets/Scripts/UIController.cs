using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// TODO: Clean up all this mess and find the bug.

public class UIController : MonoBehaviour
{
    VisualElement root;

    VisualElement veLabelContainer;
    Label lbColorWord;

    VisualElement veButtonContainer;
    Button btGreen;
    Button btRed;
    Button btBlue;
    Button btYellow;

    enum ColorWordColor {
        WHITE,
        GREEN,
        RED,
        BLUE,
        YELLOW
    };

    enum ColorWordText
    {
        DOT,
        GREEN,
        RED,
        BLUE,
        YELLOW
    };

    ColorWordColor colorWordColor;
    ColorWordText colorWordText;

    int randomizedStorage = 0;
    bool running = false;
    int numIter = 0;

    int userInput = 0;
    bool correctlyAnswered = false;
    int userScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        colorWordColor = ColorWordColor.WHITE;
        colorWordText = ColorWordText.DOT;

        root = GetComponent<UIDocument>().rootVisualElement;

        veLabelContainer = root.Q<VisualElement>("VELabelContainer");
        lbColorWord = veLabelContainer.Q<Label>("LbColorWord");

        veButtonContainer = root.Q<VisualElement>("VEButtonContainer");
        btGreen = veButtonContainer.Q<Button>("BtGreen");
        btRed = veButtonContainer.Q<Button>("BtRed");
        btBlue = veButtonContainer.Q<Button>("BtBlue");
        btYellow = veButtonContainer.Q<Button>("BtYellow");

        running = true;

        btGreen.clicked += GreenClicked;
        btRed.clicked += RedClicked;
        btBlue.clicked += BlueClicked;
        btYellow.clicked += YellowClicked;

        StartCoroutine(ChangeColorAndTextRoutine());
    }

    IEnumerator ChangeColorAndTextRoutine()
    {
        while (running)
        {
            correctlyAnswered = false;

            ++numIter;

            if(numIter % 4 == 0)
            {
                randomizedStorage = Random.Range(1, 5);
                ChangeColor(randomizedStorage);
                ChangeText(randomizedStorage);
            } else
            {
                ChangeColor(Random.Range(1, 5));
                ChangeText(Random.Range(1, 5));
            }

            lbColorWord.text = colorWordText.ToString();

            switch (colorWordColor)
            {
                case 0:
                    lbColorWord.style.color = Color.white;
                    break;
                case (ColorWordColor)1:
                    lbColorWord.style.color = Color.green;
                    break;
                case (ColorWordColor)2:
                    lbColorWord.style.color = Color.red;
                    break;
                case (ColorWordColor)3:
                    lbColorWord.style.color = Color.blue;
                    break;
                case (ColorWordColor)4:
                    lbColorWord.style.color = Color.yellow;
                    break;
            }

            if (userInput == (int)colorWordColor && colorWordColor != 0)
            {
                correctlyAnswered = true;
            }

            yield return new WaitForSeconds(2.0f);

            lbColorWord.style.color = Color.white;
            lbColorWord.text = ".";

            if(correctlyAnswered)
            {
                ++userScore;
            }

            Debug.Log(userScore);

            yield return new WaitForSeconds(0.75f);

            if(numIter == 15)
            {
                running = false;
            }
        }
    }

    private void ChangeColor(int colorNr)
    {
        colorWordColor = (ColorWordColor)colorNr;
    }


    private void ChangeText(int textNr)
    {
        colorWordText = (ColorWordText)textNr;
    }

    private void GreenClicked()
    {
        userInput = 1;
    }

    private void RedClicked()
    {
        userInput = 2;
    }

    private void BlueClicked()
    {
        userInput = 3;
    }

    private void YellowClicked()
    {
        userInput = 4;
    }
}
