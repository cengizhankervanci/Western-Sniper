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
        anim.SetBool("Shoot", true);
        fire = true;
    }

    private void Update()
    {
        if (fire)
        {
            transform.LookAt(PlayerHealthController.Instance.transform.position);
            fireTime -= Time.deltaTime;
            if (fireTime < 0)
            {
                GameObject x = Instantiate(ammoObject, transform.position + Vector3.up * 2F, Quaternion.Euler(-90, 0, 0));
                x.transform.DOMove(PlayerHealthController.Instance.transform.position + new Vector3(Random.Range(-0.5F, 0.5F), Random.Range(0F,0.5F), 0), 0.5F).OnComplete(()=>Destroy(x));
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
