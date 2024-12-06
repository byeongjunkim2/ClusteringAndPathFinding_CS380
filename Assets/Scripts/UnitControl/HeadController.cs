using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public float speed = 5f; // �̵� �ӵ�

    void Update()
    {
        // �̵� �Է� �ޱ�
        float horizontal = Input.GetAxis("Horizontal"); // A/D �Ǵ� ��/��
        float vertical = Input.GetAxis("Vertical");     // W/S �Ǵ� ��/��

        float upDown = 0f;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))              // �� Ű
        {
            upDown = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftShift))       // �� Ű
        {
            upDown = -1f;
        }

        // �̵� ���� ��� (�յ�, �¿�, ����)
        Vector3 direction = new Vector3(horizontal, upDown, vertical).normalized;

        // �̵�
        if (direction.magnitude > 0)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
