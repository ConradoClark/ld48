using UnityEngine;
using Licht.Impl.Orchestration;
using Licht.Impl.Time;
using UnityEngine.InputSystem;

[AddComponentMenu("Framework/Toolbox Behaviour")]
[DisallowMultipleComponent]
public class Toolbox : Singleton<Toolbox>
{
    public static BasicMachinery MainMachinery;
    public static DefaultTimer MainTimer;
    public static PlayerInput MainInput;
    public static Camera MainCamera;
    public static ResourceManager ResourceManager;
    public static UnitManager UnitManager;

    public PlayerInput PlayerInput;
    public Camera PlayerCamera;

    protected Toolbox() { }

    void Awake()
    {
        MainMachinery = new BasicMachinery();
        MainTimer = new DefaultTimer(() => Time.deltaTime * 1000f);
        MainInput = PlayerInput;
        MainCamera = PlayerCamera;
        ResourceManager = RegisterComponent<ResourceManager>();
        UnitManager = RegisterComponent<UnitManager>();
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