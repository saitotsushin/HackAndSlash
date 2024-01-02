using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
[CreateAssetMenu(fileName = "ItemDataBase", menuName="CreateItemDataBase")]
public class ItemDataBase : ScriptableObject {
 
	[SerializeField]
	private List<ItemData> itemLists = new List<ItemData>();
 
	//　アイテムリストを返す
	public List<ItemData> GetItemLists() {
		return itemLists;
	}
}