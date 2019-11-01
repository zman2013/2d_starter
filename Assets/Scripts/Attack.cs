using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;

public class Attack : MonoBehaviour
{

    Rigidbody2D rig;
    Character character;
    Animator animator;

    GameObject target;

	string status;

    Vector3 originalPosition;
    Vector3 targetPosition;

    float attackMoveSpeed = 30f;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
        animator = character.Animator;
    }

    void Update()
    {
        if(target == null)
		{
			if (Input.GetMouseButton(0))
			{
                Debug.Log("mouseClick");
				Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);

				if (hit.collider != null)
				{
                    
					target = hit.collider.gameObject;
                    Debug.Log("find target " + target.name);
                    // 计算终点
                    originalPosition = rig.position;
					targetPosition.Set(target.transform.position.x-0.5f, target.transform.position.y-0.01f, originalPosition.z);
                    // 设置为Kinematic
                    rig.bodyType = RigidbodyType2D.Kinematic;
                    // 设置状态为移动中
                    status = "movingToTarget";
				}
			}
		}
		else
		{
            if( status == "movingToTarget")
            {
                // 未到终点继续移动
                if( Vector3.Distance(rig.position, targetPosition) > Mathf.Epsilon)
                {
                    float distance = attackMoveSpeed * Time.deltaTime;
                    Vector3 newPosition = Vector3.MoveTowards(rig.position, targetPosition, distance);
                    rig.MovePosition(newPosition);
                }
                else if(status != "attacking")
                {

                    Debug.Log("start attack");

                    // 到达终点发起攻击
                    status = "attacking";

                    StartCoroutine("attacking");

                }
            }else if( status == "movingToOrigin")
            {
                if( Vector3.Distance(rig.position, originalPosition) > Mathf.Epsilon)
                {
                    float distance = attackMoveSpeed * Time.deltaTime;
                    Vector3 newPosition = Vector3.MoveTowards(rig.position, originalPosition, distance);
                    rig.MovePosition(newPosition);
                }
                else
                {
                    // 回到起点了
                    target = null;
                    rig.bodyType = RigidbodyType2D.Dynamic;

                    status = "idle";
                }

            }
        }
    }

    IEnumerator attacking()
    {
        character.WeaponType = WeaponType.Melee1H;
        animator.SetTrigger("Slash"); // Slash / Jab
        Debug.Log("Slash");

        yield return new WaitForSeconds(0.5f);

        // 向起点移动
        status = "movingToOrigin";
    }


}
