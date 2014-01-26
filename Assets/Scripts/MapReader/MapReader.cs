using UnityEngine;
using System.Collections;

public class MapReader : MonoBehaviour {

	enum TileType { Cliff, Grass, Water, FuckItsBroken };

	private Color _cliffColor = new Color(0.765f, 0.816f, 0.816f);

	private const float TOLERANCE = 0.05f;

	private const int HEX_GRID_WIDTH = 65;

	//center of the top left tile in the map in the native coordinates system of the texture
	public Vector2 startPoint = new Vector2(58,37);
	public string fileName;


	// Use this for initialization
	void Start () 
	{
		loadMap();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void loadMap()
	{
		Texture2D texture = Resources.Load(fileName) as Texture2D;

		int xTiles = (int)Mathf.Ceil(texture.width / HEX_GRID_WIDTH);

		for (int i = 0; i < xTiles; i++)
		{
			Color col = texture.GetPixel((int)startPoint.x + (i* HEX_GRID_WIDTH), (int)startPoint.y);

			TileType tileType = identifyTile(col);
			Debug.Log(tileType);


		}

		Debug.Log("end lline");

	}

	TileType identifyTile(Color tileCenterColor)
	{
		if( colorsKindaMatch(tileCenterColor, _cliffColor))
        {
            return TileType.Cliff;
        }

		return TileType.FuckItsBroken;

	}

    bool colorsKindaMatch(Color one, Color two)
    {
        if( prettyMuchTheSame(one.r, two.r) &&
        prettyMuchTheSame(one.r, two.r) &&
        prettyMuchTheSame(one.r, two.r) )
        {
			return true;
        }

        return false;

    }

    bool prettyMuchTheSame(double col1, double col2)
    {
        if((col1 > col2 - TOLERANCE) && (col1 < col2 + TOLERANCE))
            return true;

        return false;
    }
}
