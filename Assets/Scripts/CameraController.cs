using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform m_objectToLookAt;
    [SerializeField]
    private float m_rotationSpeed = 1.0f;
    [SerializeField]
    private Vector2 m_clampingXRotationValues = Vector2.zero;
    [SerializeField]
    private Vector2 m_cameraClamping = Vector2.zero;
    [SerializeField]
    private float m_desireDistance;
    [SerializeField]
    private float m_lerpspeed = 5.0f;
    [SerializeField]

    private float smoothFactor;
    [SerializeField]
    private CharacterControllerStateMachine m_CharacterRef;

    private bool isCameraClamped = false;
    private bool isCameraObstructed = false;


    // Update is called once per frame
    void Update()
    {
        if(m_CharacterRef.InCinematic == true)
        {
            return;
        }
        else
        {
            UpdateHorizontalMovements();
            UpdateVerticalMovements();



            UpdateCameraScroll();
        }
       
 
    }

    public void OnStart()
    {
        m_desireDistance = Vector3.Distance(transform.position, m_objectToLookAt.position);
    }
    private void FixedUpdate()
    {
        
        MoveCameraInFrontOfObstructionsFUpdate();
            
    }

   

    private void UpdateVerticalMovements()
    {
        float currentAngleY = Input.GetAxis("Mouse Y") * m_rotationSpeed;
        float eulersAngleX = transform.rotation.eulerAngles.x;

        float comparisonAngle = eulersAngleX + currentAngleY;

        comparisonAngle = ClampAngle(comparisonAngle);

        if ((currentAngleY < 0 && comparisonAngle < m_clampingXRotationValues.x)
            || (currentAngleY > 0 && comparisonAngle > m_clampingXRotationValues.y))
        {
            return;
        }
        transform.RotateAround(m_objectToLookAt.position, transform.right, currentAngleY);
    }

    private void UpdateHorizontalMovements()
    {
        float currentAngleX = Input.GetAxis("Mouse X") * m_rotationSpeed;
        m_objectToLookAt.transform.Rotate(new Vector3(0, currentAngleX, 0), Space.World);
    }

    private void UpdateCameraScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            var distanceCameraToCharacter = transform.position - m_objectToLookAt.position;
            var currentDistance = distanceCameraToCharacter.magnitude;

            m_desireDistance += Input.mouseScrollDelta.y * 1;

            // Clamp m_desireDistance to the desired range
            m_desireDistance = Mathf.Clamp(m_desireDistance, -10f, -2f);

            Debug.Log(m_desireDistance);

            // Calculate the target position based on clamped distance
            var targetPosition = m_objectToLookAt.position + transform.forward * -m_desireDistance;

            // Smoothly move the camera to the target position using Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);

            // Check for clamped distances and set the flag
            if (currentDistance <= m_cameraClamping.x)
            {
                isCameraClamped = true;
            }
            else if (currentDistance >= m_cameraClamping.y)
            {
                isCameraClamped = true;
            }
            else
            {
                isCameraClamped = false;
            }

            // Apply the clamped position only once when the camera is clamped
            if (isCameraClamped)
            {
                if (currentDistance <= m_cameraClamping.x)
                {
                    var clampedPosition = m_objectToLookAt.position + transform.forward * -m_cameraClamping.x;
                    transform.position = clampedPosition;
                }
                if (currentDistance >= m_cameraClamping.y)
                {
                    var clampedPosition = m_objectToLookAt.position + transform.forward * -m_cameraClamping.y;
                    transform.position = clampedPosition;
                }
            }
        }
    }



    private void MoveCameraInFrontOfObstructionsFUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        RaycastHit hit;

        var vecteurDiff = transform.position - m_objectToLookAt.position;
        var distance = vecteurDiff.magnitude;
        vecteurDiff.Normalize();

        if (Physics.Raycast(m_objectToLookAt.position, vecteurDiff, out hit, distance, layerMask))
        {
            // There is an obstruction between the focus and the camera
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff.normalized * hit.distance, Color.yellow);

            // Set the obstruction flag
            isCameraObstructed = true;

            // Calculate the new camera position in front of the obstruction
            Vector3 newCameraPosition = hit.point + (vecteurDiff.normalized * 0.1f); // Move slightly in front of the obstruction

            // Smoothly move the camera to the new position using Lerp
            transform.position = Vector3.Lerp(transform.position, newCameraPosition, Time.deltaTime * m_lerpspeed);
        }
        else
        {
            // No obstruction
            Debug.DrawRay(m_objectToLookAt.position, vecteurDiff, Color.white);

            // If the camera was obstructed in the previous frame, smoothly move it back to the desired position
            if (isCameraObstructed)
            {
                Vector3 targetPosition = m_objectToLookAt.position + (vecteurDiff.normalized * -m_desireDistance);
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * m_lerpspeed);

                // Check if the camera is close enough to the desired position, and if so, clear the obstruction flag
                if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
                {
                    isCameraObstructed = false;
                }
            }
            else
            {
                // Camera is not obstructed, allow for regular camera movement here
                // For example, you can handle camera movement using Input.mouseScrollDelta here
                var scrollDelta = Input.mouseScrollDelta.y;
                if (scrollDelta != 0)
                {
                    // Adjust the m_desireDistance based on scroll input
                    m_desireDistance += scrollDelta * 1;
                    m_desireDistance = Mathf.Clamp(m_desireDistance, -10f, -2f);
                }

                // Calculate the target position based on m_desireDistance
                Vector3 targetPosition = m_objectToLookAt.position + (vecteurDiff.normalized * -m_desireDistance);

                // Smoothly move the camera to the target position using Lerp
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * m_lerpspeed);
            }
        }
    }

    private float ClampAngle(float angle)
    {
        if (angle > 180)
        {
            angle -= 360;
        }
        return angle;
    }
}
