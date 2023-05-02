using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer SPR;
    Rigidbody2D RB;

    public float Speed = 5;
    // 몇 초동안 이속이 증가하며 색깔이 변함.
    public float SkillDuration; // 스킬 지속 시간
    public float maxCoolTime; // 스킬 쿨타임

    public bool CanSkill = true;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        SPR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //ctrl + s
        Move();
        Speed_UP();
        LimitPos();
    }
    void Move() {
        float x = Input.GetAxis("Horizontal") * Speed;
        Vector2 vec = new Vector2(x, RB.velocity.y);
        RB.velocity = vec;
    }
    void Speed_UP() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (CanSkill == true) {
                StartCoroutine(Speed_UPSkill());
            }
        }
    }
    IEnumerator Speed_UPSkill() {
        Speed = 10; // 스피드를 10으로 만든후
        Color blue = new Color(0,0,1,1); // blue 라는 변수는 파란색 값을 가지고 있는 변수다.
        SPR.color = blue;
        CanSkill = false;
        yield return new WaitForSeconds(SkillDuration); // 지속 시간 만큼 기다리기

        Color green = new Color(0,1,0,1); // 초록색 변수
        SPR.color = green;
        Speed = 5; // 스피드를 다시 5로
        StartCoroutine(SkillCoolTime()); // << 안됨
    }
    IEnumerator SkillCoolTime() {
        yield return new WaitForSeconds(maxCoolTime);
        CanSkill = true;
    }

    void LimitPos() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // 위치를 카메라 상에서 변환해주는 함수
        if(pos.x < 0f) pos.x = 0f;
        if(pos.x > 1f) pos.x = 1f;

        if(pos.y < 0f) pos.y = 0f;
        if(pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);       
    }
}
