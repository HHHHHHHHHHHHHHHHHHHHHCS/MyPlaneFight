using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    protected GameSetting.PlayerType playerType = GameSetting.PlayerType.enemy;
    protected float bulletDestorySelf = GameSetting.Enemy.bulletDestorySelf;
    protected static GameObject blow_NormalPrefab;
    protected Transform shootPos;
    protected int hp;
    protected float speed;
    protected int attack;
    protected float bulletSpeed;

    public virtual void GetBlowPrefab()
    {
        if (blow_NormalPrefab == null)
        {
            blow_NormalPrefab = Resources.Load<GameObject>(GameSetting.Enemy.blow_NormalPath);
        }
    }

    public virtual void GetShootPos()
    {
        shootPos = transform.Find(GameSetting.Enemy.shootPosPath).transform;
    }

    public virtual void Attack()
    {

    }

    public virtual void TakeDamge(int damage)
    {
        int newHP = hp - damage;
        hp = newHP < 0 ? 0 : newHP;
        if (hp <= 0)
        {
            Dead();
        }

    }

    public virtual void CheckDestorySelf()
    {
        if (transform.position.y <= GameSetting.Enemy.levelY)
        {
            Destroy(gameObject);
        }
    }

    public virtual void Dead()
    {
        Destroy(gameObject);
    }
}
