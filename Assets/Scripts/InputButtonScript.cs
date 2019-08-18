using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButtonScript : MonoBehaviour
{
  public Sprite originalImage;
  public Sprite disabledImage;
  private bool disabled;

  void Start()
  {
    disabled = false;
  }

  public void DisableButton()
  {
    if(disabled)
    {
      return;
    }

    disabled = true;
    Button button = GetComponent<Button>();
    button.GetComponent<Image>().sprite = disabledImage;
    //make sure to set as not interactable
    button.interactable = false;
  }

  public void EnableButton () {
    if(!disabled)
    {
      return;
    }

    disabled = false;
    Button button = GetComponent<Button>();
    button.GetComponent<Image>().sprite = originalImage;
  }
}
