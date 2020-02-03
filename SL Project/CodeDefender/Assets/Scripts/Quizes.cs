using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quizes : MonoBehaviour
{

    [SerializeField] private GameObject textField1;
    [SerializeField] private GameObject textField2;
    [SerializeField] private GameObject textField3;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject questionImage;
    [SerializeField] private GameObject answerButton;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject boolTextField1;
    [SerializeField] private GameObject boolTextField2;
    [SerializeField] private GameObject boolTextField3;
    [SerializeField] private GameObject toggleExplanation;
    [SerializeField] private GameObject variableExplanation;
    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;
    private string answer1;
    private string answer2;
    private string answer3;
    private string playerAnswer1;
    private string playerAnswer2;
    private string playerAnswer3;
    private bool boolAnswer1;
    private bool boolAnswer2;
    private bool boolAnswer3;
    private bool playerBoolAnswer1;
    private bool playerBoolAnswer2;
    private bool playerBoolAnswer3;

    private Text text1;
    private Text text2;
    private Text text3;
    private Text boolText1;
    private Text boolText2;
    private Text boolText3;
    private InputField field1;
    private InputField field2;
    private InputField field3;
    private GameObject door;
    private GameObject secondActiveObject;
    private bool openDoor;
    private GameObject teleporter;
    private bool isIfQuestion = false;
    private GameObject trigger;
    private bool questionAnsweredCorrectly = false;
    private AudioSource source;



    private void Start()
    {
        source = GetComponent<AudioSource>();
        text1 = textField1.GetComponent<Text>();
        text2 = textField2.GetComponent<Text>();
        text3 = textField3.GetComponent<Text>();
        boolText1 = boolTextField1.GetComponent<Text>();
        boolText2 = boolTextField2.GetComponent<Text>();
        boolText3 = boolTextField3.GetComponent<Text>();
    }


    public void ActivateQuiz(string correctAnswer1, string correctAnswer2, string correctAnswer3, Sprite questionImg, GameObject door, GameObject activationObject2, bool open, GameObject teleport, GameObject triggerZone)
    {
        isIfQuestion = false;
        player.GetComponent<PlayerController>().FreezePlayer();
        answer1 = correctAnswer1;
        answer2 = correctAnswer2;
        answer3 = correctAnswer3;
        questionImage.GetComponent<Image>().sprite = questionImg;
        this.door = door;
        openDoor = open;
        trigger = triggerZone;

        textField1.transform.parent.gameObject.SetActive(true);
        textField2.transform.parent.gameObject.SetActive(true);
        textField3.transform.parent.gameObject.SetActive(true);
        questionImage.SetActive(true);
        answerButton.SetActive(true);
        exitButton.SetActive(true);
        variableExplanation.SetActive(true);

        if (activationObject2 != null)
        {
            secondActiveObject = activationObject2;
        }
        

        if (teleport != null)
        {
            teleporter = teleport;
        }

    }

    public void ActivateQuiz(bool firstAnswer, bool secondAnswer, bool thirdAnswer, Sprite questionImg, GameObject door, GameObject activationObject2, bool open, GameObject teleport, string answerValue1, string answerValue2, string answerValue3, GameObject triggerZone)
    {
        isIfQuestion = true;
        player.GetComponent<PlayerController>().FreezePlayer();
        questionImage.GetComponent<Image>().sprite = questionImg;
        this.door = door;
        openDoor = open;
        boolAnswer1 = firstAnswer;
        boolAnswer2 = secondAnswer;
        boolAnswer3 = thirdAnswer;
        boolText1.text = answerValue1;
        boolText2.text = answerValue2;
        boolText3.text = answerValue3;
        trigger = triggerZone;

        boolText1.transform.parent.gameObject.SetActive(true);
        boolText2.transform.parent.gameObject.SetActive(true);
        boolText3.transform.parent.gameObject.SetActive(true);
        questionImage.SetActive(true);
        answerButton.SetActive(true);
        exitButton.SetActive(true);
        toggleExplanation.SetActive(true);

        if(activationObject2 != null)
        {
            secondActiveObject = activationObject2;
        }
        

        if (teleport != null)
        {
            teleporter = teleport;
        }
    }

    public void CheckAnswer()
    {
        if(questionAnsweredCorrectly == false) {
            if (isIfQuestion == false)
            {
                playerAnswer1 = text1.text;
                playerAnswer2 = text2.text;
                playerAnswer3 = text3.text;

                if (playerAnswer1 == answer1 && playerAnswer2 == answer2 && playerAnswer3 == answer3)
                {
                    source.PlayOneShot(success, 0.1f);
                    questionAnsweredCorrectly = true;
                    trigger.SetActive(false);
                    if (openDoor == true)
                    {
                        door.SetActive(false);


                    }
                    else if (openDoor == false && door != null)
                    {
                        door.SetActive(true);

                    }
                    if (secondActiveObject != null)
                    {
                        secondActiveObject.SetActive(!secondActiveObject.activeSelf);
                    }
                    if (teleporter != null)
                    {
                        teleporter.SetActive(true);
                    }

                }
                else
                {
                    source.PlayOneShot(failure, 0.1f);
                }
            }
            else if (isIfQuestion == true)
            {
                playerBoolAnswer1 = boolTextField1.transform.parent.gameObject.GetComponent<Toggle>().isOn;
                playerBoolAnswer2 = boolTextField2.transform.parent.gameObject.GetComponent<Toggle>().isOn;
                playerBoolAnswer3 = boolTextField3.transform.parent.gameObject.GetComponent<Toggle>().isOn;

                if (playerBoolAnswer1 == boolAnswer1 && playerBoolAnswer2 == boolAnswer2 && playerBoolAnswer3 == boolAnswer3)
                {
                    source.PlayOneShot(success, 0.1f);
                    trigger.SetActive(false);
                    questionAnsweredCorrectly = true;

                    if (openDoor == true)
                    {
                        door.SetActive(false);


                    }
                    if (secondActiveObject != null)
                    {
                        secondActiveObject.SetActive(!secondActiveObject.activeSelf);
                    }
                    else if (openDoor == false && door != null)
                    {
                        door.SetActive(true);

                    }
                    if (teleporter != null)
                    {
                        teleporter.SetActive(true);
                    }

                }
                else
                {
                    source.PlayOneShot(failure, 0.1f);
                }

            }
        
        }
    }

    public void ExitQuiz()
    {
        player.GetComponent<PlayerController>().freezePlayer = false;
        answer1 = null;
        answer2 = null;
        answer3 = null;
        playerAnswer1 = null;
        playerAnswer2 = null;
        playerAnswer3 = null;
        teleporter = null;
        door = null;
        questionImage.GetComponent<Image>().sprite = null;
        secondActiveObject = null;

        if (isIfQuestion == false)
        {
            textField1.GetComponentInParent<InputField>().text = "";
            textField2.GetComponentInParent<InputField>().text = "";
            textField3.GetComponentInParent<InputField>().text = "";
            textField1.transform.parent.gameObject.SetActive(false);
            textField2.transform.parent.gameObject.SetActive(false);
            textField3.transform.parent.gameObject.SetActive(false);
        }

        else
        {
            boolTextField1.transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
            boolTextField2.transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
            boolTextField3.transform.parent.gameObject.GetComponent<Toggle>().isOn = false;
            boolTextField1.transform.parent.gameObject.SetActive(false);
            boolTextField2.transform.parent.gameObject.SetActive(false);
            boolTextField3.transform.parent.gameObject.SetActive(false);
        }
        toggleExplanation.SetActive(false);
        variableExplanation.SetActive(false);
        questionImage.SetActive(false);
        answerButton.SetActive(false);
        exitButton.SetActive(false);
        questionAnsweredCorrectly = false;

    }
}
