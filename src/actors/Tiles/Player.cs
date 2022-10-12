enum Animation : int
{
    NORMAL = 0,
    IDLE1 = 1,
    IDLE2 = 2,
    IDLE3 = 3,
    WALK_LEFT = 4,
    WALK_RIGHT = 5,
}

class Player : Tile
{
    private int walkCD = 0;
    const int TICKS_PER_TILE = 10;
    private double i, j;
    private int tileXIdle;
    private int tileXWalk;
    private bool isMoving = false;
    private bool direction = true;
    private int idleCD = 512;
    private TileSet tileSet;

    public Player(TileSet tileSet, int srcX, int srcY) : base(tileSet, srcX, srcY)
    {
        this.tileSet = tileSet;
    }

    public override void Draw(Context context, int x, int y)
    {
        var animation = Animation.NORMAL;
        var tileX = 0;
        if (isMoving)
        {
            animation = direction ? Animation.WALK_RIGHT : Animation.WALK_LEFT;
            tileX = tileXWalk;
        }
        else
        {
            if (idleCD < 0)
            {
                animation = Animation.IDLE1;
                tileX = tileXIdle;
            }
            if (idleCD < -256)
            {
                animation = Animation.IDLE2;
            }
            if (idleCD < -512)
            {
                animation = Animation.IDLE3;
            }
        }
        context.renderer.DrawTileSet(tileSet, x, y, tileX, (int)animation);
    }

    public override void Update(Context context, Map map, int x, int y)
    {
        ++i;
        i = i % 64;
        tileXIdle = (int)(i * (8.0 / 64.0));
        ++j;
        j = j % 32;
        tileXWalk = (int)(j * (8.0 / 32.0));
        if (GameState.keyState.isPressed(Control.UP) && walkCD is 0)
        {
            if (!map.IsSolid(x, y - 1) || map.IsSemiSolid(x, y - 1))
            {
                map.SetTile(x, y - 1, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PER_TILE;
        }
        if (GameState.keyState.isPressed(Control.DOWN) && walkCD is 0)
        {
            if (!map.IsSolid(x, y + 1) || map.IsSemiSolid(x, y + 1))
            {
                map.SetTile(x, y + 1, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PER_TILE;
        }
        if (GameState.keyState.isPressed(Control.LEFT) && walkCD is 0)
        {
            if (!map.IsSolid(x - 1, y) || map.IsSemiSolid(x - 1, y))
            {
                map.SetTile(x - 1, y, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PER_TILE;
        }
        if (GameState.keyState.isPressed(Control.RIGHT) && walkCD is 0)
        {
            if (!map.IsSolid(x + 1, y) || map.IsSemiSolid(x + 1, y))
            {
                map.SetTile(x + 1, y, 9);
                map.SetTile(x, y, 0);
            }
            walkCD = TICKS_PER_TILE;
        }
        if (GameState.keyState.isPressed(Control.LEFT) || GameState.keyState.isPressed(Control.RIGHT))
        {
            direction = GameState.keyState.isPressed(Control.RIGHT);
        }
        if (GameState.keyState.isPressed(Control.LEFT) || GameState.keyState.isPressed(Control.RIGHT) || GameState.keyState.isPressed(Control.UP) || GameState.keyState.isPressed(Control.DOWN))
        {
            isMoving = true;
            idleCD = 512;
        }
        else
        {
            isMoving = false;
            idleCD--;
        }
        walkCD = Math.Max(0, walkCD - 1);
        base.Update(context, map, x, y);
    }
}
