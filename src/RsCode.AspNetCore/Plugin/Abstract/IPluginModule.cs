using System;

namespace RsCode.AspNetCore.Plugin.Abstract
{
    public interface IPluginModule
    {
        /// <summary>
        /// Title of the plugin, can be used as a property to display on the user interface
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Name of the plugin, should be an unique name
        /// </summary>
        string Name { get; }

        string Description { get;  }

        /// <summary>
        /// Version of the loaded plugin
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 作者
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 更新日期
        /// </summary>
        string UpdateDate { get; }

        /// <summary>
        /// 插件入口
        /// </summary>
        string EntryControllerName { get; }
    }
}
