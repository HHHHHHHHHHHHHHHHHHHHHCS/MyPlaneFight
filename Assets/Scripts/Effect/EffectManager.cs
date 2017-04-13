using UnityEngine;
using System.Collections;

public class EffectManager
{
    private static EffectManager _instance;

    public static EffectManager Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = new EffectManager();
                _instance.Init();
            }
            return _instance;
        }
    }

    private Transform effectParment;

    void Init()
    {
        effectParment = GameObject.Find(GameSetting.Enemy.effectParentPath).transform;
    }

    public GameObject CreateEffect(GameObject prefab)
    {
        GameObject newGo = Object.Instantiate(prefab);
        newGo.transform.SetParent(effectParment);
        return newGo;
    }
}
