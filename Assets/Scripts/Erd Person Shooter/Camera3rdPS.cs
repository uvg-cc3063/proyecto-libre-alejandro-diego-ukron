using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Camera3rdPS : MonoBehaviour
{
    private CinemachineComposer composer;

    [SerializeField]
    private float sensitivity = 1f;

    private void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        //--
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;
        //--
        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -2.5f, 2.5f);
        //--
        composer.m_TrackedObjectOffset.x += horizontal;
        composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, -5, 5);
        //--
        //transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
}
