using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemManager : MonoBehaviour {

	static public ItemManager instance;
	//　アイテムデータベース
	[SerializeField]
	private ItemDataBase itemDataBase;
    public GameObject MenuItem;
    public GameObject MenuItemsUI;
    public GameObject FieldItem;
	public GameObject ItemFieldArea;
    public GameObject FieldTarget;
    public GameObject DraggedItem;

	public EquipmentSlot EquipmentList1;//WEAPON
    public EquipmentSlot EquipmentList2;//ARMOR
    public EquipmentSlot EquipmentList3;
    public Transform DraggableRoot;
    public DropAreaField dropAreaField;
    public Camera raycastCamera;

    void Awake ()
    {
        if (instance == null) {
        
            instance = this; 
        }
        else {
            Destroy (gameObject);
        }
    }
	//　名前でアイテムを取得
	public ItemData GetItem(int searchId) {
		return itemDataBase.GetItemLists().Find(item => item.itemId == searchId);
	}
	public void CreateFieldItem(int _ItemId,GameObject _Enemy){
        ItemData _ItemData = GetItem(_ItemId);

		GameObject Item = Instantiate(
			FieldItem,
			_Enemy.transform.position,
			Quaternion.identity,
			ItemFieldArea.transform
		);
        FieldItem _FieldItem = Item.GetComponent<FieldItem>();
        _FieldItem.SetItemStatus(_ItemData);
	}
	public void SetItem(int _ItemId){
        ItemData _ItemData = GetItem(_ItemId);

		GameObject Item = Instantiate(
			MenuItem,
			new Vector3(0,0,0),
			Quaternion.identity,
			MenuItemsUI.transform
		);
        MenuItem _MenuItem = Item.GetComponent<MenuItem>();
        _MenuItem.SetItemStatus(_ItemData);
        Draggable _Draggable = Item.GetComponent<Draggable>();
        _Draggable.root = DraggableRoot;
        _Draggable.dropAreaField = dropAreaField;
        
        _Draggable.raycastCamera = raycastCamera;

    }
	public void SetEquipment(MenuItem _MenuItem){
        EquipmentSlot ChangeItem;
        ItemData _BeforeItemData;
		ItemData _AfterItemData = GetItem(_MenuItem.itemId);
        switch(_MenuItem.equipment){
            case Equipment.WEAPON:
				// ChangeItem = EquipmentList1 = _MenuItem;
                ChangeItem = EquipmentList1;
                _BeforeItemData = GetItem(EquipmentList1.itemId);
                break;
            case Equipment.ARMOR:
				// EquipmentList2 = _MenuItem;
                ChangeItem = EquipmentList2;
                _BeforeItemData = GetItem(EquipmentList2.itemId);
                break;
            case Equipment.ACCESORIE:
				// EquipmentList3 = _MenuItem;
                ChangeItem = EquipmentList3;
                _BeforeItemData = GetItem(EquipmentList3.itemId);
                break;
            default:
				_BeforeItemData = GetItem(1);
                ChangeItem = EquipmentList1;
                break;
        }
		//アイテムを入れ替える
		_MenuItem.SetItemStatus(_BeforeItemData);
		ChangeItem.SetItemStatus(_AfterItemData);
		List<EquipmentSlot> _EquipmentList = new List<EquipmentSlot>();
        _EquipmentList.Add(EquipmentList1);
		_EquipmentList.Add(EquipmentList2);
		_EquipmentList.Add(EquipmentList3);
        Player.instance.mPlayerStatus.UpdateEquipment(_EquipmentList);
    }
	public void LoadEquipmentSlot(){
        EquipmentList1.SetItemStatus(GetItem(GameSettingData.EquipmentId_1));
        EquipmentList2.SetItemStatus(GetItem(GameSettingData.EquipmentId_2));
        EquipmentList3.SetItemStatus(GetItem(GameSettingData.EquipmentId_3));
    }
    public void Fire(){
        if(FieldTarget){
            MenuItem _MenuItem = DraggedItem.GetComponent<MenuItem>();
            _MenuItem.Fire();
            Destroy(DraggedItem);
            DraggedItem = null;
        }
    }
}