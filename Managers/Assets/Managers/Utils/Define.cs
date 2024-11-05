public class Define
{
    public enum EEventType
    {

    }

    public enum EScene
    {
        Unknown,
        TitleScene,
        LoadingScene,
        GameScene,
    }
    public enum ESound
    {
        Bgm,
        SubBgm,
        Effect,
        Max,
    }

    public enum ETouchEvent
    {
        PointerUp,
        PointerDown,
        Click,
        LongPressed,
        BeginDrag,
        Drag,
        EndDrag,
        Enter,
    }

    public enum ELanguage
    {
        Korean,
        English,
        French,
        SimplifiedChinese,
        TraditionalChinese,
        Japanese,
    }

    public enum EErrorMessage
    {
        Level,
        Class,
        InventoryFull,
        Etc,
    }

}

public static class SortingLayers
{
	public const int HERO = 300;
}

public static class AnimName
{
	public const string IDLE = "idle";
}