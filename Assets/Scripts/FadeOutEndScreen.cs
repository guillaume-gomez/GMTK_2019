using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]


public class FadeOutEndScreen : MonoBehaviour
{
  public AnimationCurve alphaCurve;
  public float speed;
  private float elapsedTime = 0.0f;
  private Image img;

  void Start()
  {
    img = GetComponent<Image>();
  }

  // Update is called once per frame
  void Update()
  {
    elapsedTime += Time.deltaTime * speed;
    float newAlpha = alphaCurve.Evaluate(elapsedTime);

    img.color = new Color(img.color.r, img.color.g, img.color.b, 1 - newAlpha);
  }

}
