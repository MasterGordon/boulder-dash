class Diamond : Tile
{
    private TileSet tileSet;
    private int tileX;

    public Diamond(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
        this.tileSet = tileSet;
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        if (map.GetTile(x, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, (int)TileType.DIAMOND_FALLING);
        }
        if (map.GetTile(x, y + 1) is (int)TileType.ROCK && map.GetTile(x + 1, y) is (int)TileType.AIR && map.GetTile(x + 1, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, 0);
            map.SetTile(x + 1, y, (int)TileType.DIAMOND_FALLING);
        }
        if (map.GetTile(x, y + 1) is (int)TileType.ROCK && map.GetTile(x - 1, y) is (int)TileType.AIR && map.GetTile(x - 1, y + 1) is (int)TileType.AIR)
        {
            map.SetTile(x, y, 0);
            map.SetTile(x - 1, y, (int)TileType.DIAMOND_FALLING);
        }
        var i = GameState.tick % 32;
        tileX = (int)(i * (8.0 / 32.0));
    }

    public override void Draw(Context context, int x, int y)
    {
        context.renderer.DrawTileSet(tileSet, x, y, tileX, 10);
    }

    public override bool IsSemiSolid()
    {
        return true;
    }
}
