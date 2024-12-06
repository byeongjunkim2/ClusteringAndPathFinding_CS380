using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ToggleCanvas()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        }
    }
}