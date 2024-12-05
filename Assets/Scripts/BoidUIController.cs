using TMPro;
using UnityEngine;

public class BoidUIController : MonoBehaviour
{
    public TMP_Dropdown separationDropdown; // TextMeshPro Dropdown
    public TMP_Dropdown cohesionDropdown;  // TextMeshPro Dropdown
    public BoidManager boidManager;        // BoidManager 참조

    void Start()
    {
        // Dropdown 변경 이벤트 연결
        separationDropdown.onValueChanged.AddListener(OnSeparationDropdownChanged);
        cohesionDropdown.onValueChanged.AddListener(OnCohesionDropdownChanged);
    }

    private void OnSeparationDropdownChanged(int index)
    {
        // 모든 Boid의 SeparationMode 변경
        boidManager.UpdateSeparationMode((BoidBehavior.SeparationMode)index);
    }

    private void OnCohesionDropdownChanged(int index)
    {
        // 모든 Boid의 CohesionMode 변경
        boidManager.UpdateCohesionMode((BoidBehavior.CohesionMode)index);
    }
}
