using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    public GameObject target;

    private float offsetX = 0;
    private float offsetY = 0.6f;
    private float offsetZ = -3;

    [SerializeField]
    private float followSpeed = 3;

    Vector3 cameraPosition;

    void LateUpdate()
    {
        cameraPosition.x = target.transform.position.x + offsetX;
        cameraPosition.y = target.transform.position.y + offsetY;
        cameraPosition.z = target.transform.position.z + offsetZ;

        transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}
