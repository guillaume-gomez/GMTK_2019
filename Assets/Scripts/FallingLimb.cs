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
  public GameObject leftArmPrefab;
  public GameObject deadLimbPrefab;

  bool isActive = true;

  public void LoseleftArm() {
    if(isActive && leftArmPrefab.active)
    {
      leftArmPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbPrefab, leftArmPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart);
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
