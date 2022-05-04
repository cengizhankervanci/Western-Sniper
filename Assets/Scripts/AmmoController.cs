using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
    [SerializeField] GameObject ammoFX;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject)
        {
            GameObject x= Instantiate(ammoFX, other.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(x, 0.2F);
        }

        if(other.TryGetComponent(out PlayerHealthController player))
        {
            player.DamageTaken(5);
        }
    }
}
