using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WallPositions : MonoBehaviour
{
    public GameObject[][] AutoWallPos = new GameObject[21][];
    Vector3 WallPos; //生成する壁のワールド座標
    public GameObject AutoWalls;　//壁の位置プレハブ
    public GameObject AutoWallPrefab;
    public GameObject Door_Horizontal;
    public GameObject Door_Vertical;

    public int Width { get; }
    public int Height { get; }

    NavMeshSurface navMeshSurface;

    // セル情報
    private struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    // 方向
    private enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    // 乱数生成用
    private Random Random;
    // 現在拡張中の壁情報を保持
    private Stack<Cell> CurrentWallCells;
    //private Stack<int> CurrentWallIndex;
    // 壁の拡張を行う開始セルの情報
    private List<Cell> StartCells;

    public WallPositions()
    {
        // 迷路情報を初期化
        this.Width = 21;
        this.Height = 21;
        StartCells = new List<Cell>();
        CurrentWallCells = new Stack<Cell>();
        this.Random = new Random();
    }

    public GameObject[][] CreateMaze()
    {

        // 各マスの初期設定を行う
        for (int y = 0; y < this.Height; y++)
        {
            for (int x = 0; x < this.Width; x++)
            {
                // 外周のみ壁にしておき、開始候補として保持
                if (x == 0 || y == 0 || x == this.Width - 1 || y == this.Height - 1)
                {
                    this.AutoWallPos[x][y].tag = "Wall";
                }
                else
                {
                    this.AutoWallPos[x][y].tag = "Path";

                    // 外周ではない偶数座標を壁伸ばし開始点にしておく
                    if (x % 2 == 0 && y % 2 == 0)
                    {
                        // 開始候補座標
                        StartCells.Add(new Cell(x, y));
                    }
                }
            }
        }

        // 壁が拡張できなくなるまでループ
        while (StartCells.Count > 0)
        {
            // ランダムに開始セルを取得し、開始候補から削除
            var index = Random.Range(0,StartCells.Count);
            var cell = StartCells[index];
            StartCells.RemoveAt(index);
            var x = cell.X;
            var y = cell.Y;

            // すでに壁の場合は何もしない
            if (this.AutoWallPos[x][y].tag == "Path")
            {
                // 拡張中の壁情報を初期化
                CurrentWallCells.Clear();
                ExtendWall(x, y);
            }
        }
        return this.AutoWallPos;
    }

    // 指定座標から壁を生成拡張する
    private void ExtendWall(int x, int y)
    {
        // 伸ばすことができる方向(1マス先が通路で2マス先まで範囲内)
        // 2マス先が壁で自分自身の場合、伸ばせない
        var directions = new List<Direction>();

        if (this.AutoWallPos[x][y - 1].tag == "Path" && !IsCurrentWall(x, y - 2))
            directions.Add(Direction.Up);
        if (this.AutoWallPos[x + 1][y].tag == "Path" && !IsCurrentWall(x + 2, y))
            directions.Add(Direction.Right);
        if (this.AutoWallPos[x][y + 1].tag == "Path" && !IsCurrentWall(x, y + 2))
            directions.Add(Direction.Down);
        if (this.AutoWallPos[x - 1][y].tag == "Path" && !IsCurrentWall(x - 2, y))
            directions.Add(Direction.Left);

        // ランダムに伸ばす(2マス)
        if (directions.Count > 0)
        {
            // 壁を作成(この地点から壁を伸ばす)
            SetWall(x, y);

            // 伸ばす先が通路の場合は拡張を続ける
            var isPath = false;
            var dirIndex = Random.Range(0,directions.Count);
            switch (directions[dirIndex])
            {
                case Direction.Up:
                    isPath = (this.AutoWallPos[x][y - 2].tag == "Path");
                    SetWall(x, --y);
                    SetWall(x, --y);
                    break;
                case Direction.Right:
                    isPath = (this.AutoWallPos[x + 2][y].tag == "Path");
                    SetWall(++x, y);
                    SetWall(++x, y);
                    break;
                case Direction.Down:
                    isPath = (this.AutoWallPos[x][y + 2].tag == "Path");
                    SetWall(x, ++y);
                    SetWall(x, ++y);
                    break;
                case Direction.Left:
                    isPath = (this.AutoWallPos[x - 2][y].tag == "Path");
                    SetWall(--x, y);
                    SetWall(--x, y);
                    break;
            }
            if (isPath)
            {
                // 既存の壁に接続できていない場合は拡張続行
                ExtendWall(x, y);
            }
        }
        else
        {
            // すべて現在拡張中の壁にぶつかる場合、バックして再開
            var beforeCell = CurrentWallCells.Pop();
            ExtendWall(beforeCell.X, beforeCell.Y);
        }
    }

    // 壁を拡張する
    private void SetWall(int x, int y)
    {
        this.AutoWallPos[x][y].tag = "Wall";
        if (x % 2 == 0 && y % 2 == 0)
        {
            CurrentWallCells.Push(new Cell(x, y));
        }

        Instantiate(AutoWallPrefab, AutoWallPos[x][y].transform);

    }

    // 拡張中の座標かどうか判定
    private bool IsCurrentWall(int x, int y)
    {
        return CurrentWallCells.Contains(new Cell(x, y));
    }

    private void ChangeWall()
    {
        for(int l = 1; l <= 19; l++)
        {
            for (int p = 1; p <= 19; p++)
            {
                if (AutoWallPos[l][p - 1].tag == "Path" && AutoWallPos[l][p + 1].tag == "Path")
                {
                    int r = Random.Range(1, 11);
                    if (r < 2)
                    {
                        Destroy(AutoWallPos[l][p].gameObject);
                        Instantiate(Door_Horizontal, AutoWallPos[l][p].transform.position, Quaternion.identity);
                        AutoWallPos[l][p].tag = "RoopHole";
                    }

                }
                else if (AutoWallPos[l - 1][p].tag == "Path" && AutoWallPos[l + 1][p].tag == "Path")
                {
                    int r = Random.Range(1, 11);
                    if (r < 2)
                    {
                        Destroy(AutoWallPos[l][p].gameObject);
                        Instantiate(Door_Vertical, AutoWallPos[l][p].transform.position, Quaternion.identity);
                        AutoWallPos[l][p].tag = "RoopHole";
                    }

                }
            }
        }
        
        
    }
    

    // Start is called before the first frame update
    void Awake()
    {

        WallPos.x = -10;
        WallPos.y = 0;
        WallPos.z = 10;

        // 先頭から順番に配列に格納していく
        for (int n = 0; n <= 20; n++)
        {
            
            AutoWallPos[n] = new GameObject[21];
            
            for (int i = 0; i <= 20; i++)
            {
                if(i == 0)
                {
                    WallPos.x = -10;
                }
                else
                {
                    WallPos.x++;
                }
                
                WallPos.y = 0.5f;

                AutoWallPos[n][i] = Instantiate(AutoWalls, WallPos, Quaternion.identity);

            }

            WallPos.z--;

        }

        
        CreateMaze();

        ChangeWall();

        navMeshSurface = transform.GetComponent<NavMeshSurface>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
