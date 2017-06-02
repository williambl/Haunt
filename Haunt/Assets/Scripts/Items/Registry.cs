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
	}

	/// <summary>
	/// Finds an item by its id.
	/// </summary>
	/// <returns>The item.</returns>
	/// <param id="id">The id of the item.</param>
	public static Item FindItemByID (string id)
	{
		return itemRegistry.Find (x => x.id == id);
	}
}
