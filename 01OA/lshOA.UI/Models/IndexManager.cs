using lshOA.MODEL;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace lshOA.UI.Models
{
    public sealed class IndexManager
    {
        private static readonly IndexManager indexManager = new IndexManager();

        private IndexManager()
        {

        }

        public static IndexManager GetInstance()
        {
            return indexManager;
        }
        Queue<IndexContent> queue = new Queue<IndexContent>();

        /// <summary>
        /// 添加指定的对象到索引队列中
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void AddOrEditQueue(int id,string title,string content)
        {
            IndexContent indexContent = new IndexContent();
            if(content!=null)
            indexContent.ContentDescription = content;
            
            
                indexContent.Id = id;
            
            if(title!=null)
            {
                indexContent.Title = title;
            }
            indexContent.LuceneEnumType = MODEL.Enum.LuceneEnumType.Add;
            queue.Enqueue(indexContent);
                
        }

        /// <summary>
        /// 向队列中添加要删除的对象
        /// </summary>
        /// <param name="id"></param>
        public void DeleteQueue(int id)
        {

            IndexContent indexContent = new IndexContent();
            //只需指定要删除的对象的id，因为在索引中已经存在该id的对象，所以不需要其他的属性
            indexContent.Id = id;
            //将该对象的删除标记改为删除
            indexContent.LuceneEnumType = MODEL.Enum.LuceneEnumType.Delete;
            //加入队列
            queue.Enqueue(indexContent);
        }

        public void CreateThread()
        {
            Thread myThread = new Thread(CreateSearchIndex);
            myThread.IsBackground = true;
            myThread.Start();
        }

        /// <summary>
        /// 对索引库执行修改或删除操作
        /// </summary>
        private void CreateSearchIndex()
        {
            while(true)
            {
                //若队列长度不为0，则执行写入索引库的操作
                if(queue.Count>0)
                {
                    try
                    {
                        WriteSearchContent();
                    }
                    catch
                    {
                        //写入到日志中
                    }
                }
                //若队列长度为0，则将该线程挂起3s
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// 根据队列中的索引（添加/修改 或 删除）对索引库执行相应的操作
        /// </summary>
        private void WriteSearchContent()
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
            //第三个参数 true指重新创建索引,false指从当前索引追加
            IndexWriter writer = new IndexWriter(directory, analyzer, !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);//向索引库中写索引。这时在这里加锁。
            //2.2 取出索引队列中的全部对象

            //  //2.2 取出全部的书
            //List<Book> list_book = bookBLL.GetList(c => true).ToList();

           while(queue.Count>0)
            {
                //2.2.1 将队列中的索引对象出队
                IndexContent indexContent = queue.Dequeue();
                //2.2.2 将当前对象从 索引库中删除
                writer.DeleteDocuments(new Term("id", indexContent.Id.ToString()));
                //若当前对象是 删除状态 则直接跳过本次循环
                if(indexContent.LuceneEnumType==MODEL.Enum.LuceneEnumType.Delete)
                {
                    continue;
                }
                //2.3 将取出的全部书，添加到Document中（记录中）
                //一个doc概念上相等于一条记录，会含有一些属性字段来表示各个与之相关的数据
                Document document = new Document();//表示一篇文档。
                //Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
                document.Add(new Field("id", indexContent.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                
                //Field.Index. ANALYZED:进行分词保存:也就是要进行全文的字段要设置分词 保存（因为要进行模糊查询）
                document.Add(new Field("title", indexContent.Title,
                    Field.Store.YES,
                    Field.Index.ANALYZED,
                    Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS));

                //Lucene.Net.Documents.Field.TermVector.WITH_POSITIONS_OFFSETS:不仅保存分词还保存分词的距离。
                document.Add(new Field("ContentDescription", indexContent.ContentDescription, Field.Store.YES,//Field.Store.YES:表示是否存储原值。只有当Field.Store.YES在后面才能用doc.Get("number")取出值来.Field.Index. NOT_ANALYZED:不进行分词保存
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