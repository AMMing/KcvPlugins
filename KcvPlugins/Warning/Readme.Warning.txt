author/developer :AMing
website:http://kcvp.logs.moe/
Github:https://github.com/AMMing
SourceCode:https://github.com/AMMing/KcvPlugins




##提督很忙的拓展插件(KanColleViewer Plugins)
==========

KanColleViewer 在3.0版本之后增加了插件功能，可以通过添加插件完善kcv的功能。

本插件在只kcv3.4下测试，其他旧版本理论上可以，不过没有测试，使用时最好是用最新的kcv。


### 主要功能

* 在队伍里面有大破舰娘的时候发出警告,可以结合“AMing.SoundNotifier.dll”和“AMing.WindowsNotifierEx.dll”达到更醒目的提示。

### 使用方法

	将解压出来的dll放到kcv的Plugins目录下然后重启kcv，如果失败的话右键dll属性看看有个没有个什么什么锁定的 把那个解锁掉再重启kcv
	
### 注意事项

	“AMing.Plugins.Core.dll”为核心组件不可删除

### 参考项目

* [KanColleViewer](https://github.com/Grabacr07/KanColleViewer)


#### 许可证

* MIT License





更新日志

1.1

	tab+alt的时候看得到通知窗口的问题

1.0 -- 2015.02.04

	在队伍里面有大破舰娘的时候发出警告，入渠那边没有实时监听好需要回到母港才会生效，下个版本处理 orz因为游戏机制还是因为kcv监听的机制的原因，在推图的时候战斗结束 点击回港或者进击之后才会更新舰队信息，没办法在之前就监听到数据的变化，主动发包请求这种大作死的东西还是想都别想了orz