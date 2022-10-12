class RockFalling : Tile
{
    public RockFalling(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        if (GameState.tick % 16 == 0)
            if (map.GetTile(x, y + 1) is (int)TileType.AIR)
            {
                map.SetTile(x, y, 0);
                map.SetTile(x, y + 1, (int)TileType.ROCK_FALLING);
            }
            else if (map.GetTile(x, y + 1) is (int)TileType.PLAYER)
            {
                y += 1;
                map.SetTile(x + 1, y, (int)TileType.EXPLOSION);
                map.SetTile(x, y, (int)TileType.EXPLOSION);
                map.SetTile(x - 1, y, (int)TileType.EXPLOSION);
                map.SetTile(x + 1, y + 1, (int)TileType.EXPLOSION);
                map.SetTile(x, y + 1, (int)TileType.EXPLOSION);
                map.SetTile(x - 1, y + 1, (int)TileType.EXPLOSION);
                map.SetTile(x + 1, y - 1, (int)TileType.EXPLOSION);
                map.SetTile(x, y - 1, (int)TileType.EXPLOSION);
                map.SetTile(x - 1, y - 1, (int)TileType.EXPLOSION);
            }
            else
            {
                map.SetTile(x, y, (int)TileType.ROCK);
            }
    }
}
