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
        // CheckDrop((PointerEventData)eventData);

        // UI 機能を復元
        this.canvasGroup.blocksRaycasts = true;
        stageDropAreaField.SetRaycastTarget(false);
        if(ItemManager.instance.FieldTarget){
            //状態のリセット START #TODO まとめる
            SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
            Color newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            spriteRenderer.color = newColor;
            //状態のリセット END 
        }
        if(CanDrop){
            ItemManager.instance.Fire();
        }else{
            this.self.SetParent(this.area);
        }
    }
    private bool CheckTarget(PointerEventData eventData){
        // ドロップ地点に DropAra があったらそこに入れる
        // bool CanSetted = false;
        var dropTarget = GetRaycastTarget((PointerEventData)eventData);
        float newAlpha = 0f; // 例: アルファ値を半透明に設定

        if (dropTarget != null)
        {
            // Transform objTransform = dropTarget.transform.parent;

            TestManager.instance.TestText.text = dropTarget.transform.gameObject.name.ToString();

            string prefabTag = dropTarget.transform.tag;
            if(targetRange == TargetRange.SINGLE){
                if(prefabTag == "ItemDropField"){
                    //アイテムがSINGLEで対象がPLAYER
                    SetTarget(useTarget);
                }
                if(useTarget == UseTarget.ENEMY && prefabTag == "Enemy"){
                    SetTarget(useTarget,dropTarget.gameObject);
                    // ItemManager.instance.FieldTarget = dropTarget.gameObject;
                    // //状態のリセット START #TODO まとめる
                    // SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
                    // Color newColor = new Color(1.0f, 0f, 0f, 1.0f);
                    // spriteRenderer.color = newColor;
                    // //状態のリセット END #TODO まとめる   
                    return true;
                }
            }
            if (targetRange == TargetRange.ALL)
            {
                
                return true;
            }
            
        }else{
            if(ItemManager.instance.FieldTarget){
                ReSetTarget();
                //状態のリセット START #TODO まとめる
                // SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
                // Color newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                // spriteRenderer.color = newColor;
                //状態のリセット END #TODO まとめる
                ItemManager.instance.FieldTarget = null;
            }
        }           
        return false; 
    }
    public void SetTarget(UseTarget _useTarget, GameObject _dropTarget = null){
        //ターゲットがプレイヤー
        if(_useTarget == UseTarget.PLAYER){
            ItemManager.instance.FieldTarget = Player.instance.gameObject;
            CursorArrow _CursorArrow = Player.instance.gameObject.GetComponent<CursorArrow>();
            _CursorArrow.CursorShow(true);
        }
        if(_useTarget == UseTarget.ENEMY){
            if(_dropTarget){
                if(_dropTarget.transform.tag == "ItemDropField"){
                    // SpawnManager.instance.EnemySpawnList
                }
                if(_dropTarget.transform.tag == "Enemy"){
                    
                }
                ItemManager.instance.FieldTarget = Player.instance.gameObject;
            }
        }
    }
    public void ReSetTarget(){
        GameObject Obj = ItemManager.instance.FieldTarget.gameObject;
        CursorArrow _CursorArrow = Obj.GetComponent<CursorArrow>();
        _CursorArrow.CursorShow(false);
        ItemManager.instance.FieldTarget = null;
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
