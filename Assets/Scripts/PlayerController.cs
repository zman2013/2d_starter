using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;

public class PlayerController : MonoBehaviour
{
    public int speed = 10;

    Rigidbody2D rig;
    Collider2D collider;

    Character character;
    Animator animator;

    // 通过x的scale就行转向
    Vector3 playerScale;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        character = GetComponent<Character>();
        animator = character.Animator;
        playerScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        //
        // moveToMouseClickPosition();

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if(System.Math.Abs(horizontal) < Mathf.Epsilon && System.Math.Abs(vertical) < Mathf.Epsilon)
        {
            animator.SetTrigger("Stand");
            return;
        }

        Vector2 position = rig.position;
        position.x += horizontal * speed * Time.deltaTime;
        position.y += vertical * speed * Time.deltaTime;

        animator.SetTrigger("Walk");

        // 判断是否需要转向
        if (horizontal * transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontal)*playerScale.x, playerScale.y, playerScale.z);
        }

        rig.MovePosition(position);

        
    }

    Vector3 targetPosition;
    
    // 鼠标点击，移动到鼠标点击位置
    void moveToMouseClickPosition()
    {
        Vector3 currentPosition = rig.position;

        if (Input.GetButton("Fire1"))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.Set(targetPosition.x, targetPosition.y, currentPosition.z);
            Debug.Log(targetPosition);
            animator.SetBool("Jump", true);
        }

        if(targetPosition.magnitude < Mathf.Epsilon)
        {
            return;
        }

        float maxMoveDistance = speed * Time.deltaTime;

        if( Vector3.Distance(currentPosition, targetPosition) > 0.1f)
        {
            Vector3 position = Vector3.MoveTowards(currentPosition, targetPosition, maxMoveDistance);
            rig.MovePosition(position);

            // 移动不发生碰撞
            rig.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            animator.SetBool("Jump", false);
            character.WeaponType = WeaponType.Melee1H;
            animator.SetTrigger("Slash"); // Slash / Jab
            Debug.Log("Slash");
            targetPosition = Vector3.zero;

            // 移动发生碰撞
            rig.bodyType = RigidbodyType2D.Dynamic;
        }
       

    }
}
