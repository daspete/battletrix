using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TetriminoPreview : MonoBehaviour {

	int shapeIndex;
	int rotationIndex;

	GameController game;
	Vector2[,,] shapes;

	GameObject[] bricks;
	Vector2[] brickPositions;

	public void initialize(int _shapeIndex, int _rotationIndex){
		shapeIndex=_shapeIndex;
		rotationIndex=_rotationIndex;

		game=GameController.instance;
		shapes=game.tetriminoSettings.TetriminoShapes;

		bricks=new GameObject[shapes.GetLength(2)];
		brickPositions=new Vector2[shapes.GetLength(2)];

		createTetriminoPreview();
	}

	public void destroyTetriminoPreview(){
		for(int i=0; i < bricks.Length; i++){
			Destroy(bricks[i]);
		}

		Destroy(gameObject);
	}

	void createTetriminoPreview(){
		int minX=0;
		int minY=0;
		int maxX=0;
		int maxY=0;
		int i=0;

		int brickSize=28;
		int full=brickSize*4;

		for(i=0; i < shapes.GetLength(2); i++){
			brickPositions[i]=shapes[shapeIndex, rotationIndex, i];

			if(brickPositions[i].x < minX){
				minX=(int)brickPositions[i].x;
			}
			if(brickPositions[i].y < minY){
				minY=(int)brickPositions[i].y;
			}

			if(brickPositions[i].x > maxX){
				maxX=(int)brickPositions[i].x;
			}
			if(brickPositions[i].y > maxY){
				maxY=(int)brickPositions[i].y;
			}
		}

		int rangeX=maxX-minX;
		int rangeY=maxY-minY;

		rangeX*=brickSize;
		rangeY*=brickSize;


		for(i=0; i < shapes.GetLength(2); i++){
			Vector2 pos=new Vector2(rangeX-(full+rangeX), rangeY-(full+rangeY));
			pos+=brickPositions[i] * brickSize;

			bricks[i]=(GameObject)Instantiate(game.brickSettings.brickPreviewFab, pos, Quaternion.identity);
			bricks[i].transform.SetParent(game.uiSettings.UI.transform, false);
			bricks[i].GetComponent<Image>().color=game.tetriminoSettings.tetriminoColors[shapeIndex];
		}
	}
}
