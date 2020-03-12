using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookInterface : MonoBehaviour
{
    [SerializeField] private GameObject questLog;
    [SerializeField] private GameObject bookPanel;
    [SerializeField] private Text leftPage;
    [SerializeField] private Text rightPage;

    [SerializeField] private Image bookIcon;
    [SerializeField] private Text pages;

    Dictionary<int, BookChapter> chapters = new Dictionary<int, BookChapter>();

    private int bookIndex = 1;
    private int unlockedIndex = 6;
    private bool xboxInputUpNotOnCooldown = true;
    private bool xboxInputLeftNotOnCooldown = true;
    private bool xboxInputRightNotOnCooldown = true;
    // Start is called before the first frame update
    void Start()
    {
        EventHandeler.Current.RegisterListener(EventHandeler.EVENT_TYPE.QuestReward, BookReward);
        chapters.Add(1, new BookChapter("A Good War - Chapter 1", "His son lay still. He had died weeks ago, but only now was he at rest." + "\n" + " I am afraid for him. " + "\n" + "Do not be, Saurfang had said, so long ago. He knelt on the cold, unyielding stone floor of Icecrown Citadel and gathered his boy in his arms."));
        chapters.Add(2, new BookChapter("A Good War - Chapter 2", "They are changing our children. They have changed you. The warlocks gave me a gift. I was once powerful. Now, I am the whirlwind, he had said. I am war itself. I shall bring glory to my people until my dying day. " + "\n" + "How strange those words seemed now.How tainted."));
        chapters.Add(3, new BookChapter("A Good War - Chapter 3", "He lifted his son’s body and carried him from the citadel. The eyes of dozens of champions lay upon him. Both Horde and Alliance soldiers stood aside. Some offered silent salutes, honoring him in his grief. Our son must not follow your path. Keep him on our world, my love. "));
        chapters.Add(4, new BookChapter("A Good War - Chapter 4", "He will be safe. Untouched. Icecrown Citadel vanished. The dry chill of Northrend was replaced with the warm sun and humid air of Nagrand. He laid his son upon an unlit pyre near the final resting places of his family. His son was now dressed in simple garments from Garadar, the place he had known as a boy."));
        chapters.Add(5, new BookChapter("A Good War - Chapter 5", "Before you go, what will you name him? He is my heart. He is the heart of my whole world, he had said. He touched a burning torch to the pyre. Orange flames began to spread, first in the kindling, then in the chopped wooden logs. Shimmers of blue and white danced among the flames as the fire grew hotter."));
        chapters.Add(6, new BookChapter("A Good War - Chapter 6", "He made himself watch the flames consume his son. It was his boy’s final honor. " + "\n" +"He would not turn away. He watched skin give way to muscle, to bone, and finally, to ash. I will name him Dranosh. “Heart of Draenor.”"));
        pages.text = "PG: " + unlockedIndex.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Journal key") || (Input.GetAxisRaw("Journal axis") > 0 && xboxInputUpNotOnCooldown == true))
        {
            xboxInputUpNotOnCooldown = false;
            StartCoroutine(XboxCooldown());
            questLog.SetActive(false);
            if (bookPanel.active) { 
                bookPanel.SetActive(false);
            }
            else { 
                bookPanel.SetActive(true);
            }

            if (unlockedIndex == 0)
            {
                bookIndex = 0;
                leftPage.text = "You have nothing stored in the book yet";
                rightPage.text = "";
                return;
            }
            leftPage.text = chapters[1].Title + "\n" + "\n" + chapters[1].Story;
            rightPage.text = chapters[2].Title + "\n" + "\n" + chapters[2].Story;
            bookIndex = 1;
        }
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetAxisRaw("Flip pages axis") > 0) && xboxInputRightNotOnCooldown == true))
        {
            xboxInputRightNotOnCooldown = false;
            StartCoroutine(XboxCooldown());
            if (unlockedIndex == 0)
                return;

            CheckHigherIndex();
            leftPage.text = chapters[bookIndex].Title + "\n" + "\n" + chapters[bookIndex].Story;
            rightPage.text = chapters[bookIndex + 1].Title + "\n" + "\n" + chapters[bookIndex + 1].Story;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetAxisRaw("Flip pages axis") < 0 && xboxInputLeftNotOnCooldown == true))
        {
            xboxInputLeftNotOnCooldown = false;
            StartCoroutine(XboxCooldown());
            if (unlockedIndex == 0)
                return;

            CheckLowerIndex();
            rightPage.text = chapters[bookIndex + 1].Title + "\n" + "\n" + chapters[bookIndex + 1].Story;
            leftPage.text = chapters[bookIndex].Title + "\n" + "\n" + chapters[bookIndex].Story;
        }
        
    }

    private void BookReward(EventInfo eventInfo)
    {
        RewardQuestInfo rei = (RewardQuestInfo)eventInfo;
        if(rei.rewardNumber == 4)
        {
            unlockedIndex += 2;
            pages.text = "PG: " + unlockedIndex.ToString();
        }
    }

    private void CheckHigherIndex()
    {
        bookIndex += 2;
        if (bookIndex > unlockedIndex)
            bookIndex = unlockedIndex - 1;
        
    }

    private void CheckLowerIndex()
    {
        bookIndex -= 2;
        if (bookIndex <= 0)
            bookIndex = 1;
    }

    private IEnumerator XboxCooldown()
    {
        while (Input.GetAxisRaw("Journal axis") != 0 || Input.GetAxisRaw("Flip pages axis") != 0)
        {
            yield return new WaitForSeconds(Time.deltaTime);

        }
        xboxInputLeftNotOnCooldown = true;
        xboxInputUpNotOnCooldown = true;
        xboxInputRightNotOnCooldown = true;
    }
}

public class BookChapter
{
    private string title;
    public string Title { get { return title; } }
    private string story;
    public string Story { get { return story; } }

    public BookChapter(string t, string s)
    {
        title = t;
        story = s;
    }

}
