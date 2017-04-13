using UnityEngine;
using System.Collections;

public class Enemy_ShootPanel : EnemyBase
{
    public static GameObject bullet_ShootPanel;
    private float shootTime;
    private float shootTimer;

    void Awake()
    {
        if (bullet_ShootPanel == null)
        {
            bullet_ShootPanel = Resources.Load<GameObject>(GameSetting.Enemy.bullet_ShootPanel);
        }

        GetBlowPrefab();
        GetShootPos();

        hp = GameSetting.Enemy.shootPanel_HP;
        speed = GameSetting.Enemy.shootPanel_Speed;
        attack = GameSetting.Enemy.shootPanel_Attack;
        shootTime  = GameSetting.Enemy.shootPanel_Time;
        shootTimer = GameSetting.Enemy.startShoot_Time;
        bulletSpeed = GameSetting.Enemy.shootPanelBullet_Speed;
    }

    void Update()
    {
        CheckDestorySelf();
        if (shootTimer <= 0)
        {
            shootTimer = shootTime;
            Attack();
        }
        else
        {
            shootTimer -= Time.deltaTime;
        }
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    public override void Attack()
    {
        GameObject newGo = BulletManager.Instance.CreateBullet(bullet_ShootPanel);
        newGo.transform.position = shootPos.position;
        BulletInfo newBulletInfo = newGo.AddComponent<BulletInfo>();
        newBulletInfo.Init(playerType, attack, bulletSpeed, bulletDestorySelf);
        Vector3 moveDir = (Player.Instance.transform.position - transform.position).normalized;
        newBulletInfo.ChangeMoveDir(moveDir);
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
