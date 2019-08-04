using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
  public string[] soundsStr;

  void OnCollisionEnter (Collision col)
  {
      if(col.gameObject.tag == "Player")
      {
          GameManager.instance.PlaySound(soundsStr);
      }
  }
}
