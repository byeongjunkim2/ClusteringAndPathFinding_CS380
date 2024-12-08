using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TestUIController : MonoBehaviour
{
    public TextMeshProUGUI indicatorText;

    public TextMeshProUGUI distanceAverageText;
    public TextMeshProUGUI distanceAverageMaxText;
    public TextMeshProUGUI distanceStdDevText;
    public TextMeshProUGUI distanceStdDevMaxText;

    public BoidManager boidManager;

    public Button separationLinearButton;
    public Button separationInverseButton;
    public Button cohesionCenterButton;
    public Button cohesionWeightedButton;
    public Button GenerateButton1;
    public Button GenerateButton2;

    float distanceAverageMax = 0;
    float distanceStdDevMax = 0;

    void Start()
    {
        separationLinearButton.onClick.AddListener(() =>
        {
            boidManager.SetMode(BoidBehavior.SeparationMode.Linear);
        });

        separationInverseButton.onClick.AddListener(() =>
        {
            boidManager.SetMode(BoidBehavior.SeparationMode.InverseSquare);
        });

        cohesionCenterButton.onClick.AddListener(() =>
        {
            boidManager.SetMode( BoidBehavior.CohesionMode.Center);
        });

        cohesionWeightedButton.onClick.AddListener(() =>
        {
            boidManager.SetMode(BoidBehavior.CohesionMode.Weighted);
        });

        GenerateButton1.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(new Vector3(0,0,0));
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
            boidManager.InitializeBoids();
        });

        GenerateButton2.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(new Vector3(50, 50, 50));
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
            boidManager.InitializeBoids();
        });
    }

    void OnModeButtonClicked(BoidBehavior.SeparationMode separationMode, BoidBehavior.CohesionMode cohesionMode)
    {
        boidManager.SetMode(separationMode);
        boidManager.SetMode(cohesionMode);
    }


    void Update()
    {
 
        float distanceAverage = boidManager.CalculateAverageNeighborDistance();
        if (distanceAverage > distanceAverageMax)
        {
            distanceAverageMax = distanceAverage;
        }
        float distanceStdDev = boidManager.CalculateNeighborDistanceStdDev();
        if (distanceStdDev > distanceStdDevMax)
        {
            distanceStdDevMax = distanceStdDev;
        }

        indicatorText.text = "Separation Mode: [" +
                             (boidManager.separationMode == BoidBehavior.SeparationMode.Linear
                                 ? "Linear"
                                 : "Inverse Square") +
                             "],  Cohesion Mode: [" +
                             (boidManager.cohesionMode == BoidBehavior.CohesionMode.Center
                                 ? "Center"
                                 : "Weighted") + "]";
        distanceAverageText.text = $"Distance Average: {distanceAverage}";
        distanceStdDevText.text = $"Distance Std Dev: {distanceStdDev:F2}";
        distanceAverageMaxText.text = $"Distance Average Max: {distanceAverageMax}";
        distanceStdDevMaxText.text = $"Distance Std Dev Max: {distanceStdDevMax:F2}";
    }

}
