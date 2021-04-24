using UnityEngine;
using Licht.Impl.Orchestration;
using Licht.Impl.Time;

[AddComponentMenu("Framework/Toolbox Behaviour")]
[DisallowMultipleComponent]
public class Toolbox : Singleton<Toolbox>
{
    public static BasicMachinery MainMachinery;
    public static DefaultTimer MainTimer;

    protected Toolbox() { }

    void Awake()
    {
        MainMachinery = new BasicMachinery();
        MainTimer = new DefaultTimer(() => Time.deltaTime * 1000f);
        // UIManager = RegisterComponent<UIManager>();
        // RTSManager = RegisterComponent<RTSManager>();
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