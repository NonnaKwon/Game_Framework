
using UnityEngine;

public class Managers : MonoBehaviour
{
	public static bool Initialized { get; set; }

	private static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

	#region Contents

	private GameManager _game = new GameManager();
	private EventManager _event = new EventManager();
    public static GameManager Game { get { return Instance?._game; } }
    public static EventManager Event { get { return Instance?._event; } }
    #endregion

    #region Core

    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private SoundManager _sound = new SoundManager();
    private UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static UIManager UI { get { return Instance?._ui; } }

    #endregion

    #region Language
    private static Define.ELanguage _language = Define.ELanguage.Korean;
    public static Define.ELanguage Language
    {
        get { return _language; }
        set
        {
            _language = value;
        }
    }

    public static string GetText(string textId)
    {
        switch (_language)
        {
            case Define.ELanguage.Korean:
                break;
            case Define.ELanguage.English:
                break;
            case Define.ELanguage.French:
                break;
            case Define.ELanguage.SimplifiedChinese:
                break;
            case Define.ELanguage.TraditionalChinese:
                break;
            case Define.ELanguage.Japanese:
                break;
        }

        return textId;
    }
    
    public static string GetErrorMsg(Define.EErrorMessage msg)
    {
        switch (msg)
        {
            case Define.EErrorMessage.Level:
                return "TODO 감히 사용 할 수 없습니다.";
            case Define.EErrorMessage.Class:
                return "TODO 현재 클래스에서 사용할 수 없습니다.";
            case Define.EErrorMessage.InventoryFull:
                return "TODO 가방이 가득 찼습니다..";
            case Define.EErrorMessage.Etc:
                return "사용할 수 없습니다.";
        }

        return "";
    }

    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

			GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
            s_instance._sound.Init();
        }		
	}

	public static void Clear()
    {
        Sound.Clear();
        //Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
