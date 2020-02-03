using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrestige : MonoBehaviour {

    public UnityEngine.UI.Text textField;
    public UnityEngine.UI.Image prestige;

    public Sprite rank1;
    public Sprite rank2;
    public Sprite rank3;
    public Sprite rank4;
    public Sprite rank5;
    public Sprite rank6;

    void Start () {
        int i = PlayerPrefs.GetInt("Rank", 1);
        switch (i)
        {
            case 1:
                textField.text = "Mercenary";
                prestige.sprite = rank1;
                break;
            case 2:
                textField.text = "Private";
                prestige.sprite = rank2;
                break;
            case 3:
                textField.text = "Lieutenant";
                prestige.sprite = rank3;
                break;
            case 4:
                textField.text = "Sergant";
                prestige.sprite = rank4;
                break;
            case 5:
                textField.text = "Major";
                prestige.sprite = rank5;
                break;
            case 6:
                textField.text = "General";
                prestige.sprite = rank6;
                break;
            default:
                textField.text = "Hacker";
                prestige.sprite = null;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resetRank()
    {
        textField.text = "Mercenary";
        prestige.sprite = rank1;
    }
}
