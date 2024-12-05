using TMPro;
using UnityEngine;

public class BoidUIController : MonoBehaviour
{
    public TMP_Dropdown separationDropdown; // TextMeshPro Dropdown
    public TMP_Dropdown cohesionDropdown;  // TextMeshPro Dropdown
    public BoidManager boidManager;        // BoidManager ����

    void Start()
    {
        // Dropdown ���� �̺�Ʈ ����
        separationDropdown.onValueChanged.AddListener(OnSeparationDropdownChanged);
        cohesionDropdown.onValueChanged.AddListener(OnCohesionDropdownChanged);
    }

    private void OnSeparationDropdownChanged(int index)
    {
        // ��� Boid�� SeparationMode ����
        boidManager.UpdateSeparationMode((BoidBehavior.SeparationMode)index);
    }

    private void OnCohesionDropdownChanged(int index)
    {
        // ��� Boid�� CohesionMode ����
        boidManager.UpdateCohesionMode((BoidBehavior.CohesionMode)index);
    }
}
