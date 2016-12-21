using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Registry {
	private static List<Item> itemRegistry = new List<Item>();

	public static void RegisterItem (Item item) 
	{
		if (!itemRegistry.Contains (item))
			itemRegistry.Add (item);
		Debug.Log (((Item)itemRegistry.ToArray().GetValue(0)).id);
	}
}
