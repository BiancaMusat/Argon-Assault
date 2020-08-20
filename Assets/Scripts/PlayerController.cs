using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m/s")] [SerializeField] float controlSpeed = 4f;
    [Tooltip("In m/s")] [SerializeField] float xRange = 5f;
    [Tooltip("In m/s")] [SerializeField] float yRange = 5f;
    [SerializeField] GameObject[] guns;

    [Header("Screen Position")]
    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float posYawFactor = 5f;

    [Header("Control Throw")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool controlEnabled = true;

    // Update is called once per frame
    void Update()
    {
        if (controlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        } else
        {
            DeactivateGuns();
        }
    }

    private void DeactivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    void OnPlayerDeath() // called by string refernce from collider
    {
        controlEnabled = false;
    }

    private void ProcessRotation()
    {
        float pitch = transform.localPosition.y * posPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * posYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffset;
        rawNewXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * controlSpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffset;
        rawNewYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(rawNewXPos, rawNewYPos, transform.localPosition.z);
    }
}
