using UnityEngine;
using System.Collections;

public class GameSetting
{

    public struct Tags
    {
        public const string player = "Player";
        public const string enemy = "Enemy";
    }

    public enum PlayerType
    {
        player,
        enemy
    }

    public enum PlayerState
    {
        normal,
        invincible
    }

    public struct BackgroundSetting
    {
        public const float bgMoveSpeed = 2;
        public const float levelY = -10.35f;
        public const float backY = 10.35f;
    }

    public struct PlayerSetting
    {
        public const float maxY = 4.4f;
        public const float minY = -4.4f;
        public const float maxX = 2f;
        public const float minX = -2f;

        public const int playerMaxHP = 1000;//10
        public const float playerInvincibleAlpha = 0.5f;
        public const float playerInvincibleTime = 1f;
        public const float playerSpeed = 5f;
        public const float attckCD = 0.25f;
        public const int attackDamage = 10;
        public const float bulletSpeed = 10f;
        public const float bulletDestorySelf = 5f;

    }

    public struct BulletManager
    {
        public const string bulletParentPath = "BulletParent";
        public const string playerBulletPrefabPath = "Prefabs/PlayerBullet";
        public const float levelMaxY = 5.5f;
        public const float levelMinY = -5.5f;
    }

    public struct Enemy
    {
        public const string wavePath = "Text/wave";
        public const string effectParentPath = "EffectParent";
        public const float bulletDestorySelf = 5f;
        public const string shootPosPath = "ShootPos";
        public const float levelY = -6f;

        public const string id_small = "smallPanel";
        public const string path_small = "Prefabs/Enemy_Small";
        public const int small_HP = 5;
        public const float small_Speed = 7.5f;
        public const int small_Attack = 3;
        public const string blow_SmallPath = "Prefabs/Blow_Normal";

        public const string id_normal = "normalPanel";
        public const string path_normal = "Prefabs/Enemy_Normal";
        public const int normal_HP = 10;
        public const float normal_Speed = 5f;
        public const int normal_Attack = 5;
        public const string blow_NormalPath = "Prefabs/Blow_Normal";

        public const string id_shootPanel = "shootPanel";
        public const string path_shootPanel = "Prefabs/Enemy_ShootPanel";
        public const int shootPanel_HP = 30;
        public const float shootPanel_Speed = 5f;
        public const int shootPanel_Attack = 3;
        public const float shootPanel_Time = 3f;
        public const float startShoot_Time = 0.75f;
        public const float shootPanelBullet_Speed = 7.5f;
        public const string bullet_ShootPanel = "Prefabs/Bullet_ShootPanel";



    }

}
