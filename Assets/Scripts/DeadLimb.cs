using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class DeadLimb : MonoBehaviour
{
    [Range(0, 40)]
    public float detachForce;
    [Range(0, 40)]
    public float detachRotationForce;
    MeshRenderer mr;

    private IEnumerator FadeTo(float initValue, float duration, float timeUntilStart)
    {
        yield return new WaitForSeconds(timeUntilStart);
        mr = GetComponentInChildren<MeshRenderer>();
        Color newColor = new Color(1, 1, 1, 0);
        float alpha = mr.material.color.a;
        // change "alpha" of the mesh
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration)
        {
            newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(initValue, alpha, t));
            mr.material.color = newColor;
            if (t > 0.98f)
            {
                gameObject.SetActive(false);
            }

        }
        yield return null;
    }

    public void Fall(float fadeTime, float timeUntilStart)
    {
        StartCoroutine(FadeTo(1.0f, fadeTime, timeUntilStart));
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, detachForce, 0.0f));
        float randomTorque = Random.Range(-detachRotationForce, detachRotationForce);
        rb.AddTorque(new Vector3(0.0f, 0.0f, randomTorque));
    }
}
