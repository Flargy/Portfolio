using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeExamples : MonoBehaviour
{
    private string playerInput;
    private string answer;
    private bool red;
    private bool green;
    private bool blue;
    void Start()
    {
        
    }

    private void ForQuestions1()
    {
        int firstNumber = 5;
        int secondNumber = 2;
        int thirdNumber = 8;
        for (int i = 2; i > 0; i--)
        {
            firstNumber = secondNumber;
            secondNumber++;
            thirdNumber--;
        }
        
    }

    private void ForQuestions2()
    {
        int firstNumber = 6;
        int secondNumber = 1;
        int thirdNumber = 8;
        for (int i = 0; i < 3; i++)
        {
            firstNumber--;
            secondNumber++;
            thirdNumber = thirdNumber / 2;
        }
    }

    private void SwitchQuestions1()
    {
        switch (playerInput)
        {
            case "1":
                answer += "Open";
                break;
            case "2":
                answer += "Unlock";
                break;
            case "3":
                answer += "this";
                break;
            case "4":
                answer += "door";
                break;
}
        
    }

    private void SwitchQuestions2()
    {
        switch (playerInput)
        {
            case "1":
                answer += "Blue";
                break;
            case "2":
                answer += "Orange";
                break;
            case "3":
                answer += "Red";
                break;
            case "4":
                answer += "Green";
                break;
        }
    }

    private void IfQuestions1()
    {
        if (red == true && green == true && blue == false)
        {
            OpenDoor();
        }
        else
        {
            LockDoor();
        }
        
    }

    private void IfQuestions2()
    {
        if (red == blue && blue != green)
        {
            OpenDoor();
        }
        else if (green == true)
        {
            LockDoor();
        }
    }


    private void BossQuestion()
    {
        int firstNumber = 10;
        int secondNumber = 6;
        int thirdNumber;

        switch (secondNumber)
        {
            case 4:
                thirdNumber = 3;
                break;
            case 8:
                thirdNumber = 9;
                break;
            case 3:
                thirdNumber = 7;
                break;
            default:
                thirdNumber = 2;
                break;
        }

        if (thirdNumber > secondNumber)
        {
            firstNumber = 1;
        }
        else
        {
            firstNumber = 7;
            secondNumber = secondNumber + 3;
        }

        for (int i = 5; i > 1; i--)
        {
            thirdNumber++;
            firstNumber--;
        }

    }


    private void LockDoor()
    {

    }

    private void OpenDoor()
    {

    }

}
