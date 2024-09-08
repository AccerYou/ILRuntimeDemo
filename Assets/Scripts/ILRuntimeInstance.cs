using ILRuntime.CLR.Method;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Networking;

public class ILRuntimeInstance : MonoBehaviour
{
    private static ILRuntimeInstance instance;
    private static object _lock = new object();
    private Dictionary<string, System.IO.MemoryStream> dllMemorStrema = new Dictionary<string, System.IO.MemoryStream>();
    public static ILRuntimeInstance Instance 
    {
        get {
            if (instance == null)
            {
                lock (_lock)
                {
                    instance = FindObjectOfType(typeof(ILRuntimeInstance)) as ILRuntimeInstance;
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(ILRuntimeInstance).Name;
                        instance = (ILRuntimeInstance)obj.AddComponent(typeof(ILRuntimeInstance));
                        instance.Init();
                    }
                }
            }
            return instance;
        }
        private set 
        {
            instance = value;
        }
    }

    public ILRuntime.Runtime.Enviorment.AppDomain appdomain;

    private void Init()
    {
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        appdomain.DebugService.StartDebugService();

        //ע��������
        RegisterMethodAdapter();
    }


    unsafe void RegisterMethodAdapter() 
    {

    }


    /// <summary>
    /// ���ô˺�����ֱ�ӵ���dll�ᵼ�£�dllδ������ɺ����ѵ���
    /// </summary>
    /// <param name="dllName"></param>
    public void LoadDll(string dllName) 
    {
        if (string.IsNullOrEmpty(dllName)) 
        {
            Debug.LogError($"dll Name is Empty!!!!");
            return;
        }
        StartCoroutine(loadDll(dllName));
    }


    private IEnumerator loadDll(string dllName)
    {
        if (dllMemorStrema.ContainsKey(dllName))
        {
            Debug.LogWarning($"{dllName} is loaded");
            yield break;
        }


        UnityWebRequest request = UnityWebRequest.Get($"file:///{Application.streamingAssetsPath}/{dllName}.dll");
        
        request.SendWebRequest();
        
        while (!request.isDone)
        {
            Debug.LogWarning($"load {dllName}.dll Progress:{(request.downloadProgress * 100).ToString("0.00")}%");
            yield return null;
        }
        
        Debug.LogWarning($"load {dllName}.dll Progress:{(request.downloadProgress * 100).ToString("0.00")}%");
        
        if (request.result != UnityWebRequest.Result.Success) 
        {
            Debug.LogError($"load {dllName}.dll filed!!!!, error info :{request.error}");
            yield break;
        }

        dllMemorStrema.Add(dllName, new System.IO.MemoryStream(request.downloadHandler.data));

        appdomain.LoadAssembly(dllMemorStrema[dllName], null, null); // ���� DLL
        
        request.Dispose();
    
    }

    private void OnDisable()
    {
        /*
         �������Ѽ��ص�dll�ļ������ͷŲ���
         */
        if (dllMemorStrema != null && dllMemorStrema.Count > 0) 
        {
            foreach (var item in dllMemorStrema) 
            {
                item.Value.Dispose();
            }
            dllMemorStrema.Clear();
        }

        appdomain.Dispose();

    }
}
