using System.Collections.Generic;
using UnityEngine;

namespace ZTools
{
    public class PageLoadingController
    {
        public PageLoadingModel pageLoadingModel;
        public PageLoading pageLoadingView;
        /// <summary>
        /// 记录每一页的信息
        /// </summary>
        private Dictionary<int, List<object>> pageItemDic = new Dictionary<int, List<object>>();
        /// <summary>
        /// 存放当前按钮列表
        /// </summary>
        private List<PageLoadingPageBtn> pageBtnList = new List<PageLoadingPageBtn>();
        private int startPage;
        private int indexPage;
        private int allPage;
        private bool isLoading = false;
        private int allItemNum = 0;
        public bool isInit = false;
        int maxStartPageNum = 0;
        /// <summary>
        /// 初始化数据
        /// </summary>
        public void Init()
        {
            startPage = 1;
            indexPage = 1;
            allItemNum = 0;
            maxStartPageNum = 0;
            pageItemDic.Clear();
            pageBtnList.Clear();
            pageLoadingModel.DestroyContentChild();
            pageLoadingModel.DestroyBtnContentChild();
            allPage = 0;
            UpdateData();
        }
        /// <summary>
        /// 显示列表，每次点击都需要调用
        /// </summary>
        void UpdateData()
        {
            if (isLoading) return;
            if (!pageItemDic.ContainsKey(indexPage) || pageLoadingModel.everyPageRequest)
            {
                if (pageItemDic.ContainsKey(indexPage))
                {
                    pageItemDic[indexPage].Clear();
                    pageItemDic.Remove(indexPage);
                }
                isLoading = true;
                pageLoadingView.GetData(indexPage, pageLoadingModel.pageItemNum, (List<object> dataList, int count) =>
                {
                    AddDataList(indexPage, dataList, count);
                });
            }
            else
            {
                AddDataList(indexPage, pageItemDic[indexPage], allItemNum);
            }
        }
        /// <summary>
        /// 更新页面内容
        /// </summary>
        /// <param name="iPage">当前页面</param>
        /// <param name="pageDataList">该页显示内容列表</param>
        /// <param name="totalList">总条数</param>
        void AddDataList(int iPage, List<object> pageDataList, int totalList)
        {
            if (!pageItemDic.ContainsKey(iPage))
            {
                pageItemDic.Add(iPage, pageDataList);
            }
            // 初始化数据
            if (!isInit)
            {
                allItemNum = totalList;
                AddPageBtn();
                InitEvent();
                isInit = true;
            }
            UpdatePageBtnShowType();
            UpdatePageBtnChoiceType();
            UpdatePageItemList();
            // 加载结束
            isLoading = false;
        }
        /// <summary>
        /// 初始化底部按钮状态
        /// </summary>
        void AddPageBtn()
        {
            if (pageLoadingModel.pageItemNum != 0 && pageLoadingModel.pageBtnNum != 0)
            {
                allPage = (int)Mathf.Ceil(allItemNum / pageLoadingModel.pageItemNum);
                allPage = allPage == 0 ? 1 : allPage;
                maxStartPageNum = allPage < pageLoadingModel.pageBtnNum ? 1 : allPage - pageLoadingModel.pageBtnNum + 1;
                pageLoadingModel.AllPageTxt = allPage.ToString();
                float instAllPage = (allPage > pageLoadingModel.pageBtnNum) ? pageLoadingModel.pageBtnNum : allPage;
                for (int i = 0; i < instAllPage; i++)
                {
                    PageLoadingPageBtn pageBtn = pageLoadingModel.AddBtnObj();
                    pageBtn.prtObj = this;
                    pageBtnList.Add(pageBtn);
                }
            }
        }
        /// <summary>
        /// 初始化点击绑定事件
        /// </summary>
        void InitEvent()
        {
            if (pageLoadingModel.btnLeft != null)
                pageLoadingModel.btnLeft.onClick.AddListener(LeftClick);
            if (pageLoadingModel.btnRight != null)
                pageLoadingModel.btnRight.onClick.AddListener(RightClick);
            if (pageLoadingModel.btnPageLeft != null)
                pageLoadingModel.btnPageLeft.onClick.AddListener(BtnPageLeft);
            if (pageLoadingModel.btnPageRight != null)
                pageLoadingModel.btnPageRight.onClick.AddListener(BtnPageRight);
            if (pageLoadingModel.btnAllPage != null)
                pageLoadingModel.btnAllPage.onClick.AddListener(AllPage);
            if (pageLoadingModel.btnFirstPage != null)
                pageLoadingModel.btnFirstPage.onClick.AddListener(FirstPage);
        }
        /// <summary>
        /// 点击向左翻页按钮
        /// </summary>
        void LeftClick()
        {
            if (!isLoading)
            {
                indexPage--;
                if (indexPage < startPage)
                {
                    startPage--;
                }
                UpdateData();
            }
        }
        /// <summary>
        /// 点击向右翻页按钮
        /// </summary>
        void RightClick()
        {
            if (!isLoading)
            {
                indexPage++;
                if (indexPage > pageLoadingModel.pageBtnNum && startPage + pageLoadingModel.pageBtnNum - 1 < allPage)
                {
                    startPage++;
                }
                UpdateData();
            }
        }
        /// <summary>
        /// 点击向左大翻页按钮
        /// </summary>
        void BtnPageLeft()
        {
            if (!isLoading)
            {
                startPage -= pageLoadingModel.pageBtnNum;
                indexPage -= pageLoadingModel.pageBtnNum;
                if (startPage < 1)
                {
                    indexPage += (1 - startPage);
                    startPage = 1;
                }
                UpdateData();
            }
        }
        /// <summary>
        /// 点击向右大翻页按钮
        /// </summary>
        void BtnPageRight()
        {
            if (!isLoading)
            {
                startPage += pageLoadingModel.pageBtnNum;
                indexPage += pageLoadingModel.pageBtnNum;
                int lastPage = allPage - pageLoadingModel.pageBtnNum + 1;
                if (startPage > lastPage)
                {
                    indexPage -= (startPage - lastPage);
                    startPage = lastPage;
                }
                UpdateData();
            }
        }
        /// <summary>
        /// 点击首页
        /// </summary>
        void FirstPage()
        {
            if (!isLoading)
            {
                indexPage = 1;
                startPage = 1;
                UpdateData();
            }
        }
        /// <summary>
        /// 点击尾页
        /// </summary>
        void AllPage()
        {
            if (!isLoading)
            {
                indexPage = allPage;
                startPage = (allPage > pageLoadingModel.pageBtnNum) ? allPage - pageLoadingModel.pageBtnNum + 1 : 1;
                UpdateData();
            }
        }
        /// <summary>
        /// 翻页时，清空当前显示内容
        /// </summary>
        void UpdatePageItemList()
        {
            pageLoadingModel.DestroyContentChild();
            for (int i = 0; i < pageItemDic[indexPage].Count; i++)
            {
                Transform item = pageLoadingModel.AddItemObj();
                pageLoadingView.SetItem(item, pageItemDic[indexPage][i], i + 1);
            }
        }
        /// <summary>
        /// 更新按钮显示状态及内容
        /// </summary>
        void UpdatePageBtnShowType()
        {
            pageLoadingView.ChangePageNum(indexPage);
            if (pageBtnList[0].PageIndex != startPage)
            {
                for (int i = 0; i < pageBtnList.Count; i++)
                {
                    pageBtnList[i].PageIndex = startPage + i;
                }
            }
            if (allPage > pageLoadingModel.pageBtnNum)
            {
                pageLoadingModel.btnPageLeft.interactable = pageBtnList[0].PageIndex != 1;
                pageLoadingModel.btnPageRight.interactable = pageBtnList[pageBtnList.Count - 1].PageIndex != allPage;
            }
            else
            {
                pageLoadingModel.btnPageLeft.interactable = false;
                pageLoadingModel.btnPageRight.interactable = false;
            }
            pageLoadingModel.btnRight.interactable = (indexPage != allPage);
            pageLoadingModel.btnLeft.interactable = (indexPage != 1);
        }
        /// <summary>
        /// 更新页码按钮显示状态
        /// </summary>
        void UpdatePageBtnChoiceType()
        {
            if (pageLoadingModel.pageBtnNum > 0)
            {
                for (int i = 0; i < pageBtnList.Count; i++)
                {
                    pageBtnList[i].ChangeChoiceType(indexPage);
                }
            }
            else
            {
                Debug.LogError("最少显示页数不能为0");
            }
        }
        /// <summary>
        /// 点击翻页按钮后，对应跳到相应页面
        /// </summary>
        /// <param name="page"></param>
        public void Click2Page(int page)
        {
            if (page < 1 || page > allPage) return;
            if (allPage > pageLoadingModel.pageBtnNum && (page > startPage + pageLoadingModel.pageBtnNum - 1 || page < startPage))
            {
                startPage = GetStartPage(page);
            }
            indexPage = page;
            UpdateData();
        }
        /// <summary>
        /// 设置起始页
        /// </summary>
        int GetStartPage(int num)
        {
            return num > maxStartPageNum ? maxStartPageNum : num;
        }
    }
}
