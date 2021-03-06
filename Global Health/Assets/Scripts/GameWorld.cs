﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWorld : MonoBehaviour
{
    public double imu;
    public int population;
    public int deaths;
    public int building;
    public City[] cities;
    public List<GameObject> totalBuildings = new List<GameObject>();
    [SerializeField]
    private AudioClip music;

    public static GameWorld Instance { get; private set; }
    private void Start()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        UpdatePopulation();
        UpdateDeathrate();
        UpdateBuilding();
        UpdateGameUI();
        GetBuildings();

        AudioManager.Instance.PlayMusicWithFade(music);
    }

    private void OnLevelWasLoaded(int levelNumber)
    {
        switch(levelNumber)
        {
            //Main Menu
            case 0:
            DestroyImmediate(gameObject);
            break;

            default:
            break;
        }
    }

    public void substractIMU(double units)
    {
        imu -= units;
        playerMoney.text = "IMU: " + imu.ToString();
    }

    public void increaseIMU(double units)
    {
        imu += units;
        playerMoney.text = "IMU: " + imu.ToString();
    }

    public void UpdatePopulation()
    {
        StartCoroutine(SendPopulationRequest());
    }

    public void NewPopulation(int newpopulation)
    {
        population += newpopulation;
    }

    public float delayValue;
    //Sending message to all cities to send their current population value for total population calculation
    public IEnumerator SendPopulationRequest()
    {
        population = 0;

        for (int i = 0; i < cities.Length; i++)
        {
            cities[i].SendPopulation();
        }
        yield return new WaitForSeconds(delayValue);
        worldPopulation.text = "World population: " + population.ToString();
    }

    public void UpdateDeathrate()
    {
        StartCoroutine(SendDeathrateRequest());
    }

    public void NewDeathrate(int newdeathrate)
    {
        deaths += newdeathrate;
    }

    public float delayValueDeathrate;
    public IEnumerator SendDeathrateRequest()
    {
        deaths = 0;

        for (int i = 0; i < cities.Length; i++)
        {
            cities[i].SendDeathrate();
        }
        yield return new WaitForSeconds(delayValueDeathrate);
    }

    public void GetBuildings()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            foreach (Transform building in cities[i].buildings)
            {
                totalBuildings.Add(building.gameObject);
            }
        }
    }

    public void UpdateBuilding()
    {
        StartCoroutine(SendBuildingRequest());
    }

    public void NewBuilding(int newbuilding)
    {
        // Add the new count of buildings to the old count og buildings 
        building += newbuilding;
    }

    public float delayValueBuilding;
    public IEnumerator SendBuildingRequest()
    {
        building = 0;

        for (int i = 0; i < cities.Length; i++)
        {
            // Access every city to send their count of buildings 
            cities[i].SendBuilding();
        }
        yield return new WaitForSeconds(delayValueBuilding);
    }

    public void UpdateCities()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            cities[i].GetNewData();
        }
    }

    public Text worldPopulation;
    public Text playerMoney;
    public void UpdateGameUI()
    {
        playerMoney.text = "IMU: " + imu.ToString();
        worldPopulation.text = "World population: " + population.ToString();
        Debug.Log("pop: " + population);
    }
}
