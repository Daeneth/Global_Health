﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public CityUI cityui;
    public int currentPopulation;
    public int healthCare;
    public int economy;
    public int schooling;
    public int deathrate; 
    public int casesPrevented;
    public int cureSucces;
    public Transform buildings;
    public GameObject boxPrefab;

    private void Start()
    {
        UpdateCityUI();
    }

    public void SendPopulation()
    {
        GameWorld.Instance.NewPopulation(currentPopulation);
    }

    public void UpdatePopulation(int n_population, bool substract)
    {
        if (substract)
            currentPopulation -= n_population;
        else
            currentPopulation += n_population;

        UpdateCityUI();
    }

    public void UpdateHealthCare(int n_healthcare, bool substract)
    {
        if (substract)
            healthCare -= n_healthcare;
        else
            healthCare += n_healthcare;

        UpdateCityUI();
    }

    public void ConstructBuilding(string buildingName)
    {
        Debug.Log("Building called");
        foreach (Transform building in buildings)
        {
            if (building.name == buildingName)
            {
                Debug.Log("Building found");
                Instantiate(boxPrefab, new Vector3(building.gameObject.transform.position.x, 0.6f, building.gameObject.transform.position.z), Quaternion.identity);
                building.gameObject.SetActive(true);
            }
        }
    }
    public void UpdateCityUI()
    {
        cityui.UpdateUI(currentPopulation, healthCare, deathrate, economy, casesPrevented, cureSucces);
    }
}
