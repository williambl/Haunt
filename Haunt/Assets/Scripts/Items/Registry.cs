using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Registry {
	public static List<Item> itemRegistry = new List<Item>();

	/// <summary>
	/// Registers an item.
	/// </summary>
	/// <param name="item">Item to register.</param>
	public static void RegisterItem (Item item) 
	{
		if (!itemRegistry.Contains (item))
			itemRegistry.Add (item);
		Debug.Log (((Item)itemRegistry.ToArray().GetValue(0)).id);
	}
}
