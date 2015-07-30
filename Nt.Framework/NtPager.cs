using Nt.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Nt.Framework
{
    public class NtPager
    {
        #region Props
        /// <summary>
        /// 存放页码数据
        /// </summary>
        public List<ListItem> Pager { get; set; }
        int _pageSize = 12;
        /// <summary>
        /// 每页显示的记录条数
        /// </summary>
        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        int _pageIndex = 0;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                if (_pageIndex < 1)
                {
                    if (!Int32.TryParse(WebHelper.Request["page"], out _pageIndex)
                   || _pageIndex < 1)
                        _pageIndex = 1;
                }
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        int _recordCount = 0;
        /// <summary>
        /// 记录的总条数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
            set
            {
                _recordCount = value;
                CalculatePager();
            }
        }

        /// <summary>
        /// 页面的个数
        /// </summary>
        public int PageCount { get; private set; }

        int _pagerItemCount = 5;
        /// <summary>
        /// 每页显示的页码个数
        /// </summary>
        public int PagerItemCount
        {
            get { return _pagerItemCount; }
            set
            {
                if (value < 1) return;
                _pagerItemCount = value;
            }
        }
        /// <summary>
        /// 第一页
        /// </summary>
        public int FirstPageNo { get; private set; }
        /// <summary>
        /// 上一页
        /// </summary>
        public int PrePageNo { get; private set; }
        /// <summary>
        /// 下一页
        /// </summary>
        public int NextPageNo { get; private set; }
        /// <summary>
        /// 最后一页
        /// </summary>
        public int EndPageNo { get; private set; }

        #endregion

        #region constructors
        public NtPager()
        { }

        public NtPager(int total, int size, int pageritemC)
        {
            _pageSize = size;
            _pagerItemCount = pageritemC;
            RecordCount = total;
        }

        public NtPager(int total, int size)
            : this(total, size, 5)
        {

        }

        public NtPager(int total)
            : this(total, 12)
        {

        }

        #endregion

        bool _pagerCalculated = false;
        /// <summary>
        /// 计算页码
        /// </summary>
        void CalculatePager()
        {
            if (_pagerCalculated)
                return;
                        Pager = new List<ListItem>();
            _pagerCalculated = true;
            if (RecordCount == 0)
                return;
            PageCount = RecordCount % PageSize == 0 ? RecordCount / PageSize : RecordCount / PageSize + 1;

            if (PageIndex > PageCount)
                PageIndex = PageCount;

            int[] IntPager = GenPager(PageIndex, PageSize, PageCount, PagerItemCount);

            FirstPageNo = PageIndex < 2 ? 0 : 1;
            PrePageNo = PageIndex < 2 ? 0 : (PageIndex - 1);

            for (int i = 0; i < IntPager.Length; i++)
            {
                var pagerItem = new ListItem();
                if (PageIndex == IntPager[i])
                {
                    pagerItem.Value = "0";
                    pagerItem.Selected = true;
                }
                pagerItem.Text = IntPager[i].ToString();
                Pager.Add(pagerItem);
            }

            EndPageNo = PageIndex >= PageCount ? 0 : PageCount;
            NextPageNo = PageIndex >= PageCount ? 0 : PageIndex + 1;
        }

        /// <summary>
        /// 生成页码数据
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="viewCount">可见的页码数</param>
        /// <returns>包含可见页码的数据</returns>
        public static int[] GenPager(int pageIndex, int pageSize, int pageCount, int viewCount)
        {
            if (pageIndex < 1)
                pageIndex = 1;
            if (pageIndex > pageCount)
                pageIndex = pageCount;

            int[] list = null;
            int p = 0;
            if (pageCount <= viewCount)
            {
                list = new int[pageCount];
                for (int i = 1; i <= pageCount; i++)
                {
                    list[p++] = i;
                }
                p = 0;
            }
            else
            {
                list = new int[viewCount];
                int v = (viewCount + 1) % 2;
                int b = viewCount / 2;
                int sup = pageIndex + b - v;
                int sub = pageIndex - b;
                if (sup >= viewCount && sup <= pageCount)
                {
                    for (int i = sub; i <= sup; i++)
                    {
                        list[p++] = i;
                    }
                }
                else if (sup < viewCount)
                {
                    for (int i = 1; i <= viewCount; i++)
                    {
                        list[p++] = i;
                    }
                }
                else
                {
                    for (int i = pageCount - viewCount + 1; i <= pageCount; i++)
                    {
                        list[p++] = i;
                    }
                }
            }
            return list;
        }

    }
}
