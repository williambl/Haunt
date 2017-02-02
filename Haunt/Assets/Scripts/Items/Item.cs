using UnityEngine;

[System.Serializable]
public class Item {

	/// <summary>
	/// Gets or sets the unique id.
	/// </summary>
	/// <value>The unique id.</value>
	public string id { get; set; }

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>The human-readable name.</value>
	public string name { get; set; }

	/// <summary>
	/// Gets or sets the metadata value.
	/// </summary>
	/// <value>The metadata value.</value>
	public int meta { get; set; }

	/// <summary>
	/// Gets or sets the item lore.
	/// </summary>
	/// <value>The item lore.</value>
	public string lore { get; set; }

	/// <summary>
	/// Gets or sets the game object of the item.
	/// </summary>
	/// <value>The game object of the item.</value>
	public GameObject gObject { get; set; } 

	public Item (string id, string name, int meta, string lore, GameObject gObject)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = lore;
		this.gObject = gObject;
	}

	public Item (string id, string name, int meta, string lore)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = lore;
		this.gObject = null;
	}

	public Item (string id, string name, int meta, GameObject gObject)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = "";
		this.gObject = gObject;
	}

	public Item (string id, string name, int meta)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = "";
		this.gObject = null;
	}

	public Item (string id, string name, string lore, GameObject gObject)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = lore;
		this.gObject = gObject;
	}

	public Item (string id, string name, string lore)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = lore;
		this.gObject = null;
	}
		
	public Item (string id, string name, GameObject gObject)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = "";
		this.gObject = gObject;
	}

	public Item (string id, string name)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = "";
		this.gObject = null;
	}

	public Item (string id, GameObject gObject)
	{
		this.id = id;
		this.name = id;
		this.meta = 0;
		this.lore = "";
		this.gObject = gObject;
	}

	public Item (string id)
	{
		this.id = id;
		this.name = id;
		this.meta = 0;
		this.lore = "";
		this.gObject = null;
	}

	public Item (string id, int meta, GameObject gObject)
	{
		this.id = id;
		this.name = id;
		this.meta = meta;
		this.lore = "";
		this.gObject = gObject;
	}

	public Item (string id, int meta)
	{
		this.id = id;
		this.name = id;
		this.meta = meta;
		this.lore = "";
		this.gObject = null;
	}
}