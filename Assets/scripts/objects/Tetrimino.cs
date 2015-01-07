using UnityEngine;
using System.Collections;

public class Tetrimino : MonoBehaviour {

	public static Tetrimino instance;

	public enum State {
		Spawning,
		Falling,
		Landed,
		Fixed,
		Preview
	}

	public State state;

	bool isMoving=false;

	GameController game;
	GameObject[,] field;
	Vector2[,,] shapes;

	int shapeIndex=2;
	int rotationIndex=0;

	float nextFall=0;



	void Awake(){
		instance=this;
	}

	public void initialize(int _shapeIndex, int _rotationIndex){
		shapeIndex=_shapeIndex;
		rotationIndex=_rotationIndex;

		game=GameController.instance;
		shapes=game.tetriminoSettings.TetriminoShapes;

		switch(state){
			case State.Spawning:
				createTetrimino();
				transform.position=new Vector3((int)(game.fieldSettings.fieldWidth/2), game.fieldSettings.fieldHeight-2, 0);

				foreach(Transform brick in transform){
					if(game.field[(int)brick.position.y, (int)brick.position.x] != null){
						game.gameIsOver();
						return;
					}
				}

				state=State.Falling;
			break;
		}
	}

	void Update(){
		if(game.gameOver) return;

		if(state != State.Fixed && state != State.Preview && state != State.Spawning){
			if(state != State.Landed && Input.GetAxis("Vertical") < 0){
				StartCoroutine(fall());
			}

			if(Input.GetButton("Horizontal")){
				if(!isMoving){
					StartCoroutine(move());	
				}
			}

			if(Input.GetKeyDown("up")){
				if(CanRotate){
					StartCoroutine(rotate());
				}
			}

			if(nextFall < 0){
				StartCoroutine(fall());
				nextFall=game.moveSettings.fallCooldown;
			}

			nextFall-=game.moveSettings.fallSpeed * Time.deltaTime;
		}
	}


	IEnumerator move(){
		float moved=0f;
		float direction=Input.GetAxis("Horizontal");

		if((CanMoveRight && direction > 0) || (CanMoveLeft && direction < 0)){
			isMoving=true;
			while(moved <= 1.0f){
				float moveStep=Mathf.Min(game.moveSettings.moveSpeed * Time.deltaTime, 1.1f - moved);

				if(direction > 0){
					transform.Translate(Vector3.right * moveStep, Space.World);
				}else if(direction < 0){
					transform.Translate(Vector3.left * moveStep, Space.World);
				}

				moved+=moveStep;
				yield return 0;
			}

			transform.position=new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
		}

		isMoving=false;
	}

	IEnumerator fall(){
		if(CanMoveDown){
			transform.Translate(Vector3.down, Space.World);
		}else if(state == State.Falling){
			state=State.Landed;

			yield return new WaitForSeconds(0.4f);

			while(isMoving){
				yield return new WaitForEndOfFrame();
			}

			if(CanMoveDown){
				state=State.Falling;
			}else{
				state=State.Fixed;
				game.soundSettings.playDrop();

				foreach(Transform brick in transform){
					game.field[Mathf.RoundToInt(brick.position.y), Mathf.RoundToInt(brick.position.x)]=brick.gameObject;
				}

				ArrayList lines=findLines();

				if(lines.Count > 0){
					game.addPoints(lines.Count);
					
					foreach(int line in lines){
						int y=Mathf.CeilToInt(line);

						for(int i=0; i < game.fieldSettings.fieldWidth; i++){
							game.destroyBrick(game.field[y,i]);
							game.field[y,i]=null;
						}
					}

					yield return new WaitForEndOfFrame();

					lines.Sort();
					lines.Reverse();

					foreach(int line in lines){
						if(line >= game.fieldSettings.fieldHeight-1){
							continue;
						}

						lineFall(line+1);
					}
				}

				Transform[] bricks=GetComponentsInChildren<Transform>();
				for(int i=0; i < bricks.Length; i++){
					bricks[i].parent=null;
				}

				game.createNextTetrimino();
			}
		}
	}

	IEnumerator rotate(){
		foreach(Transform brick in transform){
			Destroy(brick.gameObject);
		}

		rotationIndex=(rotationIndex+1)%shapes.GetLength(2);
		createTetrimino();

		yield return 0;
	}


	ArrayList findLines(){
		ArrayList lines=new ArrayList();
		Hashtable open=new Hashtable();

		foreach(Transform brick in transform){
			int line=Mathf.CeilToInt(brick.position.y);

			if(!open.ContainsKey(line)){
				open.Add(line,line);
			}
		}

		foreach(int y in open.Keys){
			bool fullline=true;

			for(int i=0; i < game.fieldSettings.fieldWidth; i++){
				fullline &= game.field[Mathf.RoundToInt(y), i] != null;
			}

			if(fullline){
				lines.Add((int)y);
			}
		}

		return lines;
	}

	void lineFall(int line){
		int bottom=Mathf.RoundToInt(line);

		for(int y=bottom; y < game.fieldSettings.fieldHeight; y++){
			for(int x=0; x < game.fieldSettings.fieldWidth; x++){
				game.field[y-1,x]=game.field[y,x];

				if(game.field[y-1,x] != null){
					game.field[y-1,x].transform.Translate(Vector3.down);
				}

				game.field[y,x]=null;
			}
		}
	}


	void createTetrimino(){
		foreach(Transform brick in transform){
			Destroy(brick.gameObject);
		}

		for(int i=0; i < shapes.GetLength(2); i++){
			GameObject tmp=(GameObject)Instantiate(game.brickSettings.brickFab, shapes[shapeIndex, rotationIndex, i], Quaternion.identity);

			tmp.transform.Translate(transform.position);
			tmp.transform.parent=gameObject.transform;
			tmp.renderer.material.color=game.tetriminoSettings.tetriminoColors[shapeIndex];
		}
	}
	

	bool CanMoveDown {
		get{
			if(isMoving) return false;

			foreach(Transform brick in transform){
				if(Mathf.RoundToInt(brick.position.y - 1) < 0 || game.field[Mathf.RoundToInt(brick.position.y - 1), Mathf.RoundToInt(brick.position.x)] != null){
					return false;
				}
			}

			return true;
		}
	}

	bool CanMoveRight {
		get{
			bool canMoveRight=true;

			foreach(Transform brick in transform){
				canMoveRight &= Mathf.RoundToInt(brick.position.x+1) < game.fieldSettings.fieldWidth && game.field[Mathf.RoundToInt(brick.position.y), Mathf.RoundToInt(brick.position.x+1)] == null;
			}

			return canMoveRight;
		}
	}

	bool CanMoveLeft {
		get{
			bool canMoveLeft=true;

			foreach(Transform brick in transform){
				canMoveLeft &= Mathf.RoundToInt(brick.position.x-1) >= 0 && game.field[Mathf.RoundToInt(brick.position.y), Mathf.RoundToInt(brick.position.x-1)] == null;
			}

			return canMoveLeft;
		}
	}

	bool CanRotate {
		get{
			for(int i=0; i < shapes.GetLength(2); i++){
				Vector2 tmp=new Vector2(transform.position.x, transform.position.y) + shapes[shapeIndex, (rotationIndex+1)% shapes.GetLength(2), i];

				if(tmp.x < 0 || tmp.x >= game.fieldSettings.fieldWidth || tmp.y < 0 || tmp.y >= game.fieldSettings.fieldHeight){
					return false;
				}

				if(game.field[Mathf.RoundToInt(tmp.y), Mathf.RoundToInt(tmp.x)] != null){
					return false;
				}
			}

			return true;
		}
	}

}
