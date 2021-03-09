using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    //public GameObject target;

    private float offsetX = 0;
    private float offsetY = 0.6f;
    private float offsetZ = -3;

    public Transform Holder;
    public Vector3 cameraPos = new Vector3(0, 0, 0);
    public float currDistance = 5.0f;
    public float xRotate = 250.0f;
    public float yRotate = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float prevDistance;
    private float x = 0.0f;
    private float y = 0.0f;

    [SerializeField]
    private float followSpeed = 3;

    Vector3 cameraPosition;
    private bool isCursorVisible = false;

    //For camera colliding
    RaycastHit hit;
    public LayerMask collidingLayers = ~0; //Target marker can only collide with scene layer
    private float distanceHit;

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if(!GameManager.Instance.Player.isInMenu)
        {
            if (GameManager.Instance.Player.isInInventory)
                return;
            if (GameManager.Instance.Player.isInCharacter)
                return;

            //cameraPosition.x = Holder.position.x + offsetX;
            //cameraPosition.y = Holder.position.y + offsetY;
            //cameraPosition.z = Holder.position.z + offsetZ;

            //transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);

            if (currDistance < 2)
                currDistance = 2;

            var targetPos = Holder.position + new Vector3(0, (distanceHit - 2) / 3f + cameraPos[1], 0);

            currDistance -= Input.GetAxis("Mouse ScrollWheel") * 2;
            if (currDistance > 6.0f)
                currDistance = 6.0f;
            if (currDistance < 3.5f)
                currDistance = 3.5f;

            if (Holder)
            {
                var pos = Input.mousePosition;
                float dpiScale = 1;

                if (Screen.dpi < 1)
                    dpiScale = 1;
                if (Screen.dpi < 200)
                    dpiScale = 1;
                else
                    dpiScale = Screen.dpi / 200f;

                if (pos.x < 380 * dpiScale && Screen.height - pos.y < 250 * dpiScale)
                    return;

                x += (float)(Input.GetAxis("Mouse X") * xRotate * 0.02);
                y -= (float)(Input.GetAxis("Mouse Y") * yRotate * 0.02);
                y = ClampAngle(y, yMinLimit, yMaxLimit);

                var rotation = Quaternion.Euler(y, x, 0);
                var position = rotation * new Vector3(0, 0, -currDistance) + targetPos;
                transform.position = Vector3.Lerp(transform.position, position, followSpeed * Time.deltaTime);

                //If camera collide with collidingLayers move it to this point.
                if (Physics.Raycast(targetPos, position - targetPos, out hit, (position - targetPos).magnitude, collidingLayers))
                {
                    transform.position = hit.point;
                    //Min(4) distance from ground for camera target point
                    distanceHit = Mathf.Clamp(Vector3.Distance(targetPos, hit.point), 4, 600);
                }
                else
                {
                    transform.position = position;
                    distanceHit = currDistance;
                }
                transform.rotation = rotation;
            }

            if (prevDistance != currDistance)
            {
                prevDistance = currDistance;
                var rot = Quaternion.Euler(y, x, 0);
                // (currDistance - 2) / 3.5f - constant for far camera position
                var po = rot * new Vector3(0, 0, -currDistance) + targetPos;
                transform.rotation = rot;
                transform.position = po;
            }
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    private void FixedUpdate()
    {
        if(GameManager.Instance.Player.isInMenu || GameManager.Instance.Player.isInInventory ||
           GameManager.Instance.Player.isInCharacter)
        {
            Cursor.visible = true;
            Screen.lockCursor = false;
        } 
        else
        {
            Cursor.visible = false;
            Screen.lockCursor = true;
        }
    }
}
