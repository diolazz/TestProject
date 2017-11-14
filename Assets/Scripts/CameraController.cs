using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera manager
/// </summary>

[RequireComponent(typeof (Camera))]
public class CameraController : MonoBehaviour
{
    private Transform currentCameraTransform;

    [SerializeField] private float screenEdgeMovementSpeed = 3f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float panningSpeed = 10f;
    [SerializeField] private float mouseRotationSpeed = 10f;

    private LayerMask groundMask = -1;

    private float maxHeight = 10f;
    private float minHeight = 15f;
    private float heightDampening = 5f;
    private float scrollWheelZoomingSensitivity = 25f;

    private float zoomPos;

    private float limitX = 50f;
    private float limitY = 50f;

#region Mouse input
    private KeyCode panningKey = KeyCode.Mouse2;
    private string zoomingAxis = "Mouse ScrollWheel";
    private KeyCode mouseRotationKey = KeyCode.Mouse1;
#endregion


    private void Start()
    {
        currentCameraTransform = transform;
    }


    private void FixedUpdate()
    {
        CameraUpdate();
    }


    /// <summary>
    /// Handle mouse input and update camera position
    /// </summary>
    private void CameraUpdate()
    {
        Move();
        HeightCalculation();
        Rotation();
        LimitPosition();
    }

    /// <summary>
    /// Move camera
    /// </summary>
    private void Move()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        if (Input.GetKey(panningKey) && mouseAxis != Vector2.zero)
        {
            Vector3 desiredMove = new Vector3(-mouseAxis.x, 0, -mouseAxis.y);

            desiredMove *= panningSpeed;
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f))*desiredMove;
            desiredMove = currentCameraTransform.InverseTransformDirection(desiredMove);

            currentCameraTransform.Translate(desiredMove, Space.Self);
        }
    }

    /// <summary>
    /// Calculate height of camera
    /// </summary>
    private void HeightCalculation()
    {
        float distanceToGround = DistanceToGround();
        zoomPos += Input.GetAxis(zoomingAxis)*Time.deltaTime*scrollWheelZoomingSensitivity;

        zoomPos = Mathf.Clamp01(zoomPos);

        float targetHeight = Mathf.Lerp(minHeight, maxHeight, zoomPos);
        float difference = 0;

        if (distanceToGround != targetHeight)
        {
            difference = targetHeight - distanceToGround;
        }

        currentCameraTransform.position = Vector3.Lerp(currentCameraTransform.position,
            new Vector3(currentCameraTransform.position.x, targetHeight + difference, currentCameraTransform.position.z),
            Time.deltaTime*heightDampening);
    }

    /// <summary>
    /// Rotate camera
    /// </summary>
    private void Rotation()
    {
        if (Input.GetKey(mouseRotationKey))
            currentCameraTransform.Rotate(Vector3.up, -Input.GetAxis("Mouse X")*Time.deltaTime*mouseRotationSpeed,
                Space.World);
    }

    /// <summary>
    /// Limit camera position
    /// </summary>
    private void LimitPosition()
    {
        currentCameraTransform.position = new Vector3(Mathf.Clamp(currentCameraTransform.position.x, -limitX, limitX),
            currentCameraTransform.position.y,
            Mathf.Clamp(currentCameraTransform.position.z, -limitY, limitY));
    }

    /// <summary>
    /// Distance to ground
    /// </summary>
    /// <returns></returns>
    private float DistanceToGround()
    {
        Ray ray = new Ray(currentCameraTransform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, groundMask.value))
            return (hit.point - currentCameraTransform.position).magnitude;

        return 0f;
    }

}
