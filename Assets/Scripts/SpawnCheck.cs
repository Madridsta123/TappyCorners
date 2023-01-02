using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCheck : MonoBehaviour
{
    public void OnDestroy()
    {
        GameManager.Instance.collectibleSpawnCheck = true;
    }
}
