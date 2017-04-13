using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }
    private bool isInvincible = false;

    private GameObject playerBulletPrefab;
    private List<Transform> attackPosList = new List<Transform>();
    private SpriteRenderer spriteRender;
    private GameSetting.PlayerState playerState;
    private int playerMaxHP = GameSetting.PlayerSetting.playerMaxHP;
    private int playerNowHP = GameSetting.PlayerSetting.playerMaxHP;
    private float playerSpeed = GameSetting.PlayerSetting.playerSpeed;
    private float attackCD = GameSetting.PlayerSetting.attckCD;
    private int attackDamage = GameSetting.PlayerSetting.attackDamage;
    private float bulletSpeed = GameSetting.PlayerSetting.bulletSpeed;

    private bool canShoot = false;
    private float attackTimer = 0;

    public GameSetting.PlayerState PlayerState
    {
        get
        {
            return playerState;
        }
    }

    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }

        set
        {
            isInvincible = value;
        }
    }

    void Awake()
    {
        _instance = this;
        spriteRender = GetComponent<SpriteRenderer>();
        playerBulletPrefab = Resources.Load<GameObject>(GameSetting.BulletManager.playerBulletPrefabPath);
        foreach (Transform newTS in transform)
        {
            attackPosList.Add(newTS);
        }
    }

    void Update()
    {
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        if (attackTimer <= 0 && canShoot)
        {
            attackTimer = attackCD;
            PlayerAttack();
        }
        Vector2 moveVec2;
        if (Input.touchCount > 0)
        {
            moveVec2 = Input.GetTouch(0).deltaPosition.normalized;
        }
        else
        {
            moveVec2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
        if (moveVec2 != Vector2.zero)
        {
            Vector3 newVec3 = new Vector3(playerSpeed * moveVec2.x, playerSpeed * moveVec2.y, 0) * Time.deltaTime;
            newVec3 += transform.position;

            newVec3.x = Mathf.Clamp(newVec3.x, GameSetting.PlayerSetting.minX, GameSetting.PlayerSetting.maxX);
            newVec3.y = Mathf.Clamp(newVec3.y, GameSetting.PlayerSetting.minY, GameSetting.PlayerSetting.maxY);

            transform.position = newVec3;
        }
    }

    void PlayerAttack()
    {
        GameObject newGo = BulletManager.Instance.CreateBullet(playerBulletPrefab);
        BulletInfo newBulletInfo = newGo.AddComponent<BulletInfo>();
        newBulletInfo.transform.position = attackPosList[0].position;
        newBulletInfo.Init(GameSetting.PlayerType.player, attackDamage, bulletSpeed
            , GameSetting.PlayerSetting.bulletDestorySelf);
    }

    public void TakeDamage(int damage)
    {
        int newHP = playerNowHP - damage;
        playerNowHP = newHP < 0 ? 0 : newHP;

        if (playerNowHP <= 0)
        {

        }
        else
        {
            StartCoroutine(PlayerInvincible(GameSetting.PlayerSetting.playerInvincibleTime));
        }

    }

    IEnumerator PlayerInvincible(float time)
    {
        IsInvincible = true;
        float alpha = spriteRender.color.a;
        Color changeColor = spriteRender.color;
        changeColor.a = GameSetting.PlayerSetting.playerInvincibleAlpha;
        spriteRender.color = changeColor;
        yield return new WaitForSeconds(time);
        IsInvincible = false;
        changeColor.a = alpha;
        spriteRender.color = changeColor;
    }
}
