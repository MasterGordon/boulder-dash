using Newtonsoft.Json;

class Map : Actor
{
    private int[][] map;

    public Map(uint width, uint height)
    {
        this.map = new int[height][];
        for (uint i = 0; i < height; i++)
        {
            this.map[i] = new int[width];
        }
    }

    public Map(string json)
    {
        var map = JsonConvert.DeserializeObject<int[][]>(json);
        if (map == null)
        {
            throw new Exception("Invalid map");
        }
        this.map = map;
    }
}
