//using Cinemachine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float xySpeed = 18;
    public float lookSpeed = 340;
    public float forwardSpeed = 12;

    public float barrelRollSpeed = 720.0f; // 720 degrees per second is a full rotation in 0.5 seconds
    public float barrelRollCooldown = 2.0f; // 2 seconds cooldown

    private Transform playerModel;


    public Transform aimTarget;
    public CinemachineDollyCart dolly;

    public bool joystick = true;

    private bool inBarrelRoll = false;
    private float lastBarrelRollTime = -Mathf.Infinity;

    public float boostMultiplier = 2.0f; // Multiply forward speed by this value when boosting
    public float breakMultiplier = 0.5f; // Multiply forward speed by this value when breaking
    public float boostBreakDuration = 3.0f; // Maximum duration for boost or break
    public float boostBreakCooldown = 2.0f; // Cooldown duration after boost or break ends

    private float lastBoostBreakTime = -Mathf.Infinity;
    private bool inBoostOrBreak = false;

    // Speed boost brake
    public CinemachineVirtualCamera virtualCamera;
    public float boostFOV = 70.0f; // Increased FOV for the boost
    public float breakFOV = 50.0f; // Decreased FOV for the break
    public float normalFOV = 60.0f; // Normal FOV when not boosting or breaking
    public float fovLerpSpeed = 5.0f; // Speed for lerping between FOVs

    public ParticleSystem barrel;

    public AudioSource barrelRollAudio;
    public AudioSource boostAudio;
    public AudioSource brakeAudio;


    private void Start()
    {
        playerModel = transform.GetChild(0);
        //SetSpeed(forwardSpeed);
    }

    private void Update()
    {
        float h = joystick ? Input.GetAxis("Horizontal") : Input.GetAxis("Mouse X");
        float v = joystick ? Input.GetAxis("Vertical") : Input.GetAxis("Mouse Y");

        LocalMove(h, v, xySpeed);
        RotationLook(h, v, lookSpeed);

        if (!inBarrelRoll) // Only apply lean when not in barrel roll
        {
            HorizontalLean(playerModel, h, 80, 0.1f);
        }
        ClampPosition();

        // Barrel Roll Input
        if (Input.GetKeyDown(KeyCode.Q) && !inBarrelRoll && Time.time - lastBarrelRollTime > barrelRollCooldown)
        {
            barrelRollAudio.Play();
            barrel.Play();
            StartCoroutine(BarrelRoll());
        }

        // Boost or Break Input
        if ((Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Z)) && !inBoostOrBreak && Time.time - lastBoostBreakTime > boostBreakCooldown)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                boostAudio.Play();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                brakeAudio.Play();
            }
            StartCoroutine(HandleBoostBreak());
        }
    }

    IEnumerator BarrelRoll()
    {
        inBarrelRoll = true;
        lastBarrelRollTime = Time.time;

        float barrelRollDuration = 360f / barrelRollSpeed;
        float elapsedTime = 0;

        while (elapsedTime < barrelRollDuration)
        {
            float rotationAmount = barrelRollSpeed * Time.deltaTime;
            playerModel.Rotate(0, 0, rotationAmount);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 finalEulerAngles = playerModel.localEulerAngles;
        finalEulerAngles.z = Mathf.Round(finalEulerAngles.z / 360) * 360;
        playerModel.localEulerAngles = finalEulerAngles;

        inBarrelRoll = false;
    }

    IEnumerator HandleBoostBreak()
    {
        inBoostOrBreak = true;
        float elapsedTime = 0;

        // Store the original forward speed
        float originalForwardSpeed = dolly.m_Speed;

        while (elapsedTime < boostBreakDuration && (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Z)))
        {
            elapsedTime += Time.deltaTime;

            // Update the dolly speed and FOV based on the input
            if (Input.GetKey(KeyCode.X))
            {
                dolly.m_Speed = originalForwardSpeed * boostMultiplier;
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, boostFOV, fovLerpSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                dolly.m_Speed = originalForwardSpeed * breakMultiplier;
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, breakFOV, fovLerpSpeed * Time.deltaTime);
            }

            yield return null;
        }

        // Reset dolly speed to the original speed
        dolly.m_Speed = originalForwardSpeed;

        // Lerp back to the normal FOV
        while (Mathf.Abs(virtualCamera.m_Lens.FieldOfView - normalFOV) > 0.1f)
        {
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, normalFOV, fovLerpSpeed * Time.deltaTime);
            yield return null;
        }

        virtualCamera.m_Lens.FieldOfView = normalFOV;

        lastBoostBreakTime = Time.time;
        inBoostOrBreak = false;
    }

    // Allows the spaceship to follow the cursor
    void LocalMove(float x, float y, float xySpeed)
    {
        transform.localPosition += new Vector3(x, y, 0) * xySpeed * Time.deltaTime;
        ClampPosition();
    }

    // To avoid the player from getting out of the camera view
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(aimTarget.position, 0.5f);
        Gizmos.DrawSphere(aimTarget.position, 0.15f);
    }
}
