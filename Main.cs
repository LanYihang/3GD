using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Com.Ls.Util;
using Com.Ls.Core;
using System.Threading;
using System.Diagnostics;

namespace _3GD
{
    public partial class Main : Form
    {
        private List<string> download_list = new List<string>();//存储下载文件路径的集合
        private delegate void Entrus();//定义一个委托，用以通知主程序加载完基础数据，应该开始下载文件了

        public Main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //加载初始根节点
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                TreeNode parent_node = new TreeNode(this.txtFTP.Text);
                InitChildNodeForTreeView(this.txtFTP.Text,parent_node);
                tvFTP.Nodes.Add(parent_node);
            }
            catch (NonConnectionException ex)
            {
                this.txtShow.AppendText(ex.Message);
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText(ex.Message + "\r\n" + ex.StackTrace+"\r\n");
            }
        }
        //treeview中节点事件，用以加载子节点
        private void tvFTP_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    string child_url = e.Node.ToolTipText;
                    if (child_url != "")
                    {
                        if (e.Node.Nodes.Count == 0)//不存在子节点就加载，存在就不再请求
                            InitChildNodeForTreeView(child_url, e.Node);
                    }
                }
                else if (e.Button == MouseButtons.Right && e.Clicks == 1) 
                {
                    cmstripDownload.Show();
                }
            }catch(Exception ex)
            {
                this.txtShow.AppendText("**** Load Child Node Exception For TreeView ***\r\n");
                this.txtShow.AppendText(ex.Message+"\t"+ex.StackTrace+"\r\n");
            }
        }
        /// <summary>
        /// 按照规定的目录结构下载文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSpecialDown_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtShow.AppendText("Loads a special URL path....\r\n");
                //Entrus entrus = new Entrus(DownloadThread);
                //Thread thread = new Thread(new ParameterizedThreadStart(ThreadLoadURLMain));
                //thread.Start(entrus);
                Thread thread = new Thread(new ThreadStart(ThreadLoadURLMain));
                thread.Start();
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("**** Loads A Special URL Path Exception ***\r\n");
                this.txtShow.AppendText(ex.Message + "\t" + ex.StackTrace + "\r\n");
            }
        }
        /// <summary>
        /// 加载下载路径的线程入口，方面异常捕获和后台线程关闭
        /// <param name="obj">委托的对象</param>
        /// </summary>
        private void ThreadLoadURLMain(Object obj)
        {
            try
            {
                this.btnSpecialDown.Enabled = false;//特定下载的按钮注销
                LoadDownloadURLToDownload(this.txtFTP.Text);
                Entrus entrus = null;
                if (obj is Entrus)
                    entrus = obj as Entrus;
                entrus();
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("*** Load URL Exception ***\r\n");
                this.txtShow.AppendText(ex.Message + "\t" + ex.StackTrace + "\r\n");
            }
            finally 
            {
                //异常的情况下关闭线程
                if (Thread.CurrentThread.IsAlive)
                    Thread.CurrentThread.Abort();
            }
        }
        /// <summary>
        /// 重构不包含委托 加载下载路径的线程入口，方面异常捕获和后台线程关闭
        /// <param name="obj">委托的对象</param>
        /// </summary>
        private void ThreadLoadURLMain()
        {
            try
            {
                this.btnSpecialDown.Enabled = false;//特定下载的按钮注销
                LoadDownloadURLToDownload(this.txtFTP.Text);
                this.txtShow.AppendText("*** Download Files Success ***\r\n");
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("*** Load URL Exception ***\r\n");
                this.txtShow.AppendText(ex.Message + "\t" + ex.StackTrace + "\r\n");
            }
            finally
            {
                this.btnSpecialDown.Enabled = true;
                //异常的情况下关闭线程
                if (Thread.CurrentThread.IsAlive)
                    Thread.CurrentThread.Abort();
            }
        }
        /// <summary>
        /// 获得需要下载文件的所有URL路径并下载文件
        /// </summary>
        /// <param name="parent_url">父级URL路径</param>
        private void LoadDownloadURLToDownload(string parent_url)
        {
            try
            {
                string extension = Path.GetExtension(parent_url);
                Uri uri = new Uri(parent_url);
                WebClient client = new WebClient();
                if (extension == "")
                {
                    byte[] buf = client.DownloadData(uri);
                    string html = Encoding.Default.GetString(buf, 0, buf.Length);
                    string[] tags = BaseUtil.GetHtmlTagArray(html, "<A", "</A>");
                    bool exists_documents = false;//判定是否存在documents目录
                    foreach (string tag in tags)
                    {
                        if ((parent_url.Contains("TSGR1") && (!parent_url.Contains("Docs")) && (!parent_url.Contains("Documents"))) || (parent_url.Contains("TSGR2") && (!parent_url.Contains("Docs")) && (!parent_url.Contains("Documents"))))
                        {
                            //开始寻找该目录下的Docs或Documents目录 如果有Documents目录就下载该目录下的
                            if (tag == "Documents")
                                exists_documents = true;
                        }
                        if (tag == "tsg_ran" || tag == "WG1_RL1" || tag == "WG2_RL2" || tag.StartsWith("TSGR1") || tag.StartsWith("TSGR2"))
                        {
                            LoadDownloadURLToDownload(parent_url + tag + "/");
                        }
                        if (parent_url.Contains("Documents") || parent_url.Contains("Docs"))
                        {
                            if (tag == "[To Parent Directory]" )//|| Path.GetExtension(tag) == ".xlsm" || Path.GetExtension(tag) == ".xlsx")
                                continue;
                            //走到该步就该获得该目录下所有需要下载文件的URL
                            if (Path.GetExtension(tag) != "")//有后缀名加入集合
                            {
                                try
                                {
                                    DownloadFile(parent_url + tag);
                                    this.txtShow.AppendText("Load URL : " + parent_url + tag + "\r\n");
                                }
                                catch (WebException) 
                                {
                                    this.txtShow.AppendText("Load URL : " + parent_url + tag + "  该文件不存在\r\n");
                                    continue;
                                }
                            }
                            else
                                LoadDownloadURLToDownload(parent_url + tag + "/");
                        }
                    }
                    //当URL路径中不包含Docs或Documents的文件夹，表示需要进入寻找该目录，
                    if ((parent_url.Contains("TSGR1") && (!parent_url.Contains("Docs")) && (!parent_url.Contains("Documents"))) || (parent_url.Contains("TSGR2") && (!parent_url.Contains("Docs")) && (!parent_url.Contains("Documents"))))
                    {
                        if (exists_documents)
                            LoadDownloadURLToDownload(parent_url + "Documents/");
                        else
                            LoadDownloadURLToDownload(parent_url + "Docs/");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 为TreeView的节点添加子节点
        /// </summary>
        /// <param name="url">节点的URL路径</param>
        /// <param name="parent_node">父节点对象</param>
        private void InitChildNodeForTreeView(string url, TreeNode parent_node)
        {
            try
            {
                string extension = Path.GetExtension(url);
                Uri uri = new Uri(url);
                WebClient client = new WebClient();
                if (extension == "")
                {
                    byte[] buf = client.DownloadData(uri);
                    string html = Encoding.Default.GetString(buf, 0, buf.Length);
                    string[] tags = BaseUtil.GetHtmlTagArray(html, "<A", "</A>");
                    for (int i = 1; i < tags.Length; i++)
                    {
                        TreeNode child_node = new TreeNode(tags[i]);
                        child_node.ToolTipText = url + tags[i] + (Path.GetExtension(tags[i]) != "" ? "" : "/");
                        parent_node.Nodes.Add(child_node);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new NonConnectionException("Connection Failed!", ex);
            }
        }

        /// <summary>
        /// 开启批量下载文件模式
        /// </summary>
        private void DownloadThread()
        {
            try
            {
                foreach(string url in download_list)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(DownloadFile));
                    t.Start(url);
                }
            }
            catch (Exception ex)
            {
                this.txtShow.AppendText("*** Download File Exception ***\r\n");
                this.txtShow.AppendText(ex.Message+"\t"+ex.StackTrace+"\r\n");
            }
        }
        /// <summary>
        /// 下载文件主体方法
        /// </summary>
        /// <param name="obj">下载文件主路径</param>
        private void DownloadFile(object obj)
        {
            try
            {
                string url = "";
                if (obj is string)
                    url = obj as string;
                if (url != "")
                {
                    string target_file = url.Replace("http:/", "D:");
                    string directory_name=Path.GetDirectoryName(target_file);
                    if (!Directory.Exists(directory_name))
                        Directory.CreateDirectory(directory_name);
                    WebClient client = new WebClient();
                    client.DownloadFile(url, target_file);
                    this.txtShow.AppendText("Download file " + target_file + " completion\r\n");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //关闭窗体释放所有资源
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void tvFTP_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                TreeNode chooseNode = tvFTP.GetNodeAt(p);
                cmstripDownload.Show(tvFTP, p);
            }
        }
    }
}
