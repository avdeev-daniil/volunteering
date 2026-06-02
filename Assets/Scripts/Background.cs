using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Image bg;

    public void changeBackground(Image newSprite)
    {
        bg.Image = newSprite;
    }
}
