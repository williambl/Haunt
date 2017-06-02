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
		if (!ItemExistsFromID (item.id))
			itemRegistry.Add (item);
	}
		
	/// <summary>
	/// Finds an item from its ID.
	/// </summary>
	/// <returns>The item.</returns>
	/// <param id="id">ID of the item.</param>
	public static Item FindItemByID (string id)
	{
		return itemRegistry.Find (x => x.id == id);
	}

	/// <summary>
	/// Determines if an item exists in the registry from its ID.
	/// </summary>
	/// <returns><c>true</c>, if the item exists, <c>false</c> otherwise.</returns>
	/// <param name="id">ID of the item.</param>
	public static bool ItemExistsFromID (string id)
	{
		return itemRegistry.Exists (x => x.id == id);
	}

	/// <summary>
	/// Determines whether an item exists in the registry.
	/// </summary>
	/// <returns><c>true</c>, if the item exists, <c>false</c> otherwise.</returns>
	/// <param name="item">The item.</param>
	public static bool ItemExists (Item item)
	{
		return itemRegistry.Exists (x => x == item);
	}
}
