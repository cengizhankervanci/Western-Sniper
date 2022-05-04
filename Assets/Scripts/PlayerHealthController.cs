using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerHealthController : Singleton<PlayerHealthController>
{
    [SerializeField] int myHealth = 100;
    [SerializeField] GameObject damageObject = null;

    public void DamageTaken(int value)
    {
        myHealth -= value;
        damageObject.transform.DOScale(Vector3.one, 0.5F).OnComplete(() => damageObject.transform.DOScale(Vector3.one * 3, 0.5F));
    }
}
