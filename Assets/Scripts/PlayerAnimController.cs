using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] Animator playerAnim = null;

    private void GetUp()
    {
        playerAnim.SetBool("Up", true);
    }

    private void StandAgain()
    {
        playerAnim.SetBool("Up", false);
    }

    private void OnEnable()
    {
        GameManager.CloseZoom += StandAgain;
        GameManager.OpenZoom += GetUp;
    }

    private void OnDisable()
    {
        GameManager.CloseZoom -= StandAgain;
        GameManager.OpenZoom -= GetUp;
    }
}
