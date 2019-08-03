using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingLimb : MonoBehaviour
{
  [Range(0,10)]
  public float fadeTime = 1.0f;
  public float timeUntilStart = 2.0f;
  // use a maj because there are "constants"
  private Color White = new Color(1.0f, 1.0f, 1.0f, 1.0f);
  private Color Black = new Color(0.0f, 0.0f, 0.0f, 0.0f);

  private float nbLimbs = 1;
  public GameObject leftArm;

  bool isActive = true;

  public void LoseleftArm() {
    Debug.Log("called");
    if(isActive)
    {
      Limb limbScript = leftArm.GetComponent<Limb>();
      limbScript.StartCoroutine(limbScript.FadeTo(1.0f, fadeTime, timeUntilStart));
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

  private void ShouldToBeDisable()
  {
    if(nbLimbs <= 0)
    {
      isActive = false;
    }
  }
}
