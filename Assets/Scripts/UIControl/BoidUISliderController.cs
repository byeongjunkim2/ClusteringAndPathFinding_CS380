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
            speedSlider.value = boidManager.boidBehaviors[0].data.speed;
            neighborRadiusSlider.value = boidManager.boidBehaviors[0].data.neighborRadius;
            separationRadiusSlider.value = boidManager.boidBehaviors[0].data.separationRadius;
            separationWeightSlider.value = boidManager.boidBehaviors[0].data.separationWeight;
            alignmentWeightSlider.value = boidManager.boidBehaviors[0].data.alignmentWeight;
            cohesionWeightSlider.value = boidManager.boidBehaviors[0].data.cohesionWeight;
        }
    }


    public void OnSpeedChanged(float newValue)
    {
        boidManager.behaviorData.speed = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.speed = newValue;
        }
    }

    public void OnNeighborRadiusChanged(float newValue)
    {
        boidManager.behaviorData.neighborRadius = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.neighborRadius = newValue;
        }
    }

    public void OnSeparationRadiusChanged(float newValue)
    {
        boidManager.behaviorData.separationRadius = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.separationRadius = newValue;
        }
    }

    public void OnSeparationWeightChanged(float newValue)
    {
        boidManager.behaviorData.separationWeight = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.separationWeight = newValue;
        }
    }

    public void OnAlignmentWeightChanged(float newValue)
    {
        boidManager.behaviorData.alignmentWeight = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.alignmentWeight = newValue;
        }
    }

    public void OnCohesionWeightChanged(float newValue)
    {
        boidManager.behaviorData.cohesionWeight = newValue;
        foreach (var boid in boidManager.boidBehaviors)
        {
            boid.data.cohesionWeight = newValue;
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