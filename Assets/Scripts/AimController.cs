using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    RaycastHit hit;

    public VariableJoystick joystick;

    private float currentAngleX;
    private float currentAngleY;
    private float newAngleX;
    private float newAngleY;

    void Start()
    {

    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.down * joystick.Vertical + Vector3.right * joystick.Horizontal;
        transform.Rotate(direction.y, direction.x, 0);
        newAngleX = currentAngleX + direction.x;
        newAngleY = currentAngleY + direction.y;
        SetAngle(newAngleX, newAngleY);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.TryGetComponent(out EnemyController enemy))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("ATES EDİLDİ.");
                    enemy.GetComponent<Animator>().SetBool("Dead", true);
                }
            }
        }
    }

    void SetAngle(float rotX, float rotY)
    {
        currentAngleX = Mathf.Clamp(rotX, -50, 50);
        currentAngleY = Mathf.Clamp(rotY, -50, 50);
        transform.rotation = Quaternion.Euler(rotY, rotX, 0);
    }
}
