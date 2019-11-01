using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBloodBar : MonoBehaviour
{
    // 血条图片
    public Sprite bloodBarSprite;

    Image bloodImage;

    private void Start()
    {
        // 找到canvas
        GameObject canvas = GameObject.Find("HUD");
        // 创建image
        GameObject bloodGameObject = new GameObject();
        bloodImage = bloodGameObject.AddComponent<Image>();
        bloodImage.sprite = bloodBarSprite;
        RectTransform rectTransform = bloodGameObject.GetComponent<RectTransform>();
        // 设置宽、高
        rectTransform.sizeDelta = new Vector2(70, 10);
        // 中心设置为left-center
        // rectTransform.pivot = new Vector2(0, 0.5f);
        bloodImage.type = Image.Type.Filled;
        

        // 把image加入到canvas中
        bloodGameObject.transform.SetParent(canvas.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodImage != null)
        {
            // fillAmount逆时转圈隐藏
            bloodImage.fillAmount -= Time.deltaTime / 50.0f;
            Debug.Log(bloodImage.fillAmount);
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            bloodImage.gameObject.transform.position = new Vector3(position.x, position.y + 110, position.z);
        }
    }
}
