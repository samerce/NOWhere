using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerper : MonoBehaviour {

    [SerializeField] private Material materialToLerp;
    // array of colors to lerp between
    [SerializeField] private Color[] colors;

    // time to lerp between colors
    [SerializeField] private float lerpTime;

    [SerializeField] private float timer;

    // time to wait before starting lerp
    [SerializeField] private float waitTime;
    [SerializeField] private int colorIndex;

    // a coroutine that lerps between all colors using lerpTime every frame

    private void Start()
    {
        StartCoroutine(LerpColor());
    }

    private IEnumerator LerpColor()
    {
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > lerpTime)
            {
                timer = 0;
                colorIndex++;
                if (colorIndex > colors.Length - 1)
                {
                    colorIndex = 0;
                }
            }

            materialToLerp.color = Color.Lerp(materialToLerp.color, colors[colorIndex], timer / lerpTime * Time.deltaTime);
            yield return null;
        }
    }
}
