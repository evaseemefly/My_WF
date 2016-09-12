using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lshOA.COMMON
{
   public class PanGuToken
    {
        /// <summary>
        /// 对输入的搜索条件进行分词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] PanGuSplitWord(string str)
        {
            List<string> list = new List<string>();
            //1 创建分词器，使用实现了Lucene.net.Analysis.Analyzer 的盘古分词器PanGuAnalyzer
            //对分词主要依靠Analyzer类解析实现
            //analysis对需要建立索引的文本进行分词、过滤等操作
            Analyzer analyzer = new PanGuAnalyzer();
            //2 Analyzer内部主要通过TokenStream类实现
            //通过TokenStream获取分词器中的 分词流
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            //TokenStream 获取下一个Token 
            while ((token = tokenStream.Next()) != null)
            {
                //获取分词流中的内容
                list.Add(token.TermText());
            }
            return list.ToArray();
        }

        // /创建HTMLFormatter,参数为高亮单词的前后缀
        public static string CreateHightLight(string keywords, string Content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
             new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, Content);

        }
    }
}
