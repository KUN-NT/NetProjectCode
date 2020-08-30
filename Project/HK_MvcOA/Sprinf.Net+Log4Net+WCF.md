# Spring.Net

1、更深的层次——提到DI，依赖注入，是IOC的一种重要实现
一个对象的创建往往会涉及到其他对象的创建，比如一个对象A的成员变量持有着另一个对象B的引用，这就是依赖，A依赖于B。IOC机制既然负责了对象的创建，那么这个依赖关系也就必须由IOC容器负责起来。负责的方式就是DI——依赖注入，通过将依赖关系写入配置文件，然后在创建有依赖关系的对象时，由IOC容器注入依赖的对象，如在创建A时，检查到有依赖关系，IOC容器就把A依赖的对象B创建后注入到A中（组装，通过反射机制实现），然后把A返回给对象请求者，完成工作。

* 1、[添加组件](https://pan.baidu.com/s/1d6kDjk6edd6tIIjBmTBm-w)
* 2、添加Config文件夹 在该文件夹下添加配置文件
* 3、在Web.config文件中配置Spring.Net 其实就是指定配置文件
* 4、让Global.asax中MvcApplication类继承SpringMvcApplication
* 5、使用时只需要IUserInfoService UserInfoService { get; set; } springnet便会帮你实例化userinfoservice

####    Web.config中的配置
       
        <configSections>
            <!--配置Spring.Net-->
		    <sectionGroup name="spring">
			    <section name="context" type="Spring.Context.Support.MvcContextHandler, Spring.Web.Mvc4"/>
		    </sectionGroup>
        </configSections>
        <spring>
		    <context>
			    <!--分离配置文件 也可以不分离 写在同一文件下-->
			    <!--针对控制器-->
			    <resource uri="file://~/Config/controllers.xml"/>
			    <!--针对业务-->
			    <resource uri="file://~/Config/services.xml"/>
		        </context>
	    </spring>

####    Config文件夹下配置

        controllers.xml 控制器相关配置

            <objects xmlns="http://www.springframework.net">
                <!--singleton是否采用单例-->
	            <object type="HK_MvcOA_WebApp.Controllers.HomeController,HK_MvcOA_WebApp" singleton="false" ><!--type 控制器类员权限定名 程序集名-->
		            <property name="UserInfoService" ref="UserInfoService" /><!--name与属性名相对应 ref与services.xml相对应 页面中有几个就也几个Property-->
	            </object>
	            <object type="HK_MvcOA_WebApp.Controllers.LoginController,HK_MvcOA_WebApp" singleton="false" >
		            <property name="UserInfoService" ref="UserInfoService" /> <!--与属性名相对应-->
	            </object>
	            <!--不分离可直接在这写<object type="HK_MvcOA_Bll.UserInfoService, HK_MvcOA_Bll" singleton="false" name="UserInfoService"></object>-->
            </objects>

        services.xml 业务层相关配置

            <objects>
	            <object type="HK_MvcOA_Bll.UserInfoService, HK_MvcOA_Bll" singleton="false" name="UserInfoService">
	            </object>
            </objects>



# Log4Net

* 1、添加组件
* 2、在web.config中配置 (文件保存位置 文件保存格式)
* 3、在程序开始的地方(一般是指Global文件中的Application_Startf方法)添加读取log4net的配置信息的代码
* 4、开启一个线程 扫描异常信息 并保存到文件(或其他)中
   
#### 开启一个线程 扫描异常信息队列 有保存到日志文件 没有休息一段时间再扫
	
	1、在此之前要先新建一个类捕获异常
	public class MyExceptionAttribute : HandleErrorAttribute
	{
		public static Queue<Exception> exceptionQueue = new Queue<Exception>();
		public override void OnException(ExceptionContext filterContext)
		{
			base.OnException(filterContext);
			Exception ex = filterContext.Exception;
			//将错误写到队列
			exceptionQueue.Enqueue(ex);
			//跳转到错误页
			filterContext.HttpContext.Response.Redirect("/Error.html");
		}
	}

	2、在App_Start中FilterConfig注册上述类
	//filters.Add(new HandleErrorAttribute());
	filters.Add(new MyExceptionAttribute());

	3、Application_Start中
    string filePath = Server.MapPath("/Log");
	ThreadPool.QueueUserWorkItem((a) =>
	{
		while (true)
		{
			//判断错误队列是否有数据
			if (MyExceptionAttribute.exceptionQueue.Count > 0)
			{
				Exception ex = MyExceptionAttribute.exceptionQueue.Dequeu();
				//队列中有数据 写入日志
				if (ex != null)
				{
					//将数据保存到文件中
					//string fileName = DateTime.Now.ToStrin("yyyy-MM-dd");
					//File.AppendAllText(fileName, ex.ToString(),System.Text.Encoding.UTF8);
					//使用log4net保存数据
					ILog logger = LogManager.GetLogger("errorMsg");
					logger.Error(ex.ToString());
				}
				else
				{
					//如果队列中没有数据 休息3秒
					Thread.Sleep(3000);
				}
			}
			else
			{
				//如果队列中没有数据 休息3秒
				Thread.Sleep(3000);
			}
		}
	}, filePath);

#### 读取log4net的配置信息的代码

    log4net.Config.XmlConfigurator.Configure();

#### Web.config

    <configSections>
		<!--Log4Net配置-->
		<section name="log4net" type="log4net.Config.Log4NetConfigurationSectionHandler, log4net"/>
	</configSections>
    <log4net>
		<!-- OFF, FATAL, ERROR, WARN, INFO, DEBUG, ALL -->
		<!-- Set root logger level to ERROR and its appenders -->
		<root>
			<level value="ERROR"/>
			<appender-ref ref="SysAppender"/>
		</root>

		<!-- Print only messages of level DEBUG or above in the packages -->
		<logger name="WebLogger">
			<level value="ERROR"/>
		</logger>
		<appender name="SysAppender" type="log4net.Appender.RollingFileAppender,log4net" >
			<param name="File" value="App_Data/" /><!--文件路径-->
			<param name="AppendToFile" value="true" /><!--追加写入-->
			<param name="RollingStyle" value="Date" /><!--滚动备份依据-->
			<param name="DatePattern" value="&quot;Logs_&quot;yyyyMMdd&quot;.txt&quot;" /><!--文件名-->
			<param name="StaticLogFileName" value="false" /><!--文件名是否为静态-->
			<layout type="log4net.Layout.PatternLayout,log4net"><!--写入格式-->
				<param name="ConversionPattern" value="%d [%t] %-5p %c - %m%n" />
				<param name="Header" value="&#13;&#10;----------------------header--------------------------&#13;&#10;" />
				<param name="Footer" value="&#13;&#10;----------------------footer--------------------------&#13;&#10;" />
			</layout>
		</appender>
		<appender name="consoleApp" type="log4net.Appender.ConsoleAppender,log4net">
			<layout type="log4net.Layout.PatternLayout,log4net">
				<param name="ConversionPattern" value="%d [%t] %-5p %c - %m%n" />
			</layout>
		</appender>
	</log4net>


# 其他
## Session校验

#### 新建一个特性类继承ActionFilterAttribute类实现它的方法

	//1执行控制器代码前
	public override void OnActionExecuting(ActionExecutingContext filterContext)
	{
		base.OnActionExecuting(filterContext);
		if (Session["userInfo"] == null)
		{
			//如果使用这种跳转方式 不会返回ActionResult(而Mvc要求必须返回) 程序还是会执行拦截器后的代码
			//filterContext.HttpContext.Response.Redirect("/Login/Index");
			filterContext.Result = Redirect("/Login/Index");
		}
	}
	//2执行控制器代码后
	public override void OnActionExecuted(ActionExecutedContext filterContext)
	{
		base.OnActionExecuted(filterContext);
	}
	//3、渲染视图前
	public override void OnResultExecuting(ResultExecutingContext filterContext)
	{
		base.OnResultExecuting(filterContext);
	}
	
	//4、渲染视图后
	public override void OnResultExecuted(ResultExecutedContext filterContext)
	{
		base.OnResultExecuted(filterContext);
	}
	使用时只要在要检验的类上加上特性类的类名即可

#### 新建一个控制器类重写OnActionExecuting方法

	protected override void OnActionExecuting(ActionExecutingContext filterContext)
	{
		base.OnActionExecuting(filterContext);
		if (Session["userInfo"] == null)
		{
			//如果使用这种跳转方式 不会返回ActionResult(而Mvc要求必须返回) 程序还是会执行拦截器后的代码
			//filterContext.HttpContext.Response.Redirect("/Login/Index");
			filterContext.Result = Redirect("/Login/Index");
		}
	}
	使用时让要使用的类继承该控制器类

## Memcache分布式缓存

* 可以将缓存数据存到其他机器内存中 （而Cache只能存到本机中）
* 没提供主从赋值的功能、没提供容灾功能(缓存的机器挂了 数据就没了)(Redis提供了)(但磁盘坏了就没办法了 可以多保存几份)
* 插入 查找适合自己长度的块 会造成内存浪费
* 删除 不检测是否过期 查询时才查看它是否过期 过期就删除

#### 1、安装Memcache cmd进入memcache保存路径 输入Memcache -d install

#### 2、如果用户过多 我们就要采用分布式系统 如果用Session保存用户数据 如果下次访问请求被其他机器捕获 而这台机没有上台机的Session数据 这就出问题了这时候就可以用Memcache将数据缓存到其他机器中(把Session保存到数据库太影响性能)

Memcache分布式缓存是共享的(可以让不同主机上的多个用户同时缓存) 想要让他实现Session的效果 就要区分使用者

### 3、使用Memcache

#### MemcacheHelper类
	public class MemcacheHelper
	{
		private static readonly MemcachedClient mc = null;

		static MemcacheHelper()
		{
			//最好放在配置文件中
			string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

			//初始化池
			SockIOPool pool = SockIOPool.GetInstance();
			pool.SetServers(serverlist);

			pool.InitConnections = 3;
			pool.MinConnections = 3;
			pool.MaxConnections = 5;

			pool.SocketConnectTimeout = 1000;
			pool.SocketTimeout = 3000;

			pool.MaintenanceSleep = 30;
			pool.Failover = true;

			pool.Nagle = false;
			pool.Initialize();

			// 获得客户端实例
			mc = new MemcachedClient();
			mc.EnableCompression = false;
		}
		//存储数据
		public static bool Set(string key, object value)
		{
			return mc.Set(key, value);
		}
		public static bool Set(string key, object value, DateTime time)
		{
			return mc.Set(key, value, time);
		}
		//获取数据
		public static object Get(string key)
		{
			return mc.Get(key);
		}
		//删除数据
		public static bool Delete(string key)
		{
			if (mc.KeyExists(key))
			{
				return mc.Delete(key);

			}
			return false;
		}
	}

#### 使用
string sessionId = Guid.NewGuid().ToString();
MemcacheHelper.Set(sessionId, SerializeHelper.SerializeToString(userInfo),DateTime.Now.AddMinutes(20));//将登录用户信息存储到Memcache中。
Response.Cookies["sessionId"].Value = sessionId;//将Memcache的key以Cookie的形式返回给浏览器。

#### 检验
if (Request.Cookies["sessionId"] != null)
{
	string sessionId = Request.Cookies["sessionId"].Value;
	object obj = MemcacheHelper.Get(sessionId);
	...
}

## T4模板 <# #>中内容会执行 没在这里面的原样输出

## Json序列化（using Newtonsoft.Json;）

序列化:		JsonConvert.SerializeObject(value)
反序列化:	JsonConvert.DeserializeObject<T>(str) //T为反序列化成的数据类型

当我们将一个类型中的成员标记为[JsonIgnore] 将不会对该成员进行序列化 比如:数据中含有复杂数据类型的成员 无法将其序列化 程序就会报错 我们就可以将它标记为[JsonIgnore]


## Redis
pm>:Install-Package ServiceStack.Redis

## WebService Wcf

### WebService 能使得运行在不同机器上的不同应用程序数据通信

使用开放的xml标准来描述、发布、发现、协调和配置这些应用程序

服务端 新建web服务(记得编译) [WebMethod]标签 标记方法是公开的可以被外部访问

	public class WebService1 : System.Web.Services.WebService
	{
		[WebMethod]
		public string HelloWorld()
		{
			return "Hello World";
		}
		[WebMethod]
		public int Add(int a,int b)
		{
			return a+b;
		}
	}

客户端 添加服务应用 创建webservice对象即可调用webservice中定义的方法

	ServiceReference1.WebService1SoapClient web = new ServiceReference1.WebService1SoapClient();
	Response.Write(web.Add(3, 8));

基于互联网 只能部署到IIS

### Wcf 

#### 服务端的创建

添加一个项目 新建一个接口 添加引用System.ServiceModel 定义要公布的方法(相当于右击添加Wcf服务)
	
	[ServiceContract]//服务契约
	public interface IWeb
	{
		[OperationContract]
		int Add(int a, int b);
	}

新建一个类继承该接口 实现接口方法

	public class Web:IWeb
	{
		public int Add(int a, int b)
		{
			return a + b;
		}
	}
##### 寄宿到控制台项目 

	新建控制台应用 添加引用System.ServiceModel
	using (ServiceHost host = new ServiceHost(typeof(Bll.Web)))
	{
		host.Open();
		Console.WriteLine("服务已启动，按任意键中止...");
		Console.ReadKey(true);
		host.Close();
	}
	App.config
	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
	<system.serviceModel>
		<services>
			<service name="Bll.Web" behaviorConfiguration="behaviorConfiguration">
				<host>
					<baseAddresses>
						<add baseAddress="http://localhost:8000/"/>
					</baseAddresses>
				</host>
				<endpoint address="" binding="basicHttpBinding" contract="FBI.IWeb"></endpoint>
			</service>
		</services>
		<behaviors>
			<serviceBehaviors>
				<behavior name="behaviorConfiguration">
					<serviceMetadata httpGetEnabled="true"/>
				</behavior>
			</serviceBehaviors>
		</behaviors>
	</system.serviceModel>
	</configuration>

##### 寄宿到IIS

	新建web应用 添加wcf服务类(偷懒的方法 新建一个类将后缀改为svc)
	删除方法中内容 写入<%@ ServiceHost Service="Bll.Web"%>
	配置文件加入
	<system.serviceModel>
		<services>
			<service name="Bll.Web">
				<endpoint binding="wsHttpBinding" contract="FBI.IWeb"/>
			</service>
		</services>
	</system.serviceModel>
	在IIS中配置
	http://127.0.0.1:800/Service.svc

#### 客户端调用

	方法一:
	打开vs安装目录下的开发者命令提示符 进入客户端的目录下 输入
	svcutil http://localhost:8000/?wsdl /o:FirstServiceClient.cs
	然后将生成的两个文件包含到项目中
	删除App.config 将output.config改为App.

	添加引用System.ServiceModel
	创建wcf服务对象即可调用相应的方法

	方法二:
	就像WebService一样添加服务引用
	其他同上

#### 数据契约
[DataContract]
[DataMember]
在传输中序列化

## Wcf Ria Service

[博客园](http://www.cnblogs.com/lincats/archive/2011/07/05/2098155.html)

创建Silverlight应用程序时选择使用RIA服务的话 RIA服务会在客户端创建Generated_Code文件夹，这里就是由RIA服务构建任务生成的代码的位置

当我们在服务端创建域服务类（实际上就是一个标准的WCF服务）后，RIA会自动在客户端生成代码投影以便与服务器端进行通信，意味着开发人员不用关注添加领域服务引用之类的工作

RIA服务构建任务自动在客户端项目中产生代码以便与领域服务器端进行交互，它为每个领域服务创建一个领域上下文（Domain Context），并为由领域服务公开的实体类创建了一个代理类

## 创建领域服务时检测不到实体类

原因：原来的实体框架类生成的时候是基于DbContext,现在是基于ObjectContext


解决方法：

  1. 打开你的实体模型设计窗口

  2. 为了保证没有实体被选中，你可以在空白处单击一下

  3. 在实体设计窗口的属性窗口中把“Code Generation Strategy” 即代码生成策略属性从none 改为default

  4. 删除两个.tt文件

  5. 重新生成项目


## 遇到问题

FirstOrDefault下面出现红线 (不会自动加)可能没加using System.Linq;
将数据保存到Memcache时报错 没改ef模板 没法序列化复杂的数据类型(这里是后面的几个导航属性 导航属性可以让我们操作相关表)

## 理解
菜单权限:控制相关菜单的显示 		比如:提供用户管理的权限 就只给他显示用户管理的入口 
非菜单权限:控制在页面中的操作种类	 比如:打开用户管理页面 只能编辑 不能进行删除等操作
菜单权限在主界面进行校验 非菜单界面在页面基类中校验

## silverlight的几点小知识

创建silverlight应用程序
会让你选择是否创建服务端和是否创建RIA服务(会生成隐藏目录Generated_Code)

添加一个silverlight客户端是会让你选择依附于那个服务端(会在服务端的ClientBin目录下生成xap包)
是否启用RIA服务
是否在服务端添加相应页(会在服务端添加相应的aspx和html代码)
...