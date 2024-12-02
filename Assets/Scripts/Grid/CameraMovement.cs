using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float panSpeed = 3.0f; // camera move speed
    public float topBottomEdgeCondition; // initilized on Start func
    public float rightLeftEdgeCondition;

    public bool enableMouseMovement = true;
    public bool enableKeyBoardMovement = true;



    // ----- private

    float screenWidth = Screen.width;
    float screenHeight = Screen.height;

    void Start()
    {
        float magicNumber = 40.0f / 1920;  // 0.0208
        topBottomEdgeCondition = screenWidth * magicNumber;
        rightLeftEdgeCondition = screenHeight * magicNumber;
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;


        // pan speed increase/decrease
        float modifier = 3.0f;
        if (Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.Plus)) // Plus not working ;
        {
            panSpeed = (panSpeed < (100 - modifier)) ? panSpeed + modifier : 100;

        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus) || Input.GetKeyDown(KeyCode.Minus))
        {
            panSpeed = (panSpeed > modifier) ? panSpeed - modifier : 0;
        }

        // mouse move?
        if (enableMouseMovement == true)
        {

            Vector3 mousePosition = Input.mousePosition;


            // top
            if (mousePosition.y >= screenHeight - topBottomEdgeCondition)
            {
                movement += Vector3.forward;
            }

            // bottom
            if (mousePosition.y <= topBottomEdgeCondition)
            {
                movement += Vector3.back;
            }

            // left
            if (mousePosition.x <= rightLeftEdgeCondition)
            {
                movement += Vector3.left;
            }

            // right
            if (mousePosition.x >= screenWidth - rightLeftEdgeCondition)
            {
                movement += Vector3.right;
            }
        }

        // keyboard move?
        if (enableKeyBoardMovement == true)
        {
            // top
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement += Vector3.forward;
            }

            // bottom
            if ( Input.GetKey(KeyCode.DownArrow))
            {
                movement += Vector3.back;
            }
            
            // left
            if ( Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector3.left;
            }

            // right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector3.right;
            }
        }

        // apply move
        transform.Translate(movement * panSpeed * Time.deltaTime, Space.World);
    }
}