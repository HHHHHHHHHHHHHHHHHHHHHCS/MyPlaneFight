﻿using UnityEngine;
using System.Collections;

public class Enemy_Normal : EnemyBase
{

    void Awake()
    {
        GetBlowPrefab();

        hp = GameSetting.Enemy.normal_HP;
        speed = GameSetting.Enemy.normal_Speed;
        attack = GameSetting.Enemy.normal_Attack;
    }

    void Update()
    {
        CheckDestorySelf();
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(GameSetting.Tags.player))
        {
            if (!Player.Instance.IsInvincible)
            {
                Player.Instance.TakeDamage(attack);
                Transform newEffectTS = EffectManager.Instance.CreateEffect(blow_NormalPrefab).transform;
                newEffectTS.position = transform.position;
                Destroy(gameObject);
            }
        }
    }


    public override void TakeDamge(int damage)
    {
        base.TakeDamge(damage);
    }

    public override void Dead()
    {
        Transform newEffectTS = EffectManager.Instance.CreateEffect(blow_NormalPrefab).transform;
        newEffectTS.position = transform.position;
        Destroy(gameObject);
    }
}
