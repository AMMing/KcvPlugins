##提督很忙的拓展插件([KanColleViewer Plugins](http://kcvp.logs.moe))

KanColleViewer 在3.0版本之后增加了插件功能，可以通过添加插件完善kcv的功能。

本插件在只kcv3.4下测试，其他旧版本理论上可以，不过没有测试，使用时最好是用最新的kcv。


###主要功能 

SettingsExtensions

	* 添加到系统托盘
	* 最小化时隐藏任务栏上的kcv
	* 退出kcv时提示
	* 更换kcv窗体主题
	* 设置kcv的工具面板位置
	* 肝船用的迷你小窗口（新样式）
	* 迷你小窗口添加鼠标穿透模式（幽灵模式）
	* 热键（老板键，开关声音，快速截图）等按键设置

WindowsNotifierEx

	* 适用不同情况下的信息通知效果,可以在win7下实现win8的通知效果，当然win8 win8.1这些也可以用，不过使用的时候需要将kcv的通知插件“WindowsNotifier.dll”去掉，否则就重复通知了。

SoundNotifier

	* 有通知的时候发出声音提示

Warning

	* 在队伍里面有大破舰娘的时候发出警告,可以结合“SoundNotifier”和“WindowsNotifierEx”达到更醒目的提示。


### 使用方法

* 将解压出来的dll放到kcv的Plugins目录下然后重启kcv，如果失败的话右键dll属性看看有个没有个什么什么锁定的 把那个解锁掉再重启kcv

### 注意事项

“AMing.Plugins.Core.dll”为核心组件不可删除

### 更新主页

* [http://kcvp.logs.moe](http://kcvp.logs.moe)

### 参考项目

* [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)

### SVG Icons 素材

* [http://raphaeljs.com/icons](http://raphaeljs.com/icons)
* [http://thenounproject.com/](http://thenounproject.com)


#### 许可证

* MIT License
