using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDFollowCamera : MonoBehaviour {

    [SerializeField] private Camera m_camera;
    [SerializeField] private float m_positionDamping;
    [SerializeField] private float m_rotationLerp;
    [SerializeField] private float m_relativeDistance = 1f;
    [SerializeField] private Rect m_screenBounds = new Rect(0.25f, 0.25f, 0.5f, 0.5f);
    [SerializeField] private Transform m_cursorTransform;
    private Vector3 m_velocity;

    void Start()
    {
        if (m_camera == null)
        {
            m_camera = Camera.main;
        }
    }

    void Update()
    {
        //position
        Vector3 currentHUDPosition = transform.position;
        Vector2 screenHUDPosition = m_camera.WorldToViewportPoint(currentHUDPosition);
        Vector3 targetPosition;

        Quaternion currentRotation;
        Quaternion targetRotation;

        Vector3 position;
        Quaternion rotation;

        Rect standradRect = new Rect(0f, 0f, 1f, 1f);

        //if (!standradRect.Contains(screenHUDPosition))
        //{
        //    //transform.position = m_camera.transform.position + m_camera.transform.forward * m_relativeDistance;
        //    targetPosition = m_camera.transform.position + m_camera.transform.forward * m_relativeDistance;
        //}

        //else if (!m_screenBounds.Contains(screenHUDPosition))
        //{
        //    Vector3 closestPoint;
        //    closestPoint.x = Mathf.Clamp(screenHUDPosition.x, m_screenBounds.xMin, m_screenBounds.xMax);
        //    closestPoint.y = Mathf.Clamp(screenHUDPosition.y, m_screenBounds.yMin, m_screenBounds.yMax);
        //    closestPoint.z = m_relativeDistance;
        //    targetPosition = m_camera.ViewportToWorldPoint(closestPoint);
        //}

        //else
        //{
        //    targetPosition = m_camera.ViewportToWorldPoint(new Vector3(screenHUDPosition.x, screenHUDPosition.y, m_relativeDistance));
        //}

        ////Rotation
        //currentRotation = transform.rotation;
        //targetRotation = Quaternion.LookRotation(targetPosition - m_camera.transform.position, Vector3.up);

        //position = Vector3.SmoothDamp(currentHUDPosition, targetPosition, ref m_velocity, m_positionDamping * Time.deltaTime);
        //rotation = Quaternion.Slerp(currentRotation, targetRotation, m_rotationLerp * Time.deltaTime);
        //transform.SetPositionAndRotation(position, rotation);

        if (!standradRect.Contains(screenHUDPosition))
        {
            //targetPosition = m_camera.transform.position + m_camera.transform.forward * m_relativeDistance;
            //targetRotation = Quaternion.LookRotation(targetPosition - m_camera.transform.position, Vector3.up);
            //transform.SetPositionAndRotation(targetPosition, targetRotation);
            Vector2 cursorPosition = m_camera.WorldToViewportPoint(m_cursorTransform.position);

            Vector3 closestPoint;
            closestPoint.x = cursorPosition.x;
            closestPoint.y = cursorPosition.y;
            closestPoint.z = m_relativeDistance;
            targetPosition = m_camera.ViewportToWorldPoint(closestPoint);
            currentRotation = transform.rotation;
            targetRotation = Quaternion.LookRotation(targetPosition - m_camera.transform.position, Vector3.up);

            position = Vector3.SmoothDamp(currentHUDPosition, targetPosition, ref m_velocity, m_positionDamping * Time.deltaTime);
            rotation = Quaternion.Slerp(currentRotation, targetRotation, m_rotationLerp * Time.deltaTime);
            transform.SetPositionAndRotation(position, rotation);

        }
        else if (!m_screenBounds.Contains(screenHUDPosition))
        {
            Vector3 closestPoint;
            closestPoint.x = Mathf.Clamp(screenHUDPosition.x, m_screenBounds.xMin, m_screenBounds.xMax);
            closestPoint.y = Mathf.Clamp(screenHUDPosition.y, m_screenBounds.yMin, m_screenBounds.yMax);
            closestPoint.z = m_relativeDistance;
            targetPosition = m_camera.ViewportToWorldPoint(closestPoint);
            currentRotation = transform.rotation;
            targetRotation = Quaternion.LookRotation(targetPosition - m_camera.transform.position, Vector3.up);

            position = Vector3.SmoothDamp(currentHUDPosition, targetPosition, ref m_velocity, m_positionDamping * Time.deltaTime);
            rotation = Quaternion.Slerp(currentRotation, targetRotation, m_rotationLerp * Time.deltaTime);
            transform.SetPositionAndRotation(position, rotation);
        }
        else
        {
            targetPosition = m_camera.ViewportToWorldPoint(new Vector3(screenHUDPosition.x, screenHUDPosition.y, m_relativeDistance));
            currentRotation = transform.rotation;
            targetRotation = Quaternion.LookRotation(targetPosition - m_camera.transform.position, Vector3.up);

            position = Vector3.SmoothDamp(currentHUDPosition, targetPosition, ref m_velocity, m_positionDamping * Time.deltaTime);
            rotation = Quaternion.Slerp(currentRotation, targetRotation, m_rotationLerp * Time.deltaTime);
            transform.SetPositionAndRotation(position, rotation);
        }
    }
}
