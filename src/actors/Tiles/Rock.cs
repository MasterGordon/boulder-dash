class Rock : Tile
{
    public Rock(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        if (map.GetTile(x, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, 0);
            map.SetTile(x, y + 1, (int)TileType.ROCK_FALLING);
        }
        if (map.GetTile(x, y + 1) is (int)TileType.ROCK && map.GetTile(x + 1, y) is (int)TileType.AIR && map.GetTile(x + 1, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, 0);
            map.SetTile(x + 1, y, (int)TileType.ROCK_FALLING);
        }
        if (map.GetTile(x, y + 1) is (int)TileType.ROCK && map.GetTile(x - 1, y) is (int)TileType.AIR && map.GetTile(x - 1, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, 0);
            map.SetTile(x - 1, y, (int)TileType.ROCK_FALLING);
        }
    }
}
