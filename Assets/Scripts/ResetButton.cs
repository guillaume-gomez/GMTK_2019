using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
  public void OnClick()
  {
    GameManager.instance.PlaySoundUI("ui_click");
    GameManager.instance.Restart();
  }
}
