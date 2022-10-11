class Player : Tile
{
    private int walkCD = 0;
    const int TICKS_PAR_TILE = 10;

    public Player(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
    }

    public override void Draw(Context context, int x, int y)
    {
        base.Draw(context, x, y);
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        if (GameState.keyState.isPressed(Control.UP) && walkCD is 0)
        {
            if (!map.IsSolid(x, y - 1) || map.IsSemiSolid(x, y - 1))
            {
                map.SetTile(x, y - 1, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PAR_TILE;
        }
        if (GameState.keyState.isPressed(Control.DOWN) && walkCD is 0)
        {
            if (!map.IsSolid(x, y + 1) || map.IsSemiSolid(x, y + 1))
            {
                map.SetTile(x, y + 1, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PAR_TILE;
        }
        if (GameState.keyState.isPressed(Control.LEFT) && walkCD is 0)
        {
            if (!map.IsSolid(x - 1, y) || map.IsSemiSolid(x - 1, y))
            {
                map.SetTile(x - 1, y, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PAR_TILE;
        }
        if (GameState.keyState.isPressed(Control.RIGHT) && walkCD is 0)
        {
            if (!map.IsSolid(x + 1, y) || map.IsSemiSolid(x + 1, y))
            {
                map.SetTile(x + 1, y, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PAR_TILE;
        }
        walkCD = Math.Max(0, walkCD - 1);
        base.Update(context, map, x, y);
    }
}
