using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.HeroEditor.Common.CharacterScripts;
using HeroEditor.Common.Enums;

// 被击中动画
public class Hit : MonoBehaviour
{
    Character character;
    Animator animator;
    Rigidbody2D rig;


    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
        animator = character.Animator;
        rig = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + " <- " + collision.gameObject.name);
        hit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hitBack();
    }

    Vector2 originalPosition;
    void hit()
    {
        originalPosition = rig.position;
        Vector2 position = new Vector2(originalPosition.x + 0.05f, originalPosition.y);
        rig.MovePosition(position);
    }

    void hitBack()
    {
        rig.MovePosition(originalPosition);
    }


}
