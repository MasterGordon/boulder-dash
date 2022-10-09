class SceneManager
{
    private Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
    private Scene? currentScene;

    public void AddScene(string name, Scene scene)
    {
        scenes.Add(name, scene);
    }

    public void ChangeScene(string name, Context context)
    {
        if (currentScene != null)
        {
            currentScene.Destroy(context);
        }

        currentScene = scenes[name];
        currentScene.Create(context);
    }

    public Scene? GetCurrentScene()
    {
        return currentScene;
    }
}
