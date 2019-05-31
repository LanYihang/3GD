using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ls.Util
{
    class BaseUtil
    {
        /// <summary>
        /// 获得页面字符串中某节点的全部数据
        /// </summary>
        /// <param name="html">页面数据</param>
        /// <param name="start_tag">起始节点</param>
        /// <param name="end_tag">结束节点</param>
        /// <returns>返回所有匹配节点数组</returns>
        public static string[] GetHtmlTagArray(string html, string start_tag,string end_tag)
        {
            try
            {
                List<string> tag_list = new List<string>();//获得的标签组合
                do
                {
                    string tag = html.Substring(html.IndexOf(start_tag), html.IndexOf(end_tag) - html.IndexOf(start_tag) + end_tag.Length);
                    tag_list.Add(tag.Substring(tag.IndexOf(">")+1,tag.LastIndexOf("<")-tag.IndexOf(">")-1));
                    html = html.Replace(tag, "");//清除掉已匹配的节点
                } while (html.IndexOf(start_tag) > 0);
                return tag_list.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception("获得标签组异常!", ex);
            }
        }
    }
}
