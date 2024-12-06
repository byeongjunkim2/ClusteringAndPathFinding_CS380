using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TestUIController : MonoBehaviour
{
    public TextMeshProUGUI distanceAverageText;
    public TextMeshProUGUI distanceAverageMaxText;
    public TextMeshProUGUI distanceStdDevText;
    public TextMeshProUGUI distanceStdDevMaxText;

    public BoidManager boidManager;

    public Button separationLinearButton;
    public Button separationInverseButton;
    public Button cohesionCenterButton;
    public Button cohesionWeightedButton;

    float distanceAverageMax = 0;
    float distanceStdDevMax = 0;

    void Start()
    {
        // 각 버튼의 클릭 이벤트 연결
        separationLinearButton.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(Vector3.zero);
            boidManager.SetMode(BoidBehavior.SeparationMode.Linear, BoidBehavior.CohesionMode.Center);
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
        });

        separationInverseButton.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(Vector3.zero);
            boidManager.SetMode(BoidBehavior.SeparationMode.InverseSquare, BoidBehavior.CohesionMode.Center);
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
        });

        cohesionCenterButton.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(new Vector3(50, 50, 50));
            boidManager.SetMode(BoidBehavior.SeparationMode.Linear, BoidBehavior.CohesionMode.Center);
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
        });

        cohesionWeightedButton.onClick.AddListener(() =>
        {
            boidManager.SetSpawnArea(new Vector3(50, 50, 50));
            boidManager.SetMode(BoidBehavior.SeparationMode.Linear, BoidBehavior.CohesionMode.Weighted);
            distanceAverageMax = 0;
            distanceStdDevMax = 0;
        });
    }

    void OnModeButtonClicked(BoidBehavior.SeparationMode separationMode, BoidBehavior.CohesionMode cohesionMode)
    {
        // BoidManager의 모드 설정 호출
        boidManager.SetMode(separationMode, cohesionMode);
    }


    void Update()
    {
        // BoidManager에서 테스트 데이터 가져오기
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
        // 결과 업데이트
        distanceAverageText.text = $"Distance Average: {distanceAverage}";
        distanceStdDevText.text = $"Distance Std Dev: {distanceStdDev:F2}";
        distanceAverageMaxText.text = $"Distance Average Max: {distanceAverageMax}";
        distanceStdDevMaxText.text = $"Distance Std Dev Max: {distanceStdDevMax:F2}";
    }

}
