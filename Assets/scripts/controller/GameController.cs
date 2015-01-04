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
	public SoundSettings soundSettings;

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
		initialize();
		

		nextTetriminoIndex=getRandomTetrimino();
		nextTetriminoRotationIndex=getRandomTetriminoRotation();

		createNextTetrimino();
	}

	void initialize(){
		uiSettings.initialize();
		soundSettings.playMusic();
	}

	public void setLevel(int _level){
		level=_level;

		uiSettings.setLevel(level);

		moveSettings.fallCooldown*=0.9f;
	}

	public void setLines(int _lines){
		lines+=_lines;

		uiSettings.setLines(lines);

		if(_lines == 2){
			uiSettings.showBonus(iTween.Hash(
				"headline", "DOUBLE BONUS",
				"text", "TRY TO GET MORE LINES AT ONCE"
			));	
		}
		if(_lines == 3){
			uiSettings.showBonus(iTween.Hash(
				"headline", "TRIPLE BONUS",
				"text", "CAN YOU MAKE A TETRIS?"
			));	
		}
		if(_lines == 4){
			uiSettings.showBonus(iTween.Hash(
				"headline", "QUAD BONUS",
				"text", "YOU MADE A TETRIS"
			));	
		}
		

		
	}

	public void addPoints(int _points){
		soundSettings.playLine();
		setLines(_points);
		points+=_points*_points;
		uiSettings.setPoints(points);

		if(lines > level*linesPerLevel){
			setLevel(level+1);
		}
	}

	public void destroyBrick(GameObject brick){
		float randomScale=Random.Range(10f, 15f);
		Vector3 randomScaleVector=new Vector3(randomScale, randomScale, randomScale);

		float randomRotation=Random.Range(90f, 270f);
		Vector3 randomRotationVector=new Vector3(randomRotation, randomRotation, randomRotation);

		iTween.FadeTo(brick, iTween.Hash(
			"alpha", 0,
			"time", 0.6f,
			"easetype", "easeOutSine"
		));
		iTween.RotateTo(brick, iTween.Hash(
			"rotation", randomRotationVector,
			"time", 0.6f,
			"easetype", "easeOutSine"
		));
		iTween.ScaleTo(brick, iTween.Hash(
			"scale", randomScaleVector,
			"time", 0.8f,
			"delay", Random.Range(0f,0.2f),
			"oncomplete", "removeBrick",
			"easetype", "easeOutSine"
		));

		
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