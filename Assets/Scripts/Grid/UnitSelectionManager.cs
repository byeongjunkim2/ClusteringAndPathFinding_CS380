using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private GameObject UnitsFolder;

    // -- private

    private Vector3 startMousePosition;
    private bool isDragging = false;

    void Update()
    {
        // when press left button
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            startMousePosition = Input.mousePosition;
        }

        // when release button
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            SelectUnits();
        }
    }

    void OnGUI()
    {
        if (isDragging)
        {
            Rect rect = GetScreenRect(startMousePosition, Input.mousePosition);
            DrawScreenRect(rect, new Color(0, 0, 1, 0.1f));
            DrawScreenRectBorder(rect, 2, Color.blue);
        }
    }

    void SelectUnits()
    {
        Rect selectionRect = GetScreenRect(startMousePosition, Input.mousePosition);


        Release();

        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            Vector3 screenPos = mainCamera.WorldToScreenPoint(unit.transform.position);
            screenPos.y = Screen.height - screenPos.y; 

            if (selectionRect.Contains(screenPos))
            {
                unit.transform.SetParent(this.transform);
            }
        }
    }

    void Release()
    {
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        foreach (Transform child in children)
        {
            child.SetParent(UnitsFolder.transform);
        }
    }

    Rect GetScreenRect(Vector2 screenPosition1, Vector2 screenPosition2)
    {
        // rect
        float xMin = Mathf.Min(screenPosition1.x, screenPosition2.x);
        float xMax = Mathf.Max(screenPosition1.x, screenPosition2.x);
        float yMin = Mathf.Min(screenPosition1.y, screenPosition2.y);
        float yMax = Mathf.Max(screenPosition1.y, screenPosition2.y);

        return new Rect(xMin, Screen.height - yMax, xMax - xMin, yMax - yMin);
    }

    void DrawScreenRect(Rect rect, Color color)
    {
        GUI.color = color;
        GUI.DrawTexture(rect, Texture2D.whiteTexture);
        GUI.color = Color.white;
    }

    void DrawScreenRectBorder(Rect rect, float thickness, Color color)
    {
        // draw
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color); 
        DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color); 
        DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color); 
        DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color); 
    }
}