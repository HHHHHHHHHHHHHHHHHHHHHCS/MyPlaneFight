using UnityEngine;
using System.Collections;

public class BulletManager
{
    private static BulletManager _instance;

    public static BulletManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new BulletManager();
                _instance.Init();
            }
            return _instance;
        }
    }

    private Transform bulletParent;

    void Init()
    {
        bulletParent = GameObject.Find(GameSetting.BulletManager.bulletParentPath).transform;
    }

    public GameObject CreateBullet(GameObject prefab)
    {
        GameObject newGo = Object.Instantiate(prefab);
        newGo.transform.SetParent(bulletParent);
        return newGo;
    }
}
