using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! 在有能力管理时，尽量不要多开场景，可以先分场景开发，最后再整合改为热更新
/// <summary>
/// 整个游戏的入口，也是永远不被销毁的对象
/// </summary>
public class GameLaunch : ISingleton<GameLaunch>
{
    public override void Awake() {
        base.Awake();

        //todo 初始化游戏框架
        this.gameObject.AddComponent<ResourceManager>();
        this.gameObject.AddComponent<EventManager>();
        this.gameObject.AddComponent<UIManager>();
        this.gameObject.AddComponent<AudioManager>();
        this.gameObject.AddComponent<InputListener>();
        this.gameObject.AddComponent<SaveLoadManager>();
        //end

        //todo  进入游戏的逻辑入口
        this.gameObject.AddComponent<GameApp>();
        //end
    }

    private void Start() {
        GameApp.Instance.EnterGame();
    }
}
