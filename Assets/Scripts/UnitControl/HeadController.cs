using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    public float speed = 5f; // 이동 속도

    void Update()
    {
        // 이동 입력 받기
        float horizontal = Input.GetAxis("Horizontal"); // A/D 또는 ←/→
        float vertical = Input.GetAxis("Vertical");     // W/S 또는 ↑/↓

        float upDown = 0f;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))              // ↑ 키
        {
            upDown = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftShift))       // ↓ 키
        {
            upDown = -1f;
        }

        // 이동 벡터 계산 (앞뒤, 좌우, 상하)
        Vector3 direction = new Vector3(horizontal, upDown, vertical).normalized;

        // 이동
        if (direction.magnitude > 0)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
