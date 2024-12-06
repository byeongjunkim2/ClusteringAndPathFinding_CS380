using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoidUISliderController : MonoBehaviour
{
    [Header("References")]
    public BoidManager boidManager;

    public Slider speedSlider;
    public Slider neighborRadiusSlider;
    public Slider separationRadiusSlider;
    public Slider separationWeightSlider;
    public Slider alignmentWeightSlider;
    public Slider cohesionWeightSlider;

    void Start()
    {
        UpdateSlider();

        speedSlider.onValueChanged.AddListener(OnSpeedChanged);
        neighborRadiusSlider.onValueChanged.AddListener(OnNeighborRadiusChanged);
        separationRadiusSlider.onValueChanged.AddListener(OnSeparationRadiusChanged);
        separationWeightSlider.onValueChanged.AddListener(OnSeparationWeightChanged);
        alignmentWeightSlider.onValueChanged.AddListener(OnAlignmentWeightChanged);
        cohesionWeightSlider.onValueChanged.AddListener(OnCohesionWeightChanged);
    }

    public void UpdateSlider()
    {
        if (boidManager.boidBehaviors.Count > 0)
        {
            speedSlider.value = boidManager.boidBehaviors[0].speed;
            neighborRadiusSlider.value = boidManager.boidBehaviors[0].neighborRadius;
            separationRadiusSlider.value = boidManager.boidBehaviors[0].separationRadius;
            separationWeightSlider.value = boidManager.boidBehaviors[0].separationWeight;
            alignmentWeightSlider.value = boidManager.boidBehaviors[0].alignmentWeight;
            cohesionWeightSlider.value = boidManager.boidBehaviors[0].cohesionWeight;
        }
    }


    public void OnSpeedChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.speed = newValue;
        }
    }

    public void OnNeighborRadiusChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.neighborRadius = newValue;
        }
    }

    public void OnSeparationRadiusChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.separationRadius = newValue;
        }
    }

    public void OnSeparationWeightChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.separationWeight = newValue;
        }
    }

    public void OnAlignmentWeightChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.alignmentWeight = newValue;
        }
    }

    public void OnCohesionWeightChanged(float newValue)
    {
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.cohesionWeight = newValue;
        }
    }

    void OnDestroy()
    {
        if (speedSlider != null)
            speedSlider.onValueChanged.RemoveListener(OnSpeedChanged);
        if (neighborRadiusSlider != null)
            neighborRadiusSlider.onValueChanged.RemoveListener(OnNeighborRadiusChanged);
        if (separationRadiusSlider != null)
            separationRadiusSlider.onValueChanged.RemoveListener(OnSeparationRadiusChanged);
        if (separationWeightSlider != null)
            separationWeightSlider.onValueChanged.RemoveListener(OnSeparationWeightChanged);
        if (alignmentWeightSlider != null)
            alignmentWeightSlider.onValueChanged.RemoveListener(OnAlignmentWeightChanged);
        if (cohesionWeightSlider != null)
            cohesionWeightSlider.onValueChanged.RemoveListener(OnCohesionWeightChanged);
    }
}