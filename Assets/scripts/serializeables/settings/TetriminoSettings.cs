using UnityEngine;
using System.Collections;

[System.Serializable]
public class TetriminoSettings {

	public GameObject tetriminoFab;
	public GameObject tetriminoPreviewFab;

	public Color[] tetriminoColors;
	
	public Vector2[,,] TetriminoShapes=new Vector2[7,4,4]{
		{ // J-TETRIMINO
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(0,-1) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(2, 0) },
			{ new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1), new Vector2(0,-1) },
		},
		
		{ // L-TETRIMINO
			{ new Vector2(2, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(1,-1) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(0, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1) }
		},
		
		{ // T-TETRIMINO
			{ new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(1, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1), new Vector2(1, 0) },
			{ new Vector2(0, 0), new Vector2(1, 1), new Vector2(1, 0), new Vector2(1,-1) }
		},
		
		{ // O-TETRIMINO
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) }
		},
		
		{ // I-TETRIMINO
			{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(0,-2) },
			{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(3, 0) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1), new Vector2(0,-2) }
		},                                                                           
		
		{ // S-TETRIMINO                                                                            
			{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1,-1) },
			{ new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(2, 1) },
			{ new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(1,-1) }
		},                                                                           
		
		{ // Z-TETRIMINO
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0) },
			{ new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0,-1) },
			{ new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0), new Vector2(2, 0) },
			{ new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0), new Vector2(0,-1) }
		}
	};

}
