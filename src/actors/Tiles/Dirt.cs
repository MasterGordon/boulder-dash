class Dirt : Tile
{
    public Dirt(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
    }

    public override bool IsSemiSolid()
    {
        return true;
    }
}
