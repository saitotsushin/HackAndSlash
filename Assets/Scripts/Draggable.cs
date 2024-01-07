using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform root;
    public Transform area;
    private Transform self;
    public DropAreaFieldTarget stageDropAreaField;
    private CanvasGroup canvasGroup = null;
    public Camera raycastCamera;
    public UseTarget useTarget;
    public TargetRange targetRange;
    public bool CanDrop = false;

    public void Awake()
    {
        this.self = this.transform;
        this.area = this.self.parent;
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }
    void Start()
    {
        MenuItem mMenuItem = this.gameObject.GetComponent<MenuItem>();
        useTarget = mMenuItem.useTarget;
        targetRange = mMenuItem.targetRange;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグできるよういったん DropArea の上位に移動する
        this.self.SetParent(this.root);

        // UI 機能を一時的無効化
        this.canvasGroup.blocksRaycasts = false;
        stageDropAreaField.SetRaycastTarget(true);
        ItemManager.instance.DraggedItem = this.gameObject;
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.self.transform.position = eventData.position;
        CanDrop = CheckTarget((PointerEventData)eventData);
    }

    private static Vector3 GetLocalPosition(Vector3 position, Transform transform)
    {
        // 画面上の座標 (Screen Point) を RectTransform 上のローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            position,
            Camera.main,
            out var result);
        return new Vector3(result.x, result.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // UI 機能を復元
        this.canvasGroup.blocksRaycasts = true;
        stageDropAreaField.SetRaycastTarget(false);

        if(CanDrop){
            ItemManager.instance.Fire();
        }else{
            this.self.SetParent(this.area);
        }
        if(ItemManager.instance.FieldTarget.Count > 0){
            ReSetTarget();
        }
    }
    private bool CheckTarget(PointerEventData eventData){
        // ドロップ地点に DropAra があったらそこに入れる
        // bool CanSetted = false;
        var dropTarget = GetRaycastTarget((PointerEventData)eventData);
        float newAlpha = 0f; // 例: アルファ値を半透明に設定

        if (dropTarget != null)
        {
            TestManager.instance.TestText.text = dropTarget.transform.gameObject.name.ToString();

            string prefabTag = dropTarget.transform.tag;
            if(targetRange == TargetRange.SINGLE){
                if(prefabTag == "ItemDropField"){
                    //アイテムがSINGLEで対象がPLAYER
                    SetSingleTarget(useTarget,dropTarget.gameObject);
                }
                if(useTarget == UseTarget.ENEMY && prefabTag == "Enemy"){
                    
                    SetSingleTarget(useTarget,dropTarget.gameObject);
                }
            }
            if (targetRange == TargetRange.ALL)
            {
                SetAllTarget(useTarget);
            }
            
        }else{
            if(ItemManager.instance.FieldTarget.Count > 0){
                ReSetTarget();
                //リセット
                ItemManager.instance.FieldTarget = new List<GameObject>();
            }
        }
        if (ItemManager.instance.FieldTarget.Count > 0)
        {
            return true;
        }
        return false; 
    }
    public void SetSingleTarget(UseTarget _useTarget, GameObject _dropTarget = null){
        ReSetTarget();
        //ターゲットがプレイヤー、
        if(_useTarget == UseTarget.PLAYER){
            ItemManager.instance.FieldTarget.Add(Player.instance.gameObject);
            CursorArrow _CursorArrow = Player.instance.gameObject.GetComponent<CursorArrow>();
            _CursorArrow.CursorShow(true);
        }
        if(_useTarget == UseTarget.ENEMY){
            if(_dropTarget){
                if(_dropTarget.transform.tag == "ItemDropField"){
                    if(Player.instance.AttackEnemy){
                        //すでに攻撃しようとしている敵を対象にする
                        ItemManager.instance.FieldTarget.Add(Player.instance.AttackEnemy);
                    }else{
                        //なければ一番近い敵を対象にする
                        Enemy _Enemy = Player.instance.mEnemyEffectArea.GetNearEnemy(Player.instance.gameObject);
                        if(_Enemy){
                            ItemManager.instance.FieldTarget.Add(_Enemy.gameObject);
                        }
                    }
                }
                if(_dropTarget.transform.tag == "Enemy"){
                    ItemManager.instance.FieldTarget.Add(_dropTarget.transform.parent.gameObject);          
                }
                if(ItemManager.instance.FieldTarget.Count > 0){
                    for(int i = 0; i < ItemManager.instance.FieldTarget.Count; i++){
                        CursorArrow _CursorArrow = ItemManager.instance.FieldTarget[i].gameObject.GetComponent<CursorArrow>();
                        _CursorArrow.CursorShow(true);
                    }
                }
            }
        }
    }
    public void SetAllTarget(UseTarget _useTarget){
        ItemManager.instance.FieldTarget = new List<GameObject>();
        List<GameObject> EnemyList = Player.instance.mEnemyEffectArea.EnemyList;
        Debug.Log("EnemyList.Count=" + EnemyList.Count);
        if (_useTarget == UseTarget.ENEMY)
        {
            for(int i = 0; i < EnemyList.Count; i++){
                CursorArrow _CursorArrow = EnemyList[i].GetComponent<CursorArrow>();
                _CursorArrow.CursorShow(true);
                ItemManager.instance.FieldTarget.Add(EnemyList[i]);
            }
        }
    }
    public void ReSetTarget(){
        if(ItemManager.instance.FieldTarget.Count > 0){
            for (int i = 0; i < ItemManager.instance.FieldTarget.Count; i++)
            {
                GameObject Obj = ItemManager.instance.FieldTarget[i].gameObject;
                if (Obj)
                {
                    CursorArrow _CursorArrow = Obj.GetComponent<CursorArrow>();
                    if (_CursorArrow)
                    {
                        _CursorArrow.CursorShow(false);
                    }
                }
            }

        }
        ItemManager.instance.FieldTarget = new List<GameObject>();
    }
    private void CheckDrop(PointerEventData eventData){
        var dropArea = GetRaycastArea((PointerEventData)eventData);
        if (dropArea != null)
        {
            this.area = dropArea.transform;
        }
        this.self.SetParent(this.area);  

        var dropEquipmentArea = GetRaycastEquipmentArea((PointerEventData)eventData);
        if (dropEquipmentArea != null)
        {
            if(this.gameObject.GetComponent<MenuItem>()){
                MenuItem _MenuItem = this.gameObject.GetComponent<MenuItem>();
                if(_MenuItem.itemType == ItemType.EQUIPMENT){
                    ItemManager.instance.SetEquipment(_MenuItem);
                }
            }
        }    
    }
    /// <summary>
    /// イベント発生地点の DropArea を取得する
    /// </summary>
    /// <param name="eventData">イベントデータ</param>
    /// <returns>DropArea</returns>
    public DropAreaFieldTarget GetRaycastTarget(PointerEventData eventData)
    {
        // ドラッグ中の座標を取得
        Vector2 dragPosition = eventData.position;

        // Raycastを使ってドラッグ中の座標に対するGameObjectを取得
        RaycastHit2D[] hits = Physics2D.RaycastAll(raycastCamera.ScreenToWorldPoint(dragPosition), Vector2.zero);
        // ヒットした全てのGameObjectに対する処理を行います
        foreach (RaycastHit2D hit in hits)
        {
            GameObject hitGameObject = hit.collider.gameObject;
            if (hitGameObject.GetComponent<DropAreaFieldTarget>())
            {
                return hitGameObject.GetComponent<DropAreaFieldTarget>();
            }
        }
        //UI周り
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var hit in results)
        {
            if (hit.gameObject.GetComponent<DropAreaFieldTarget>())
            {
                // 処理
                return hit.gameObject.GetComponent<DropAreaFieldTarget>();
            }
        }
        return null;
    }
    /// <summary>
    /// イベント発生地点の DropArea を取得する
    /// </summary>
    /// <param name="eventData">イベントデータ</param>
    /// <returns>DropArea</returns>
    private static DropAreaFieldTarget GetRaycastArea(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var hit in results)
        {
            if (hit.gameObject.GetComponent<DropAreaFieldTarget>())
            {
                // 処理
                return hit.gameObject.GetComponent<DropAreaFieldTarget>();
            }
        }
        return null;
    }
    private static DropAreaEquipment GetRaycastEquipmentArea(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var hit in results)
        {
            if (hit.gameObject.GetComponent<DropAreaEquipment>())
            {
                // 処理
                return hit.gameObject.GetComponent<DropAreaEquipment>();
            }
        }
        return null;
    }
}
