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
    private CanvasGroup canvasGroup = null;

    public void Awake()
    {
        this.self = this.transform;
        this.area = this.self.parent;
        // this.root = this.area.parent;
        this.canvasGroup = this.GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // ドラッグできるよういったん DropArea の上位に移動する
        this.self.SetParent(this.root);

        // UI 機能を一時的無効化
        this.canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        // this.self.localPosition = GetLocalPosition(((PointerEventData)eventData).position, this.transform);
        this.self.transform.position = eventData.position;
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
