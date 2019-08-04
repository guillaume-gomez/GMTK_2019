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
  public GameObject capPrefab; 

  public GameObject deadLimbLeftArmPrefab;
  public GameObject deadLimbRightArmPrefab;
  public GameObject deadLimbFeetPrefab;
  public GameObject deadLimbHeadArmPrefab;
  public GameObject deadCapPrefab;

  bool isActive = true;
    public Transform headCenter;

    public void LoseLeftArm() { LoseLeftArm(false);  }

  public void LoseLeftArm(bool shakeCam)
  {
    if(isActive && leftArmPrefab.activeSelf)
    {
      leftArmPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbLeftArmPrefab, leftArmPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart, shakeCam);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

    public void LoseRightArm()    { LoseRightArm(false); }

  public void LoseRightArm(bool shakeCam)
    {
    if(isActive && rightArmPrefab.activeSelf)
    {
      rightArmPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbRightArmPrefab, rightArmPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart, shakeCam);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

    public void LoseHead()
    {
        LoseHead(false);
    }

  public void LoseHead(bool shakeCam)
    {
    if(isActive && headPrefab.activeSelf)
    {
      headPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbHeadArmPrefab, headPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>().Fall(fadeTime, timeUntilStart, shakeCam);
      ShouldToBeDisable();
      nbLimbs--;
    }
  }

    public void LoseFeet()    {        LoseFeet(false);     }

    public void LoseFeet(bool shakeCam)
    {
    if(isActive && feetPrefab.activeSelf)
    {
      feetPrefab.SetActive(false);
      GameObject instance = Instantiate (deadLimbFeetPrefab, feetPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>()?.Fall(fadeTime, timeUntilStart , shakeCam);
      ShouldToBeDisable();

      capPrefab.SetActive(false);
      instance = Instantiate(deadCapPrefab, capPrefab.transform.position, Quaternion.identity) as GameObject;
      instance.GetComponent<DeadLimb>()?.Fall(fadeTime, timeUntilStart, false);

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
