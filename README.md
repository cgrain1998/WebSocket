# WebSocket
Websocket前后端交互Demo


##https://www.cnblogs.com/whatarey/p/12725984.html

[背景]
需要定时查询数据显示到前端,刚开始使用想到的是Ajax长轮询(这样子资源消耗太多了),然后百度,谷歌,发现Http 有一种新的请求方式  
##  ``` WebSocket```

## WebSocket 是什么?

在没有接触```WebSocket```之前,听说过 ``` Socket``` ,那么 ```WebSocket``` 和 ```Socket``` 到底是什么,他们二者关系有是什么?

```WebSocket ```与 ```Socket``` 的关系--> 其实讨论这个点的的时候,我想到的 ```Java``` 和   ```Javascript``` 的关系,但是写文章不能靠自认为,于是我搜索了一下二者联系,发现很多博文都没有说明二者的关系,而是都在对比二者


>  #长答案
>  ## 当我们探讨两件事物的区别和联系时，我们想探讨些什么？

> ### 对于我来说，大多数情况是想知道两件事物本身，而并不是想只想了解「区别」本身。那么对这个问题最直接的解决方法应该是去了解Socket和WebSocket的来源和用法
> ### 那么它们的区别和联系就不言自明了。
> ### [WebSocket和Socket的区别  作者:TheAlchemist ](https://www.jianshu.com/p/59b5594ffbb0/)

##所以, ```WebSocket``` 和 ```Socket``` 没有可比性 -->划重点


#介绍 一下 ```WebSocket```  && ```Socket```

##```WebSocket ```
  > WebSocket 是为了满足基于 Web 的日益增长的实时通信需求而产生的(一次请求,终身连接-->这里有点问题,大家明白就好,我主要先说的就是比Ajax定时请求 消耗资源少 ^_^)
  >  在传统的 Web 中，要实现实时通信，通用的方式是采用 HTTP 协议不断发送请求(AJAX 定时请求)
  >  但这种方式即浪费带宽（HTTP HEAD 是比较大的），又消耗服务器 CPU 占用（没有信息也要接受请求）

-----------------------------------------------------------------------------------------

## ```Socket ``` 
>Socket可以有很多意思，和IT较相关的本意大致是指在端到端的一个连接中，这两个端叫做Socket。
>对于IT从业者来说，它往往指的是TCP/IP网络环境中的两个连接端，大多数的API提供者（如操作系统，JDK）往往会提供基于这种概念的接口
>所以对于开发者来说也往往是在说一种编程概念。同时，操作系统中进程间通信也有Socket的概念，但这个Socket就不是基于网络传输层的协议了

>作者：TheAlchemist
>链接：https://www.jianshu.com/p/59b5594ffbb0/
>来源：简书
>简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。


``` WebSocket ```和``` Socket``` 最大的一点 ,就在于传输协议的不同

网上就张很神奇的七层协议图
> ### [  图片来自此博客 ](https://www.cnblogs.com/jiangzhaowei/p/8781635.html)

![七层协议](https://blog.zengrong.net/uploads/2014/12/TCP-IP.gif)

Socket是传输控制层协议 ->  这个靠近中间,不注重数据进行,负责数据传输,速度较快,功能较少



WebSocket是应用层协议-> 应用层靠近用户,然后功能多,所以速度较慢,反向代理,负载均衡,这些都是在这个基础进行

##关于 ```WebSocket``` 和 ```Socket```  这里我就先讲到这里,
下面是代码实际操作
本篇代码思路来自 
[C#版Websocket实例 作者:~雨落忧伤~](https://www.cnblogs.com/cjm123/p/9674506.html)
下面我就献丑了

>后端 NetCore 3.1
>前端 Html页面
>环境 Windows10
>测试浏览器 谷歌
> 借助工具 nssm  ,VS2019

