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
		float minX=0;
		float minY=0;
		float maxX=0;
		float maxY=0;
		int i=0;

		float brickSize=28f;
		float full=150f;

		for(i=0; i < shapes.GetLength(2); i++){
			brickPositions[i]=shapes[shapeIndex, rotationIndex, i];

			if(brickPositions[i].x < minX){
				minX=(float)brickPositions[i].x;
			}
			if(brickPositions[i].y < minY){
				minY=(float)brickPositions[i].y;
			}

			if(brickPositions[i].x > maxX){
				maxX=(float)brickPositions[i].x;
			}
			if(brickPositions[i].y > maxY){
				maxY=(float)brickPositions[i].y;
			}
		}

		float rangeX=maxX-minX+1;
		float rangeY=maxY-minY+1;
		
		if(rangeY%2!=0 && rangeY > 1){
			rangeY-=1.5f;
		}
		if(rangeY > 3){
			rangeY-=4f;
		}

		rangeX*=brickSize;
		rangeY*=brickSize;

		float startX=-full/2-rangeX/2;
		float startY=-full/2-rangeY/2;
		
		for(i=0; i < shapes.GetLength(2); i++){
			Vector2 pos=new Vector2(startX, startY);
			pos+=brickPositions[i] * brickSize;

			bricks[i]=(GameObject)Instantiate(game.brickSettings.brickPreviewFab, pos, Quaternion.identity);
			bricks[i].transform.SetParent(game.uiSettings.UI.transform, false);
			bricks[i].GetComponent<Image>().color=game.tetriminoSettings.tetriminoColors[shapeIndex];
		}
	}
}
