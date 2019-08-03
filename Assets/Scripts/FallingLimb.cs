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

  private float nbLimbs = 3;
  public GameObject leftArmPrefab;
  public GameObject rightArmPrefab;
  public GameObject headPrefab;
  public GameObject feetPrefab;
  public GameObject deadLimbPrefab;

  bool isActive = true;

  public void LoseLeftArm()
  {
    if(isActive && leftArmPrefab.active)
    {
      leftArmPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbPrefab, leftArmPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

  public void LoseRightArm()
  {
    if(isActive && rightArmPrefab.active)
    {
      rightArmPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbPrefab, rightArmPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

  public void LoseHead()
  {
    if(isActive && headPrefab.active)
    {
      headPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbPrefab, headPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

  public void LoseFeet()
  {
    if(isActive && feetPrefab.active)
    {
      feetPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbPrefab, feetPrefab.transform.position, Quaternion.identity) as GameObject;
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
