author/developer :AMing
website:http://kcvp.logs.moe/
Github:https://github.com/AMMing
SourceCode:https://github.com/AMMing/KcvPlugins




##提督很忙的拓展插件(KanColleViewer Plugins)
==========

KanColleViewer 在3.0版本之后增加了插件功能，可以通过添加插件完善kcv的功能。

本插件在只kcv3.4下测试，其他旧版本理论上可以，不过没有测试，使用时最好是用最新的kcv。


### 主要功能

* 适用不同情况下的信息通知效果,可以在win7下实现win8的通知效果，当然win8 win8.1这些也可以用，不过使用的时候需要将kcv的通知插件“WindowsNotifier.dll”去掉，否则就重复通知了。

### 使用方法

	将解压出来的dll放到kcv的Plugins目录下然后重启kcv，如果失败的话右键dll属性看看有个没有个什么什么锁定的 把那个解锁掉再重启kcv
	
### 注意事项

	“AMing.Plugins.Core.dll”为核心组件不可删除

### 参考项目

* [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)


#### 许可证

* MIT License





更新日志

1.3

	tab+alt的时候看得到通知窗口的问题，通知的背景颜色根据kcv的主题色改变


1.2 -- 2015.02.03

	修复某些桌面下通知窗口的位置不对的bug,调用core模块实现接收其他插件发出的通知，添加警告样式


1.1 -- 2015.01.19

	点击通知信息会隐藏信息并激活kcv


1.0 -- 2015.01.18

	在win7下模仿win8的通知效果 没有声音，打算分开成另一个插件