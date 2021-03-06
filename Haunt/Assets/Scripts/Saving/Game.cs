﻿[System.Serializable]
public class Game {

	public static Game currentGame;

	public int levelReached { get; set; }
	public GameState gameState { get; set; }
	public int level { get; set; }

	public Game (int levelReached, GameState gamestate, int level)
	{
		this.levelReached = levelReached;
		this.gameState = gamestate;
		this.level = level;
	}

	public Game ()
	{
		this.levelReached = 0;
		this.gameState = 0;
		this.level = 0;
	}
}
