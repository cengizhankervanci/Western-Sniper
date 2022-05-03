using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float fireTime = 3;
    [SerializeField] Animator anim = null;
    [SerializeField] GameObject ammoObject = null;
    private bool fire = false;

    private void FireFree()
    {
        transform.LookAt(PlayerHealthController.Instance.transform.position);
        anim.SetBool("Shoot", true);
        fire = true;
    }

    private void Update()
    {
        if (fire)
        {
            fireTime -= Time.deltaTime;
            if (fireTime < 0)
            {
                GameObject x = Instantiate(ammoObject, transform.position + Vector3.up * 2F, Quaternion.Euler(-90, 0, 0));
                x.transform.DOMove(PlayerHealthController.Instance.transform.position + new Vector3(Random.Range(-2F, 2F), 0, 0), 1F);
                fireTime = 3;
            }
        }
        
    }

    private void OnEnable()
    {
        GameManager.CloseZoom += FireFree;
    }

    private void OnDisable()
    {
        GameManager.CloseZoom -= FireFree;
    }
}
