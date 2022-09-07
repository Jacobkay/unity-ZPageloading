using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZTools;
/// <summary>
/// 使用示例
/// </summary>
public class PageLoadingDemo : MonoBehaviour
{
    public PageLoading pageLoadingView;
    void Awake()
    {
        pageLoadingView.GetPageDataEvent += PageLoadingView_GetPageDataEvent;
        pageLoadingView.SetPageItemEvent += PageLoadingView_SetPageItemEvent;
        //pageLoadingView.Init();
        //pageLoadingView.Jump2Page(2);
    }
    /// <summary>
    /// 设置item的值
    /// </summary>
    /// <param name="trm"></param>
    /// <param name="data"></param>
    /// <param name="crtIndex"></param>
    private void PageLoadingView_SetPageItemEvent(Transform trm, object data, int crtIndex)
    {
        Debug.Log("SetData");
        trm.Find("Text").GetComponent<Text>().text = data.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="crtIndexPage">当前页码</param>
    /// <param name="pageItemNum">请求多少条数据</param>
    /// <returns></returns>
    private PageLoadingData PageLoadingView_GetPageDataEvent(int crtIndexPage, int pageItemNum)
    {
        // 此处为展示怎样使用，拿到当前页码（crtIndexPage）和请求多少条数据（pageItemNum）后即可请求接口
        Debug.Log("getdata");
        //需要返回一个对象，objList:当前页面需要显示的对象列表，totalDataAmount：一共有多少条数据
        PageLoadingData data = new PageLoadingData
        {
            objList = new List<object>() { 1, 2, 3, 4, 54, 56 },
            totalDataAmount = 50
        };
        return data;
    }
    private void OnDestroy()
    {
        pageLoadingView.GetPageDataEvent -= PageLoadingView_GetPageDataEvent;
        pageLoadingView.SetPageItemEvent -= PageLoadingView_SetPageItemEvent;
    }
}
