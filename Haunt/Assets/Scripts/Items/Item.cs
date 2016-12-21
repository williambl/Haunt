[System.Serializable]
public class Item {

	public string id { get; set; }
	public string name { get; set; }
	public int meta { get; set; }
	public string lore { get; set; }

	public Item (string id, string name, int meta, string lore)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = lore;
	}

	public Item (string id, string name, int meta)
	{
		this.id = id;
		this.name = name;
		this.meta = meta;
		this.lore = "";
	}

	public Item (string id, string name, string lore)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = lore;
	}

	public Item (string id, string name)
	{
		this.id = id;
		this.name = name;
		this.meta = 0;
		this.lore = "";
	}

	public Item (string id)
	{
		this.id = id;
		this.name = id;
		this.meta = 0;
		this.lore = "";
	}

	public Item (string id, int meta)
	{
		this.id = id;
		this.name = id;
		this.meta = meta;
		this.lore = "";
	}
}