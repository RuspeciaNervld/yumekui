using UnityEngine;

public class Player : ICreature {
    private const string PLAYER_DATA_KEY = "PlayerData";
    private const string PLAYER_DATA_FILE_NAME = "PlayerData.ruspecia";
    [Header("=== Player Data ===")]
    public string playerName;
    public int playerCoin;
    public int playerPoint;
    public float playerSpeed;
    public float nearMult;
    public float farMult;
    public float power;


    private Animator anim;
    private SpriteRenderer sr;
    private PlayerController moveController;


    #region 数据存取
    //! 可序列化的内部类，用于存储数据
    [System.Serializable]
    class PlayerData {
        public string playerName;
        //public int playerLevel;
        public int playerCoin;
        public int playerPoint;
        public Vector2 playerPosition;
        public float playerHp;
        public float playerAccept;
        public float playerAttack;
        public float playerSpeed;
        public float nearMult;
        public float farMult;
        public float power;
    }

    PlayerData GetPlayerData() {
        var playerData = new PlayerData();

        playerData.playerName = playerName;
        //playerData.playerLevel = playerLevel;
        playerData.playerCoin = playerCoin;
        playerData.playerPoint = playerPoint;
        playerData.playerPosition = transform.position;
        playerData.playerHp = hp;
        playerData.playerAccept = accept;
        playerData.playerAttack = attack;
        playerData.playerSpeed = playerSpeed;
        playerData.nearMult = nearMult;
        playerData.farMult = farMult;
        playerData.power = power;

        return playerData;
    }

    void SetPlayerData(PlayerData loadData) {
        playerName = loadData.playerName;
        //playerLevel = loadData.playerLevel;
        playerCoin = loadData.playerCoin;
        playerPoint = loadData.playerPoint;
        hp = loadData.playerHp;
        accept = loadData.playerAccept;
        attack = loadData.playerAttack;
        transform.position = loadData.playerPosition;
        playerSpeed = loadData.playerSpeed;
        nearMult = loadData.nearMult;
        farMult = loadData.farMult;
        power = loadData.power;
    }
    public void SaveByPlayerPrefs() {
        SaveLoadManager.Instance.SaveByPlayerPrefs(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromPlayerPrefs() {
        var json = SaveLoadManager.Instance.LoadFromPlayerPrefs(PLAYER_DATA_KEY);
        var loadData = JsonUtility.FromJson<PlayerData>(json);

        SetPlayerData(loadData);
    }

    //#if UNITY_EDITOR
    //    [UnityEditor.MenuItem("Developer/Delete PlayerData Prefs")]
    //    public static void DeletePlayerPrefs() {
    //        //PlayerPrefs.DeleteAll();
    //        PlayerPrefs.DeleteKey(PLAYER_DATA_FILE_NAME);
    //    }
    //#endif

    public void SaveByJson() {
        SaveLoadManager.Instance.SaveByJson(PLAYER_DATA_KEY, GetPlayerData());
    }

    public void LoadFromJson() {
        var loadData = SaveLoadManager.Instance.LoadFromJson<PlayerData>(PLAYER_DATA_FILE_NAME);
        SetPlayerData(loadData);
    }

    //#if UNITY_EDITOR
    //    [UnityEditor.MenuItem("Developer/Delete PlayerData File")]
    //    public static void DeletePlayerDataFile() {
    //        SaveLoadManager.Instance.DeleteSaveFile(PLAYER_DATA_FILE_NAME);
    //    }
    //#endif

    #endregion

    private void Awake() {
        anim = GetComponent<Animator>();
        moveController = GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    private void Update() {
        if(canBeHurt && beHurtController.isRecovering()) {
            anim.enabled = false;
            moveController.enabled = false;
        } else {
            anim.enabled = true;
            moveController.enabled = true;
        }
    }

    //private void onSkillHurt() {
    //    //todo 找到受到伤害的生物，计算出自己的攻击，传给对方的受伤函数，最终由对方计算实际伤害
    //}

    //private void onSkillEnd() {
    //    //todo 结束动画,恢复状态，也可以调用敌人的受伤结束函数，产生击退或者处刑效果等
    //}



    //public void onSkillAttack() {
    //    Debug.Log("技能发动");
    //    skillAttack = 2;
    //    skillHurtTime = 1.0f;
    //    skillEndTime = 1.4f;
    //    skillAttackRadius = 1.0f;

    //    if (this.attackController.doAttack(skillHurtTime, skillEndTime, onSkillHurt, onSkillEnd)) {
    //        //todo 播放攻击动画
    //    }
    //}

    public void NormalAttack() {
        Debug.Log("平A");
        if (this.attackController.doAttack(weapon.normalAttackHurtTime, weapon.normalAttackEndTime, weapon.NormalAttackHurt, weapon.NormalAttackEnd)) {
            //todo 播放攻击动画
            weapon.NormalAttackAnim();
        }
        
    }

    public override void beHurtAction() {
        base.beHurtAction();
    }

    //! 这是一种有实体的受伤方式，另一种由对方直接调用函数
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Weapon")) {
            beHurtController.beHurt(collision.gameObject.GetComponent<IWeapon>().computedAttack);
        }
    }


}
