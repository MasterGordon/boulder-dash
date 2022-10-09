struct Context
{
    public Context(Window window, Renderer renderer)
    {
        this.window = window;
        this.renderer = renderer;
        this.resourceLoader = new ResourceLoader();
        this.fontManager = new FontManager(resourceLoader);
    }

    public Window window { get; }
    public Renderer renderer { get; }
    public ResourceLoader resourceLoader { get; }
    public FontManager fontManager { get; }
}
