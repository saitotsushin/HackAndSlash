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
    public DropAreaField dropAreaField;
    private CanvasGroup canvasGroup = null;
    public Camera raycastCamera;

    public void Awake()
    {
        this.self = this.transform;
        this.area = this.self.parent;
        // this.root = this.transform.root.Find("Canvas");
        // this.root = this.area.parent;
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグできるよういったん DropArea の上位に移動する
        this.self.SetParent(this.root);

        // UI 機能を一時的無効化
        this.canvasGroup.blocksRaycasts = false;
        dropAreaField.SetRaycastTarget(true);
        ItemManager.instance.DraggedItem = this.gameObject;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // this.self.localPosition = GetLocalPosition(((PointerEventData)eventData).position, this.transform);
        this.self.transform.position = eventData.position;
        CheckTarget((PointerEventData)eventData);
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
        CheckDrop((PointerEventData)eventData);

        // UI 機能を復元
        this.canvasGroup.blocksRaycasts = true;
        dropAreaField.SetRaycastTarget(false);
        //状態のリセット START #TODO まとめる
        SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
        Color newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        spriteRenderer.color = newColor;
        //状態のリセット END 
        ItemManager.instance.Fire();
    }
    private void CheckTarget(PointerEventData eventData){
        // ドロップ地点に DropAra があったらそこに入れる
        var dropTarget = GetRaycastTarget((PointerEventData)eventData);
        float newAlpha = 0f; // 例: アルファ値を半透明に設定

        if (dropTarget != null)
        {
            Transform objTransform = dropTarget.transform;
            ItemManager.instance.FieldTarget = dropTarget.gameObject;
            //状態のリセット START #TODO まとめる
            SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
            Color newColor = new Color(1.0f, 0f, 0f, 1.0f);
            spriteRenderer.color = newColor;         
            //状態のリセット END #TODO まとめる   
        }else{
            if(ItemManager.instance.FieldTarget){
                //状態のリセット START #TODO まとめる
                SpriteRenderer spriteRenderer = ItemManager.instance.FieldTarget.GetComponent<SpriteRenderer>();
                Color newColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                spriteRenderer.color = newColor;
                //状態のリセット END #TODO まとめる
                ItemManager.instance.FieldTarget = null;
            }
        }            
    }
    private void CheckDrop(PointerEventData eventData){
        // ドロップ地点に DropAra があったらそこに入れる
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
                    // this.area = dropEquipmentArea.transform;
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
        return null;
    }
    /// <summary>
    /// イベント発生地点の DropArea を取得する
    /// </summary>
    /// <param name="eventData">イベントデータ</param>
    /// <returns>DropArea</returns>
    private static DropArea GetRaycastArea(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach(var hit in results)
        {
            if (hit.gameObject.GetComponent<DropArea>())
            {
                // 処理
                return hit.gameObject.GetComponent<DropArea>();
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
