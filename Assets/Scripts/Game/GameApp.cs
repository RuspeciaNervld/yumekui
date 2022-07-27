using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : ISingleton<GameApp>
{
    public void EnterGame() {
        this.EnterFightingScene();
    }

    public void EnterFightingScene() {
        // 释放我们的UI
        UIManager.Instance.ShowUIView("TestUI");//! 根据预制体名称
        // end



        // 获取场景,并初始化,包括脚本挂载,物体创建和获取
        GameObject mapPrefab = ResourceManager.Instance.GetAssetCache<GameObject>("Maps/Test.prefab");
        GameObject map = GameObject.Instantiate(mapPrefab);
        map.AddComponent<TestMgr>().InitGame();
        // end

    }

    public void EnterMainMenu() {

    }
}
