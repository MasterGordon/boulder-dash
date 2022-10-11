class Diamond : Rock
{
    private TileSet tileSet;
    private double i;
    private int tileX;

    public Diamond(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
        this.tileSet = tileSet;
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        ++i;
        i = i % 128;
        tileX = (int)(i * (8.0 / 128.0));
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
