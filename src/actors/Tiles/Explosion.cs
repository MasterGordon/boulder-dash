class Explosion : Tile
{
    private int i = 32 * 9;
    public Explosion(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        i--;
        if (i <= 0)
        {
            map.SetTile(x, y, 0);
        }
    }
}
