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

    [SerializeField] List<Transform> movePoints = new List<Transform>();
    private Transform targetMovePoints;
    private int wayPointIndex = 0;
    private float minDistance = 0.1F;
    private int lastPointIndex;

    [SerializeField] private bool walkAble = false;

    private void Start()
    {
        if (walkAble)
        {
            lastPointIndex = movePoints.Count - 1;
            targetMovePoints = movePoints[wayPointIndex];
            anim.SetBool("Walk", true);
        }
    }

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
                x.transform.DOMove(PlayerHealthController.Instance.transform.position + new Vector3(Random.Range(-0.5F, 0.5F), Random.Range(0F, 0.5F), 0), 0.5F).OnComplete(() => Destroy(x));
                fireTime = 3;
            }
            walkAble = false;
        }

        if (walkAble)
        {
            Vector3 directionToTarget = targetMovePoints.position - transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, 3F * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, targetMovePoints.position);
            CheckWaypoint(distance);

            transform.position = Vector3.MoveTowards(transform.position, targetMovePoints.position, 1F * Time.deltaTime);
        }
    }

    void CheckWaypoint(float activeDistance)
    {
        if (activeDistance <= minDistance)
        {
            wayPointIndex++;
            UpdateWaypoint();
        }
    }

    void UpdateWaypoint()
    {
        if (wayPointIndex > lastPointIndex)
        {
            wayPointIndex = 0;
        }

        targetMovePoints = movePoints[wayPointIndex];
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
