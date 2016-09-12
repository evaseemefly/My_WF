using lshOA.MODEL;
using lshOA.UI.Models;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lshOA.UI.Controllers
{
    public class SearchController : Controller
    {
        IBLL.IBookBLL bookBLL { get; set; }
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBook()
        {
            Book model = new Book();
            
            model.AurhorDescription = "jlkfdjf";
            model.Author = "蒋坤2222";
            model.CategoryId = 1;
            model.Clicks = 1;
            model.ContentDescription = "面向JK编程";
            model.EditorComment = "adfsadfsadf";
            model.ISBN = "18890099";
            model.PublishDate = DateTime.Now;
            model.PublisherId = 72;
            model.Title = "jk入门到精通";
            model.TOC = "aaaaaaaaaadfsdfaaaaaa";
            model.UnitPrice = 22.3m;
            model.WordsCount = 1234;
            // BLL.BookManager bll = new BLL.BookManager();
            //int id= bll.Add(model);
            int id = 2222;
            IndexManager.GetInstance().AddOrEditQueue(id, model.Title, model.ContentDescription);
            return Content("ok");

        }

        public ActionResult SearchContent()
        {
            if(!string.IsNullOrEmpty(Request["btnSearch"]))
            {
                //搜索
                List<SearchResultViewModel> list = SearchBookContent();
                ViewData["searchList"] = list;
                return View("Index");
            }
            else
            {
                //创建索引
                //CreateContent();
            }
            return Content("ok");
        }

        public List<SearchResultViewModel> SearchBookContent()
        {
            string dirPath= @"C:\lucenedir";
            string searchKey = Request["txtSearchContent"];
            //将搜索的条件进行分词
            string[] keywords = COMMON.PanGuToken.PanGuSplitWord(searchKey);

            //创建搜索器
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(dirPath), new NoLockFactory());

            IndexReader reader = IndexReader.Open(directory, true);

            IndexSearcher searcher = new IndexSearcher(reader);

            //搜索条件
            PhraseQuery query = new PhraseQuery();
            foreach (string item in keywords)
            {
                query.Add(new Term("ContentDescription", item));
            }
            //多个查询条件的词之间的最大距离.在文章中相隔太远 也就无意义.（例如 “大学生”这个查询条件和"简历"这个查询条件之间如果间隔的词太多也就没有意义了。）
            query.SetSlop(50);

            //TopScoreDocCollector是盛放查询结果的容器
            TopScoreDocCollector collector = TopScoreDocCollector.create(1000, true);
            //根据query查询条件进行查询，查询结果放入collector容器
            searcher.Search(query, null, collector);
            ///得到所有查询结果中的文档,GetTotalHits():表示总条数   TopDocs(300, 20);
            /// //表示得到300（从300开始），到320（结束）的文档内容.
            //可以用来实现分页功能
            ScoreDoc[] docs = collector.TopDocs(0, collector.GetTotalHits()).scoreDocs;

            List<SearchResultViewModel> searchResultList = new List<SearchResultViewModel>();
            for (int i = 0; i < docs.Length; i++)
            {
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);
                SearchResultViewModel viewModel = new SearchResultViewModel();
                viewModel.Id = int.Parse(doc.Get("id"));
                viewModel.Title = doc.Get("title");
                viewModel.Url = "";
                viewModel.ContentDescription = COMMON.PanGuToken.CreateHightLight(searchKey, doc.Get("ContentDescription"));
                searchResultList.Add(viewModel);
            }
            return searchResultList;
        }

        public void CreateContent()
        {
            //1 将索引创建在磁盘中
            string indexPath = @"C:\lucenedir";//注意和磁盘上文件夹的大小写一致，否则会报错。将创建的分词内容放在该目录下。
            
            //1.1 打开索引
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());//指定索引文件(打开索引目录) FS指的是就是FileSystem
            //1.2 判断索引的目录下是否含有索引的文件
            bool isUpdate = IndexReader.IndexExists(directory);//IndexReader:对索引进行读取的类。该语句的作用：判断索引库文件夹是否存在以及索引特征文件是否存在。
            if (isUpdate)
            {
                //同时只能有一段代码对索引库进行写操作。当使用IndexWriter打开directory时会自动对索引库文件上锁。
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁（提示一下：如果我现在正在写着已经加锁了，但是还没有写完，这时候又来一个请求，那么不就解锁了吗？这个问题后面会解决）
                //1.3 判断索引文件是否被锁定，若被锁定则解除锁定
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }

            //2 创建分词器（使用盘古分词）
            Analyzer analyzer = new PanGuAnalyzer();
            //2.1 向索引库中写入索引
            IndexWriter writer = new IndexWriter(directory, analyzer, !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。
            //2.2 取出全部的书
            List<Book> list_book = bookBLL.GetList(c => true).ToList();
            foreach (var item in list_book)            
            {
                //2.3 将取出的全部书，添加到Document中（记录中）
                //一个doc概念上相等于一条记录，会含有一些属性字段来表示各个与之相关的数据
                Document document = new Document();//表示一篇文档。
                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("id", item.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                document.Add(new Field("author", item.Author.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）
                document.Add(new Field("title", item.Title, 
                    Field.Store.YES,
                    Field.Index.ANALYZED, 
                    Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("ContentDescription", item.ContentDescription, Field.Store.YES,//Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                    Field.Index.ANALYZED, //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）
                    Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS//Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。

                    ));
                //2.4 将记录（document）写入索引中
                writer.AddDocument(document);

            }
            //3 解锁并关闭连接
            writer.Close();//会自动解锁。
            directory.Close();//不要忘了Close，否则索引结果搜不到
        }
    }
}