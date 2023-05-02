using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer SPR;
    Rigidbody2D RB;

    public float Speed = 5;
    // �� �ʵ��� �̼��� �����ϸ� ������ ����.
    public float SkillDuration; // ��ų ���� �ð�
    public float maxCoolTime; // ��ų ��Ÿ��

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
        Speed = 10; // ���ǵ带 10���� ������
        Color blue = new Color(0,0,1,1); // blue ��� ������ �Ķ��� ���� ������ �ִ� ������.
        SPR.color = blue;
        CanSkill = false;
        yield return new WaitForSeconds(SkillDuration); // ���� �ð� ��ŭ ��ٸ���

        Color green = new Color(0,1,0,1); // �ʷϻ� ����
        SPR.color = green;
        Speed = 5; // ���ǵ带 �ٽ� 5��
        StartCoroutine(SkillCoolTime()); // << �ȵ�
    }
    IEnumerator SkillCoolTime() {
        yield return new WaitForSeconds(maxCoolTime);
        CanSkill = true;
    }

    void LimitPos() {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position); // ��ġ�� ī�޶� �󿡼� ��ȯ���ִ� �Լ�
        if(pos.x < 0f) pos.x = 0f;
        if(pos.x > 1f) pos.x = 1f;

        if(pos.y < 0f) pos.y = 0f;
        if(pos.y > 1f) pos.y = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(pos);       
    }
}
