using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryPortrait : MonoBehaviour
{
    public Image frame;
    public Image portrait;
    public Image back;

    public void SetPortrait(Sprite sprite)
    {
        portrait.sprite = sprite;
    }

}
