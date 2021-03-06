using Licht.Impl.Debug;
using UnityEngine;
using Licht.Impl.Orchestration;
using Licht.Impl.Time;
using UnityEngine.InputSystem;

[AddComponentMenu("Framework/Toolbox Behaviour")]
[DisallowMultipleComponent]
public class Toolbox : Singleton<Toolbox>
{
    public BasicMachinery MainMachinery;
    public DefaultTimer MainTimer;
    public ResourceManager ResourceManager;
    public UnitManager UnitManager { get; private set; }
    public CommandManager CommandManager { get; private set; }
    public UIManager UIManager;
    public PlayerInput MainInput;
    public Camera MainCamera;

    protected Toolbox() { }

    void OnEnable()
    {
        DebugLicht.Write = Debug.Log;
        MainMachinery = new BasicMachinery();
        MainTimer = new DefaultTimer(() => Time.deltaTime * 1000f);
        MainInput ??= RegisterComponent<PlayerInput>();
        MainCamera ??= RegisterComponent<Camera>();
        ResourceManager ??= RegisterComponent<ResourceManager>();
        UnitManager ??= RegisterComponent<UnitManager>();
        CommandManager ??= RegisterComponent<CommandManager>();
        UIManager ??= RegisterComponent<UIManager>(); 
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update() 
    {
        MainMachinery.Update();
        MainTimer.Update();
    }

    // (optional) allow runtime registration of global objects
    public static T RegisterComponent<T>() where T : Component
    {
        return Instance.GetOrAddComponent<T>();
    }
}