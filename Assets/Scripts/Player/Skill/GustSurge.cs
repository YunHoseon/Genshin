using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GustSurge : MonoBehaviour
{
    private float moveSpeed = 2.0f;

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
