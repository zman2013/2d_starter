using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 通过mask实现血条展示
// Mask与子Image交集才会显示Image的内容
// https://learn.unity.com/tutorial/visual-styling-ui-head-up-display?language=en&projectId=5c6166dbedbc2a0021b1bc7c#5d6559afedbc2a0020986e55
public class BloodBarController : MonoBehaviour
{

    Image image;
    float originalSize;


    public void Awake()
    {
        image = transform.Find("BloodImage").GetComponent<Image>();
        originalSize = image.rectTransform.rect.width;
    }

    public void setHealth(float percent)
    {
        // 设置血条长度
        image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * percent);
    }

}
