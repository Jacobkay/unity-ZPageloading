# ZPageLoading

#### 介绍
unity分页加载组件，可自定义展示形式，自定义展示数量，操作简单，内含操作示例，如有任何问题请加企鹅群：747648080

#### 软件架构
软件架构说明


#### 安装教程

1.  下载拖入工程中
2.	将“PageLoading/Prefabs/PagingLoad”预制体直接拖入到canvas下
3.  Inspector面板中即可看到所有显示配置项
4.	点击运行，即可使用
5.	内含使用demo脚本（PageLoadingDemo.cs），方便学习使用
6.	内含Tab组件，如需了解如何使用，请查看https://gitee.com/jacobkay/ztools

#### 使用说明

config
1.  Awake2Init: true => 分页加载运行即初始化，false => 运行时不会自动初始化，如有需要可自行调用接口进行初始化
2.  EveryPageRequest: true => 每一页都会请求接口进行数据刷新，false => 加载过的页面，再次查看不会重新请求数据
3.	Input2Page： true => 可打开输入页码进行跳转功能，false => 关闭输入页码跳转功能
4.	PageItemNum: 一页显示多少条内容
5.	PageBtnNum: 一次显示多少页可选择
6.	PageItemObj: 需要加载显示的对象（transform）
7.	PageBtnObj:	翻页按钮对象（gameobject）

API(PageLoading中)
1.	GetPageDataEvent（广播事件）： 通过网络请求获取当前页面数据，可拿到当前页面页码，一共要请求多少条
2.	SetPageItemEvent（广播事件）： 设置每一条数据对象显示内容，可拿到当前需要展示的对象，和展示的数据对象
3.	PageNumEvent（广播事件）： 可以监听到当前选择的页码
4.	CrtPageNum（属性）：可手动获取当前当前日期
5.	通过接口（Init）可初始化   
6.	跳到传进来的参数编号，即可翻到对应页面
7.	内含（unity单选框，复选框）功能，想了解怎样使用的，请查看unity-ZTools单选框，复选框说明书


#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
