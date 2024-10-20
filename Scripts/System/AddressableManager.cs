using JetBrains.Annotations;
using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;
using Debug = ccm.Debug;

public class AddressableManager : Singleton<AddressableManager>
{
    private bool isCompleteLoad = false;
    private Dictionary<string, SpriteAtlas> atlasDic = new Dictionary<string, SpriteAtlas>();
    private Dictionary<string, TextAsset> csvDic = new Dictionary<string, TextAsset>();
    private Dictionary<string, GameObject> prefabDic = new Dictionary<string, GameObject>();
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();

    List<string> labels = new List<string>
        {
            Constants.Addressable.LABEL_MATERIAL,
            Constants.Addressable.LABEL_ATLAS,
            Constants.Addressable.LABEL_AUDIO,
            Constants.Addressable.LABEL_PREFAB,
            Constants.Addressable.LABEL_CSV
        };

    private readonly float KB_1 = 1024;

    public bool IsCompleteLoad { get => isCompleteLoad; }

    private async Task LoadAssetsAsync<T>(string label, Dictionary<string, T> dictionary, bool autoRelease = false) where T : UnityEngine.Object
    {
        var operation = Addressables.LoadAssetsAsync<T>(label, null);
        await operation.Task;
        foreach (var asset in operation.Result)
        {
            dictionary[asset.name] = asset;
        }
    }

    private async Task LoadAllAssetsAsync()
    {
        var tasks = new List<Task>
        {
            LoadAssetsAsync<SpriteAtlas>(Constants.Addressable.LABEL_ATLAS, atlasDic),
            LoadAssetsAsync<TextAsset>(Constants.Addressable.LABEL_CSV, csvDic),
            LoadAssetsAsync<GameObject>(Constants.Addressable.LABEL_PREFAB, prefabDic),
            LoadAssetsAsync<AudioClip>(Constants.Addressable.LABEL_AUDIO, audioDic)
        };

        await Task.WhenAll(tasks);
    }

    private IEnumerator InitAddressable()
    {
        var init = Addressables.InitializeAsync();
        yield return init;
        yield return CheckedUpdate();        
    }

    private IEnumerator CheckedUpdate()
    {
        var result = Addressables.GetDownloadSizeAsync(labels);
        yield return result;

        if (result.Result > 0)
        {
            float size = (float)result.Result / (KB_1 * KB_1);
            PopupManager.Instance.Show<UI_Popup_Update>();
            PopupManager.Instance.Get<UI_Popup_Update>().ShowDownloadSize(size);
            yield break;
        }

        yield return Load();
    }

    private IEnumerator Patch()
    {
        var result = Addressables.DownloadDependenciesAsync(labels, Addressables.MergeMode.Union);
        float progress = 0;

        UI_Popup_Update popup = PopupManager.Instance.Get<UI_Popup_Update>();

        while (result.Status == AsyncOperationStatus.None)
        {
            progress = result.GetDownloadStatus().Percent * 100;
            popup.UpdatePercentage(progress);

            yield return null;
        }

        result.Release();
        PopupManager.Instance.Hide<UI_Popup_Update>();
        yield return Load();
    }

    private IEnumerator Load()
    {        
        var loadTask = LoadAllAssetsAsync();
        yield return new WaitUntil(() => loadTask.IsCompleted);
        isCompleteLoad = true;
    }

    private void Debug(string assetName)
    {
        ccm.Debug.Log($"{assetName}에셋은 존재하지 않습니다.");
    }

    public void Init(bool onlyLoad)
    {
        if (onlyLoad)
        {
            StartCoroutine(Load());
            return;
        }

        StartCoroutine(InitAddressable());
    }

    public void ProcessPatch()
    {
        StartCoroutine(Patch());
    }

    public Sprite GetSprite(string atlasName, string spriteName)
    {
        if (!atlasDic.TryGetValue(atlasName, out SpriteAtlas atlas))
        {
            Debug(atlasName);
            return null;
        }

        return atlas.GetSprite(spriteName);
    }

    public TextAsset GetCsv(string csvName)
    {
        if (!csvDic.TryGetValue(csvName, out TextAsset csv))
        {
            Debug(csvName);
            return null;
        }

        return csv;
    }

    public GameObject GetPrefab(string name)
    {
        if (!prefabDic.TryGetValue(name, out GameObject prefab))
        {
            Debug(name); 
            return null;
        }

        return prefab;
    }

    public AudioClip GetAudioClip(string name)
    {
        if (!audioDic.TryGetValue(name, out AudioClip audioClip))
        {
            Debug(name); 
            return null;
        }

        return audioClip;
    }

}
