using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    static public SpawnManager instance;
    public List<GameObject> EnemyList;
    public List<GameObject> EnemySpawnList;
    public Camera MainCamera;
    private Transform CameraRange;
    private float time;
    public float CreateTime = 3.0f;
    // public GameObject Player;
    public GameObject EnemyField;
    public int DebugCount = 0;
    public EnemyCreatArea mEnemyCreatArea;
    void Awake ()
    {
        if (instance == null) {
        
            instance = this; 
        }
        else {
            Destroy (gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraRange = MainCamera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.instance.IsActionMove == false){
            return;
        }
        // 前フレームからの時間を加算していく
        time = time + Time.deltaTime;
        
        if(DebugCount < 3){
        }else{
            return;
        }

        // 約1秒置きにランダムに生成されるようにする。
        if(time > CreateTime)
		{
            // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
            Vector3 posA = mEnemyCreatArea.RangeA.position;
            Vector3 posB = mEnemyCreatArea.RangeB.position;
            float x = Random.Range(posA.x, posB.x);
            // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
            float y = Random.Range(posA.y, posB.y);

            GameObject createPrefab = EnemyList[0];

            // GameObjectを上記で決まったランダムな場所に生成
            GameObject Enemy = Instantiate(
                EnemyList[0],
                new Vector3(x,y,0),
                createPrefab.transform.rotation,
                EnemyField.transform
            );
            EnemySpawnList.Add(Enemy);
            DebugCount++;

            // 経過時間リセット
            time = 0f;
        }
    }
}
