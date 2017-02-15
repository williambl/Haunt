using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization; 
using System.IO;
using UnityEngine;

public static class SaverLoader {
	
	public static void Save(string saveName, Game saveGame) {
		XmlSerializer xmlS = new XmlSerializer();
		FileStream saveFile = File.Create (Application.persistentDataPath + "/" + saveName);
		xmlS.Serialize(saveFile, saveGame);
		saveFile.Close();
	}

	public static Game Load(string saveName) {
		if(File.Exists(Application.persistentDataPath + "/" + saveName)) {
			XmlSerializer xmlS = new XmlSerializer();
			FileStream file = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open);
			Game openedGame = (Game)xmlS.Deserialize(file);
			file.Close();
			return openedGame;
		}
		return null;
	}
}
