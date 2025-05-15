using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MercenaryPortrait : MonoBehaviour
{
    public Image frame;
    public Image back;
    public Image portrait;
    public Mercenary mercenary;

    public void SetPortrait(Sprite sprite)
    {
        portrait.sprite = sprite;
    }
    public void SetMercenary(Mercenary mercenary)
    {
        this.mercenary = mercenary;
        portrait.sprite = mercenary.dataSO.mercenaryPortrait;
        UpdateFrameColor();
    }
        public void UpdateFrameColor()
    {
        if (mercenary != null && mercenary.isSelected)
        {
            frame.color = Color.green;
        }
        else
        {
            frame.color = Color.white;
        }
    }
}
