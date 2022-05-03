using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPanelController : Singleton<AimPanelController>
{
    [SerializeField] GameObject panel = null;
    [SerializeField] GameObject zoomObject = null;

    private void ZoomOpen()
    {
        panel.SetActive(false);
        zoomObject.SetActive(true);
    }

    private void ZoomClose()
    {
        panel.SetActive(true);
        zoomObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.CloseZoom += ZoomClose;
        GameManager.OpenZoom += ZoomOpen;
    }

    private void OnDisable()
    {
        GameManager.CloseZoom -= ZoomClose;
        GameManager.OpenZoom -= ZoomOpen;
    }
}
