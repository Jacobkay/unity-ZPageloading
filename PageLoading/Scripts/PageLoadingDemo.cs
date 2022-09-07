using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZTools;
/// <summary>
/// ʹ��ʾ��
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
    /// ����item��ֵ
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
    /// <param name="crtIndexPage">��ǰҳ��</param>
    /// <param name="pageItemNum">�������������</param>
    /// <returns></returns>
    private PageLoadingData PageLoadingView_GetPageDataEvent(int crtIndexPage, int pageItemNum)
    {
        // �˴�Ϊչʾ����ʹ�ã��õ���ǰҳ�루crtIndexPage����������������ݣ�pageItemNum���󼴿�����ӿ�
        Debug.Log("getdata");
        //��Ҫ����һ������objList:��ǰҳ����Ҫ��ʾ�Ķ����б�totalDataAmount��һ���ж���������
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
