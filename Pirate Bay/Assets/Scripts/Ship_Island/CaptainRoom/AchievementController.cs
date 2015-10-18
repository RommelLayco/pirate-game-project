using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AchievementController : MonoBehaviour {
    private GameManager manager;
    private List<Achievement> completedAchievements;
    public Text achievementTitle;
    public Text achievementDescription;
	// Use this for initialization
	void Awake () {
        manager = GameManager.getInstance();
        setAchievementInformation();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void onLeftClick()
    {
        //scrolls to the achievement to the left (or the end if at the start of the list)
        manager.achievementIndex--;
        if (manager.achievementIndex < 0)
        {
            manager.achievementIndex = manager.achievements.ToArray().Length- 1;
        }
        setAchievementInformation();
    }

    public void onRightClick()
    {
        //scrolls to the achievement to the right (or the first if at the end of the list)
        manager.achievementIndex++;
        if (manager.achievementIndex >= manager.achievements.ToArray().Length)
        {
            manager.achievementIndex = 0;
        }
        setAchievementInformation();
    }
    private void setAchievementInformation()
    {
        //Displaying the crew members name and stats
        Achievement a = manager.achievements[manager.achievementIndex];
        achievementTitle.text = a.getName();
        achievementDescription.text = a.getDescription();
        if (a.getCompleted())
        {
            achievementDescription.color = new Color(184,168,22);
            achievementTitle.color = new Color(184, 168, 22);
        }
        else
        {
            achievementDescription.color = new Color(50, 50, 50);
            achievementTitle.color = new Color(50, 50, 50);
        }
    }
}
