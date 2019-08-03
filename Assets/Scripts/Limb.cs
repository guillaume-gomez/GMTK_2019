using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]

public class Limb : MonoBehaviour
{

  public IEnumerator FadeTo(float initValue, float duration, float timeUntilStart) {
    yield return new WaitForSeconds(timeUntilStart);
    MeshRenderer mr = GetComponent<MeshRenderer>();
    Color newColor = new Color(1, 1, 1, 0);
    float alpha = mr.material.color.a;
    // change "alpha" of the mesh
    for(float t = 0.0f; t < 1.0f; t += Time.deltaTime / duration) {
      newColor = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(initValue, alpha, t));
      mr.material.color = newColor;
      if( t > 0.98f) {
        gameObject.SetActive(false);
      }

    }
    yield return null;
  }
}
