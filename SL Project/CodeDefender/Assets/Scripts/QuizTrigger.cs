using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTrigger : MonoBehaviour
{
    [SerializeField] private GameObject quizHandeler;
    [SerializeField] private string answer1;
    [SerializeField] private string answer2;
    [SerializeField] private string answer3;
    [SerializeField] private Sprite picture;
    [SerializeField] private GameObject activationObject1;
    [SerializeField] private GameObject activationObject2;
    [SerializeField] private bool openDoor;
    [SerializeField] private bool isIfQuestion = false;
    [SerializeField] private GameObject teleporter = null;
    [SerializeField] private bool boolAnswer1;
    [SerializeField] private bool boolAnswer2;
    [SerializeField] private bool boolAnswer3;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isIfQuestion == false)
        {
            quizHandeler.GetComponent<Quizes>().ActivateQuiz(answer1, answer2, answer3, picture, activationObject1, activationObject2, openDoor, teleporter, gameObject);
        }
        else if(other.CompareTag("Player") && isIfQuestion == true)
        {
            quizHandeler.GetComponent<Quizes>().ActivateQuiz(boolAnswer1, boolAnswer2, boolAnswer3, picture, activationObject1, activationObject2, openDoor, teleporter, answer1, answer2, answer3, gameObject);
        }
    }

}
