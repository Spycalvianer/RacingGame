using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PanelActivation : MonoBehaviour
{
    public GameObject panel;
    private void Start()
    {
        panel.SetActive(false);
    }
    public void ActivatePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}
