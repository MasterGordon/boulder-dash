class Tile
{
    private TileSet tileSet;
    private int srcX, srcY;

    public Tile(TileSet tileSet, int srcX, int srcY)
    {
        this.tileSet = tileSet;
        this.srcX = srcX;
        this.srcY = srcY;
    }

    public virtual void Draw(Context context, int x, int y)
    {
        context.renderer.DrawTileSet(tileSet, x, y, srcX, srcY);
    }

    public virtual void Update(Context context, Map map, int x, int y)
    {
    }

    public virtual bool IsSolid()
    {
        return true;
    }

    public virtual bool IsSemiSolid()
    {
        return false;
    }
}
