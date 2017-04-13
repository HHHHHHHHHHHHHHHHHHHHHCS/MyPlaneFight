using UnityEngine;
using System.Collections;

public class BackgroundMove : MonoBehaviour
{
    Vector3 backVec3;

    void Awake()
    {
        backVec3 = new Vector3(0, GameSetting.BackgroundSetting.backY, 0);
    }

    void Update()
    {
        transform.Translate(Vector3.down * GameSetting.BackgroundSetting.bgMoveSpeed * Time.deltaTime);
        if (transform.position.y <= GameSetting.BackgroundSetting.levelY)
        {
            transform.position = backVec3;
        }

    }
}
