// using Newtonsoft.Json;
using System.Text.Json;

class Map : Actor
{
    private int[][] map;
    private Dictionary<int, Tile> tiles;

    public Map(uint width, uint height, Dictionary<int, Tile> tiles)
    {
        this.map = new int[height][];
        for (uint i = 0; i < height; i++)
        {
            this.map[i] = new int[width];
        }
        this.tiles = tiles;
    }

    public Map(string json, Dictionary<int, Tile> tiles)
    {
        var map = JsonSerializer.Deserialize<int[][]>(json);

        if (map == null)
        {
            throw new Exception("Invalid map");
        }
        this.map = map;
        this.tiles = tiles;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this.map);
    }

    public void Dump()
    {
        for (uint i = 0; i < this.map.Length; i++)
        {
            var row = String.Join("", this.map[i]);
            Console.WriteLine(row);
        }
    }

    public override void Draw(Context context)
    {
        for (int y = 0; y < this.map.Length; y++)
        {
            for (int x = 0; x < this.map[y].Length; x++)
            {
                if (this.tiles.ContainsKey(this.map[y][x]))
                {
                    var tile = this.tiles[this.map[y][x]];
                    tile.Draw(context, x * 32, y * 32);
                }
            }
        }
    }

    public override void Update(Context context)
    {
        for (int y = 0; y < this.map.Length; y++)
        {
            for (int x = 0; x < this.map[y].Length; x++)
            {
                if (this.tiles.ContainsKey(this.map[y][x]))
                {
                    var tile = this.tiles[this.map[y][x]];
                    tile.Update(context, this, x, y);
                }
            }
        }
    }

    public bool IsSolid(int x, int y)
    {
        if (this.tiles.ContainsKey(this.map[y][x]))
        {
            var tile = this.tiles[this.map[y][x]];
            return tile.IsSolid();
        }
        return false;
    }

    public bool IsSemiSolid(int x, int y)
    {
        if (this.tiles.ContainsKey(this.map[y][x]))
        {
            var tile = this.tiles[this.map[y][x]];
            return tile.IsSemiSolid();
        }
        return false;
    }

    public void SetTile(int x, int y, int tile)
    {
        if (this.map.Length <= y)
        {
            // expand map
            var newMap = new int[y + 1][];
            for (int i = 0; i < y + 1; i++)
            {
                newMap[i] = new int[this.map[0].Length];
            }
            for (int i = 0; i < this.map.Length; i++)
            {
                for (int j = 0; j < this.map[i].Length; j++)
                {
                    newMap[i][j] = this.map[i][j];
                }
            }
            this.map = newMap;
        }
        if (this.map[y].Length <= x)
        {
            // expand map
            var newMap = new int[this.map.Length][];
            for (int i = 0; i < this.map.Length; i++)
            {
                newMap[i] = new int[x + 1];
            }
            for (int i = 0; i < this.map.Length; i++)
            {
                for (int j = 0; j < this.map[i].Length; j++)
                {
                    newMap[i][j] = this.map[i][j];
                }
            }
            this.map = newMap;
        }
        this.map[y][x] = tile;
    }

    public int GetTile(int x, int y)
    {
        return this.map[y][x];
    }
}
