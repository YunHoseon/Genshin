using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.0f;
    [SerializeField]
    private float alphaSpeed = 1.0f;
    private float destroyTime = 1.0f;

    private Text txtDamage;
    private Color alpha;
    private Vector3 scaleChange;

    void Start()
    {
        txtDamage = GetComponent<Text>();
        alpha = txtDamage.color;
        scaleChange = new Vector3(2, 2, 2);
        Invoke("DestroyText", destroyTime);
    }
    
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime), 0);
        //alpha.a = Mathf.Lerp(alpha.a, 0.0f, alphaSpeed * Time.deltaTime);
        //txtDamage.color = alpha;
        txtDamage.transform.localScale = scaleChange;
    }

    void DestroyText()
    {
        Destroy(this.gameObject);
    }
}
