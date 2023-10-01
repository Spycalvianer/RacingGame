using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Text speedText;
    public Rigidbody rb;
    private float speedToShow;
    public float maxSpeedOnUI;
    public CarController carController;
    public GameObject pausePanel;
    public Image speedImage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();

    }
    private void Start()
    {
        pausePanel.SetActive(false);
    }
    private void Update()
    {
        UISpeed();
        if(Input.GetKeyDown(KeyCode.Escape)) Pause();
        StopTimeInPause();
    }
    public void UISpeed()
    {
        //speedToShow = (rb.velocity.magnitude * maxSpeedOnUI) / carController.maxSpeed;
        speedToShow = rb.velocity.magnitude * 3.6f;
        speedText.text = "Speed " + Mathf.RoundToInt(speedToShow);

        speedImage.fillAmount = 1 - (((carController.maxSpeed * 3.6f) - speedToShow) / (carController.maxSpeed * 3.6f));
    }
    public void Pause()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }
    public void StopTimeInPause()
    {
        if (pausePanel.activeSelf == true)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;
    }
}
