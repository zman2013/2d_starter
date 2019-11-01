using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBar : MonoBehaviour
{
    // 血条Pref
    public GameObject bloodBarPrefab;

    BloodBarController bloodBarController;
    GameObject bloodGameObject;

    private void Start()
    {
        // 找到canvas
        GameObject canvas = GameObject.Find("HUD");
        // 创建血条对象
        bloodGameObject = Instantiate(bloodBarPrefab, canvas.transform);
        bloodBarController = bloodGameObject.GetComponent<BloodBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bloodGameObject != null)
        {
            // fillAmount逆时转圈隐藏
            bloodBarController.setHealth(0.70f);
            // 设置血条位置
            Vector3 position = Camera.main.WorldToScreenPoint(transform.position);
            bloodGameObject.gameObject.transform.position = new Vector3(position.x-35, position.y + 110, position.z);
        }
    }
}
