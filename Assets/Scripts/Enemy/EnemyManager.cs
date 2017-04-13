using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private Dictionary<string, GameObject> enemysDic = new Dictionary<string, GameObject>();
    private List<SpawnEnemyInfo> spawnEnemyInfoList = new List<SpawnEnemyInfo>();

    void Awake()
    {
        Init();
        ChangeWave(1);
        StartCoroutine(SpawnEnemy());
    }

    void Init()
    {
        GameObject newGo;

        newGo = Resources.Load<GameObject>(GameSetting.Enemy.path_small);
        enemysDic.Add(GameSetting.Enemy.id_small, newGo);

        newGo = Resources.Load<GameObject>(GameSetting.Enemy.path_normal);
        enemysDic.Add(GameSetting.Enemy.id_normal, newGo);

        newGo = Resources.Load<GameObject>(GameSetting.Enemy.path_shootPanel);
        enemysDic.Add(GameSetting.Enemy.id_shootPanel, newGo);
    }


    public void ChangeWave(int wave)
    {
        spawnEnemyInfoList.Clear();
        string wavePathStr = wave.ToString().PadLeft(3, '0');
        wavePathStr = string.Format("{0}{1}", GameSetting.Enemy.wavePath, wavePathStr);
        string waveInfo = Resources.Load<TextAsset>(wavePathStr).text;
        string[] spawnEnemys = waveInfo.Split('\n');
        string[] detailInfos;

        foreach (string spawnEnemyInfo in spawnEnemys)
        {
            detailInfos = spawnEnemyInfo.Split(',');
            if (!string.IsNullOrEmpty(detailInfos[0].Trim()))
            {
                SpawnEnemyInfo newEnemyInfo = new SpawnEnemyInfo()
                {
                    spawnID = detailInfos[0],
                    spawnPos = new Vector3(float.Parse(detailInfos[1]), float.Parse(detailInfos[2]), 0),
                    nextTime = float.Parse(detailInfos[3])
                };
                spawnEnemyInfoList.Add(newEnemyInfo);
            }
        }
    }

    void CreateEnemy(SpawnEnemyInfo newInfo)
    {
        Transform newTS = Instantiate(enemysDic[newInfo.spawnID]).transform;
        newTS.SetParent(transform);
        newTS.position = newInfo.spawnPos;
    }

    IEnumerator SpawnEnemy()
    {
        if (spawnEnemyInfoList.Count > 0)
        {
            float waitTime = 0;
            while (spawnEnemyInfoList.Count > 0)
            {
                SpawnEnemyInfo newInfo = spawnEnemyInfoList[0];
                spawnEnemyInfoList.RemoveAt(0);
                CreateEnemy(newInfo);
                if (newInfo.nextTime > 0)
                {
                    waitTime = newInfo.nextTime;
                    break;
                }
            }
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(SpawnEnemy());
        }
    }
}
