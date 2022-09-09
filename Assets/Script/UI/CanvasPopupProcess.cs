using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopupProcess : MonoBehaviour
{
    public Sprite[] ListTexture;
    public Image ImageGift;
    public void ChangeSkin(int IndexTexture)
    {
        ImageGift.sprite = ListTexture[IndexTexture];
    }
   
}
