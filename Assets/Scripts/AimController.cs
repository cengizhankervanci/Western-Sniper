using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : Singleton<AimController>
{
    RaycastHit hit;

    public VariableJoystick joystick;

    private float currentAngleX;
    private float currentAngleY;
    private float newAngleX;
    private float newAngleY;

    [SerializeField] GameObject shootFX;

    void FixedUpdate()
    {
        Vector3 direction = Vector3.down * joystick.Vertical * 1.5f + Vector3.right * joystick.Horizontal * 1.5f;
        transform.Rotate(direction.y, direction.x, 0);
        newAngleX = currentAngleX + direction.x;
        newAngleY = currentAngleY + direction.y;
        SetAngle(newAngleX, newAngleY);

        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.TryGetComponent(out EnemyController enemy))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);

                if (Input.GetMouseButtonUp(0))
                {
                    Instantiate(shootFX, hit.point, Quaternion.identity);
                    enemy.GetComponent<Animator>().SetBool("Dead", true);
                    enemy.enabled = false;
                    GameManager.CloseZoom?.Invoke();
                }
            }

            if(hit.collider!=null && Input.GetMouseButtonUp(0))
            {
                Instantiate(shootFX, hit.point, Quaternion.identity);
                GameManager.CloseZoom?.Invoke();
            }
        }
    }

    void SetAngle(float rotX, float rotY)
    {
        currentAngleX = Mathf.Clamp(rotX, -50, 50);
        currentAngleY = Mathf.Clamp(rotY, -50, 50);
        transform.rotation = Quaternion.Euler(rotY, rotX, 0);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
