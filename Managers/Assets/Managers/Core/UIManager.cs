using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UIManager
{
    private int _popupOrder = 100;
    private int _toastOrder = 500;

    private List<UI_Popup> _popupList = new List<UI_Popup>();
    private UI_Scene _sceneUI = null;


    public UI_Scene SceneUI
    {
        set { _sceneUI = value; }
        get { return _sceneUI; }
    }

    private Dictionary<string, UI_Popup> _popups = new Dictionary<string, UI_Popup>();

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void CacheAllPopups()
    {
        var list = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(UI_Popup)));

        foreach (Type type in list)
        {
            CachePopupUI(type);
        }

        CloseAllPopupUI();
    }

    public Canvas SetCanvas(GameObject go, bool sort = true, int sortOrder = 0)
    {
        Canvas canvas = Utils.GetOrAddComponent<Canvas>(go);
        if (canvas != null)
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.overrideSorting = true;
        }

        CanvasScaler cs = go.GetOrAddComponent<CanvasScaler>();
        if (cs != null)
        {
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(2640, 1080);
        }

        go.GetOrAddComponent<GraphicRaycaster>();

        if (sort)
        {
            canvas.sortingOrder = _popupOrder;
            _popupOrder++;
        }

        return canvas;
    }

    public T GetSceneUI<T>() where T : UI_Base
    {
        return _sceneUI as T;
    }

    public T MakeWorldSpaceUI<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        Canvas canvas = go.GetOrAddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.worldCamera = Camera.main;

        return Utils.GetOrAddComponent<T>(go);
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null, bool pooling = false)
        where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}", parent, pooling);
        go.transform.SetParent(parent);
        return Utils.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"{name}");
        T sceneUI = Utils.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }


    public void CachePopupUI(Type type)
    {
        string name = type.Name;

        if (_popups.TryGetValue(name, out UI_Popup popup) == false)
        {
            GameObject go = Managers.Resource.Instantiate(name, Root.transform);
            popup = go.GetComponent<UI_Popup>();
            if (popup == null)
            {
                Debug.LogError("Missing Script at Popup");
                return;
            }

            _popups[name] = popup;
        }

        _popupList.Add(popup);
    }

    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        if (_popups.TryGetValue(name, out UI_Popup popup) == false)
        {
            GameObject go = Managers.Resource.Instantiate(name);
            popup = Utils.GetOrAddComponent<T>(go);
            _popups[name] = popup;
        }

        _popupList.Add(popup);

        popup.transform.SetParent(Root.transform);
        popup.gameObject.SetActive(true);
        _popupOrder++;
        popup.UICanvas.sortingOrder = _popupOrder;

        return popup as T;
    }

    public T GetLastPopupUI<T>() where T : UI_Popup
    {
        if (_popupList.Count == 0)
            return null;

        return _popupList.Last() as T;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupList.Count == 0)
            return;

        Managers.Sound.PlayPopupClose();
        _popupList.Remove(popup);
        popup.gameObject.SetActive(false);

        // 팝업이 최상단에 있는 경우에만 Order를 감소
        if (_popupList.Count != 0 && _popupList.Last() == popup)
        {
            _popupOrder--;
        }
    }

    private void ClosePopupUI()
    {
        if (_popupList.Count == 0)
            return;

        UI_Popup popup = _popupList.Last();
        _popupList.Remove(popup);
        popup.gameObject.SetActive(false);
        _popupOrder--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupList.Count > 0)
            ClosePopupUI();
    }

    public int GetPopupCount()
    {
        return _popupList.Count;
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _popups.Clear();
        Root.gameObject.DestroyChildren();
        Time.timeScale = 1;
        _sceneUI = null;
    }

}