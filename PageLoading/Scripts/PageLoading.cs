using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZTools
{
    /// <summary>
    /// 返回的对象
    /// </summary>
    public struct PageLoadingData
    {
        public List<object> objList;
        public int totalDataAmount;
    }
    [RequireComponent(typeof(PageLoadingModel))]
    public class PageLoading : MonoBehaviour
    {
        /// <summary>
        /// 通过网络请求获取当前页面数据
        /// 参数：（当前页码，当前页请求数量）
        /// 返回值：（当前页数据对象list，总共有多少条数据）;
        /// </summary>
        public event Func<int, int, PageLoadingData> GetPageDataEvent;
        /// <summary>
        /// 设置显示每一条数据
        /// 参数：（要显示的内容对象，显示数据，当前是第几条内容）;
        /// </summary>
        public event Action<Transform, object, int> SetPageItemEvent;
        /// <summary>
        /// 可以监听到选择的页码
        /// </summary>
        public event Action<int> PageNumEvent;
        /// <summary>
        /// 手动获取当前选择那页
        /// </summary>
        public int CrtPageNum { get; set; }
        PageLoadingController pageLoadingController;
        PageLoadingModel pageLoadingModel;
        private void Start()
        {
            pageLoadingModel = this.GetComponent<PageLoadingModel>();
            pageLoadingController = new PageLoadingController
            {
                pageLoadingModel = this.pageLoadingModel,
                pageLoadingView = this
            };
            pageLoadingModel.pageLoadingController = pageLoadingController;
            if (pageLoadingModel.awake2Init)
            {
                pageLoadingController.Init();
            }
            pageLoadingModel.Init();
        }
        /// <summary>
        /// 初始化分页加载
        /// </summary>
        public void Init()
        {
            pageLoadingController.Init();
        }
        /// <summary>
        /// 跳到对应的页面
        /// </summary>
        public void Jump2Page(int page)
        {
            pageLoadingController.Click2Page(page);
        }
        /// <summary>
        /// 调用接口获取要更新的数据
        /// </summary>
        /// <param name="indexPage">当前页码</param>
        /// <param name="pageNum">当前页请求数量</param>
        /// <param name="fun">回调函数（当前页数据对象list，总共有多少条数据）</param>
        [Obsolete("事件触发器，不可使用，请使用GetPageDataEvent获取接口内容")]
        public void GetData(int indexPage, int pageNum, Action<List<object>, int> fun)
        {
            if (null != GetPageDataEvent)
            {
                PageLoadingData data = GetPageDataEvent.Invoke(indexPage, pageNum);
                fun(data.objList, data.totalDataAmount);
            }
        }
        /// <summary>
        /// 根据获得的对象设置应该显示的内容
        /// </summary>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <param name="i"></param>
        [Obsolete("事件触发器，不可使用，请使用SetPageItemEvent设置对象显示内容")]
        public void SetItem(Transform item, object data, int i)
        {
            if (null != SetPageItemEvent)
            {
                SetPageItemEvent(item, data, i);
            }
        }
        /// <summary>
        /// 改变选择的页码
        /// </summary>
        [Obsolete("事件触发器，不可使用，请使用PageNumEvent获取页码")]
        public void ChangePageNum(int page)
        {
            CrtPageNum = page;
            if (null != PageNumEvent)
            {
                PageNumEvent.Invoke(page);
            }
        }
        private void OnDestroy()
        {
            pageLoadingController = null;
            GC.Collect();
        }
    }
}

