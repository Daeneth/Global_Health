﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndscreenScore : MonoBehaviour
{
    public Text totalScoreText;
    public Text survivorsText;
    public Text deathrateText;
    public Text buildingsText;

    int survivors;
    int deathrate;
    int buildings;

    public int multiplier = 100;

    int totalScore;

    public Text[] pop;
    public Text[] death;
    public Text[] building;

    [SerializeField]
    private AudioClip buttonClicksSFX;
    [SerializeField]
    private AudioClip music;

    // Start is called before the first frame update
    void Start()
    { 
        survivors = GameWorld.Instance.population;
        survivorsText.text = "Total Survivors: " + survivors.ToString();

        deathrate = GameWorld.Instance.deaths;
        deathrateText.text = "Total Deathrate: " + deathrate.ToString();

        buildings = GameWorld.Instance.building;
        buildingsText.text = "Total Buildings: " + buildings.ToString();

        totalScore = (survivors + buildings * multiplier) - deathrate;

        totalScoreText.text = "Total Score: " + totalScore.ToString();

        for (int i = 0; i < GameWorld.Instance.cities.Length; i++)
        {
            pop[i].text = "Survivors: " + GameWorld.Instance.cities[i].currentPopulation.ToString();
            death[i].text = "Deathrate: " + GameWorld.Instance.cities[i].deaths.ToString();
            building[i].text = "Buildings: " + GameWorld.Instance.cities[i].countBuilding.ToString();
        }

        //AudioManager.Instance.PlayMusicWithFade(music);

        Destroy(GameObject.Find("GameWorldCanvas"));
    }

    public void TakeScreenshot()
    {
        Debug.Log("Screenshot taken");
        AudioManager.Instance.PlaySFX(buttonClicksSFX, 1);
        ScreenCapture.CaptureScreenshot("Endscreen");
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to main menu...");
        SceneManager.LoadScene("MainMenu");
        AudioManager.Instance.PlaySFX(buttonClicksSFX, 1);
    }
}
