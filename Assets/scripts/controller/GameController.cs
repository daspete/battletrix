using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static GameController instance;

	public FieldSettings fieldSettings;
	public TetriminoSettings tetriminoSettings;
	public BrickSettings brickSettings;
	public MoveSettings moveSettings;
	public CameraSettings cameraSettings;
	public UISettings uiSettings;

	public int linesPerLevel=10;

	public GameObject[,] field;

	GameObject tetrimino;
	GameObject tetriminoPreview;

	int currentTetriminoIndex;
	int currentTetriminoRotationIndex;
	int nextTetriminoIndex;
	int nextTetriminoRotationIndex;

	int level=1;
	int points=0;
	int lines=0;

	void Awake(){
		instance=this;

		field=new GameObject[fieldSettings.fieldHeight, fieldSettings.fieldWidth];
	}

	void Start(){
		nextTetriminoIndex=getRandomTetrimino();
		nextTetriminoRotationIndex=getRandomTetriminoRotation();

		createNextTetrimino();
	}

	public void setLevel(int _level){
		level=_level;

		uiSettings.setLevel(level);

		moveSettings.fallCooldown*=0.9f;
	}

	public void setLines(int _lines){
		lines+=_lines;

		uiSettings.setLines(lines);
	}

	public void addPoints(int _points){
		setLines(_points);
		points+=_points*_points;
		uiSettings.setPoints(points);

		if(lines > level*linesPerLevel){
			setLevel(level+1);
		}
	}

	int getRandomTetrimino(){
		return (int)(Random.Range(0, tetriminoSettings.TetriminoShapes.GetLength(0)));
	}

	int getRandomTetriminoRotation(){
		return (int)(Random.Range(0, tetriminoSettings.TetriminoShapes.GetLength(1)));
	}

	public void createNextTetrimino(){
		currentTetriminoIndex=nextTetriminoIndex;
		currentTetriminoRotationIndex=nextTetriminoRotationIndex;

		nextTetriminoIndex=getRandomTetrimino();
		nextTetriminoRotationIndex=getRandomTetriminoRotation();

		if(tetrimino != null){
			Destroy(tetrimino);	
		}

		if(tetriminoPreview != null){
			tetriminoPreview.GetComponent<TetriminoPreview>().destroyTetriminoPreview();
		}
		
		tetrimino=(GameObject)Instantiate(tetriminoSettings.tetriminoFab, Vector3.zero, Quaternion.identity);
		tetrimino.GetComponent<Tetrimino>().initialize(currentTetriminoIndex, currentTetriminoRotationIndex);

		tetriminoPreview=(GameObject)Instantiate(tetriminoSettings.tetriminoPreviewFab, Vector3.zero, Quaternion.identity);
		tetriminoPreview.GetComponent<TetriminoPreview>().initialize(nextTetriminoIndex, nextTetriminoRotationIndex);
	}

}