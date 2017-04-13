using UnityEngine;
using System.Collections;

public class BulletInfo : MonoBehaviour
{
    private GameSetting.PlayerType playerType;
    private int damage;
    private float speed;
    private float destoryTime;
    private Vector3 moveDir;

    public GameSetting.PlayerType PlayerType
    {
        get
        {
            return playerType;
        }
    }

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    public float DestoryTime
    {
        get
        {
            return destoryTime;
        }
    }

    public virtual void Init(GameSetting.PlayerType _playerType, int _damage, float _speed
        , float _destoryTime)
    {
        playerType = _playerType;
        damage = _damage;
        speed = _speed;
        destoryTime = _destoryTime;

        if (_playerType == GameSetting.PlayerType.player)
        {
            moveDir = Vector3.up;
        }
        else if (_playerType == GameSetting.PlayerType.enemy)
        {
            moveDir = Vector3.down;
        }

        StartCoroutine(DestorySelf());
    }


    void Update()
    {
        if(transform.position.y>=GameSetting.BulletManager.levelMaxY
            || transform.position.y <= GameSetting.BulletManager.levelMinY)
        {
            Destroy(gameObject);
        }

            transform.Translate(moveDir * Speed * Time.deltaTime);
    }

    public void ChangeMoveDir(Vector3 _moveDir)
    {
        moveDir = _moveDir;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerType == GameSetting.PlayerType.player)
        {
            if (collision.CompareTag(GameSetting.Tags.enemy))
            {
                Destroy(gameObject);
                collision.GetComponent<EnemyBase>().TakeDamge(damage);
            }
        }

        else if (playerType == GameSetting.PlayerType.enemy)
        {
            if (collision.CompareTag(GameSetting.Tags.player))
            {
                if (!Player.Instance.IsInvincible)
                {
                    Destroy(gameObject);
                    Player.Instance.TakeDamage(damage);
                }
            }
        }

    }

    IEnumerator DestorySelf()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gameObject);
    }
}
