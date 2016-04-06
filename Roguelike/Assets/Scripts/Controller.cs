using UnityEngine;
using System.Collections;

public static class Controller{

    public static bool acceleratorMove = true;
    public static bool swipeShoot = true;
    public static int highScore = 0;
    public static float volume = .5f;
    public static int difficulty = 1;
    public static int highestLevel = 0;
    
    public static void LoadData()
    {
        volume = PlayerPrefs.GetFloat("Volume");
        acceleratorMove = PlayerPrefs.GetInt("Accelerator Move") > 0;
        swipeShoot = PlayerPrefs.GetInt("Swipe Shoot") > 0;
        highScore = PlayerPrefs.GetInt("High Score");
        difficulty = PlayerPrefs.GetInt("Difficulty");
        highestLevel = PlayerPrefs.GetInt("Highest Level");
    }
	
    public static void StoreData()
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Accelerator Move", acceleratorMove ? 1 : 0);
        PlayerPrefs.SetInt("Swipe Shoot", swipeShoot ? 1 : 0);
        PlayerPrefs.SetInt("High Score", highScore);
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("Highest Level", highestLevel);
    }

}
