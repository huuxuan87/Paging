using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebQuanlyKiot.MyCode
{
    public class MyPaged
    {
        /// <summary>
        /// vao: trang hien tai, so luong item moi trang, tong so item<para />
        /// pageCur = 1: [1] 2 3 4 5 ... End(12)<para />
        /// pageCur=2: 1 [2] 3 4 5 ... End(12)<para />
        /// pageCur=3: 1 2 [3] 4 5 ... End(12)<para />
        /// pageCur=4: 1 2 3 [4] 5 6 ... End(12)<para />
        /// pageCur=5: 1 2 3 4 [5] 6 7 ... End(12)<para />
        /// pageCur=6: First(1) ... 4 5 [6] 7 8 ... End(12)<para />
        /// pageCur=7: First(1) ... 5 6 [7] 8 9 ... End(12)<para />
        /// pageCur=8: First(1) ... 6 7 [8] 9 10 11 12<para />
        /// pageCur=9: First(1) ... 7 8 [9] 10 11 12<para />
        /// pageCur=10: First(1) ... 8 9 [10] 11 12<para />
        /// pageCur=11: First(1) ... 8 9 10 [11] 12<para />
        /// pageCur=12: First(1) ... 8 9 10 11 [12]<para />
        /// </summary>
        /// <param name="pageCur"></param>
        /// <param name="sizePerPage"></param>
        /// <param name="totalItem"></param>
        /// <param name="sizePagePerSection"></param>
        /// <returns></returns>
        public static MyPagedResult Paged(int pageCur, int sizePerPage, int totalItem, int sizePagePerSection = 5)
        {
            // input
            if (pageCur < 1)
            {
                pageCur = 1;
            }
            if(sizePerPage < 1)
            {
                sizePerPage = 1;
            }
            var rs = new MyPagedResult();
            if (totalItem <= 0)
            {
                return rs;
            }
            if(sizePagePerSection < 1)
            {
                sizePagePerSection = 1;
            }
            int totalPage = (int)Math.Ceiling((double)totalItem / sizePerPage);
            if(sizePagePerSection > totalPage)
            {
                sizePagePerSection = totalPage;
            }
            if(pageCur > totalPage)
            {
                pageCur = totalPage;
            }
            
            rs.PageCur = pageCur;
            rs.SizePerPage = sizePerPage;
            rs.TotalItem = totalItem;
            rs.TotalPage = totalPage;
            rs.ItemPageCurStartIndex = (pageCur - 1) * sizePerPage;
            rs.ItemPageCurFinalIndex = rs.ItemPageCurStartIndex + sizePerPage - 1;

            int sizePagePerSectionHalf = sizePagePerSection / 2;

            // trang ban dau
            rs.SectionPageStart = pageCur - sizePagePerSectionHalf;
            if(rs.SectionPageStart < 1)
            {
                rs.SectionPageStart = 1;
            }
            // trang ket thuc
            rs.SectionPageEnd = rs.SectionPageStart + sizePagePerSection - 1;
            if(rs.SectionPageEnd > totalPage)
            {
                rs.SectionPageStart -= (rs.SectionPageEnd - totalPage);
                rs.SectionPageEnd = totalPage;
                if(rs.SectionPageStart < 1)
                {
                    rs.SectionPageStart = 1;
                }
            }

            // co trang dau tien
            if (rs.SectionPageStart <= 3)
            {
                rs.SectionPageStart = 1;
            }
            else
            {
                rs.HasPageFirst = true;
                // khoang cach dau
                if (pageCur - sizePagePerSectionHalf >= 2)
                {
                    rs.HasDotFirst = true;
                }
            }
            // trang cuoi
            if(rs.SectionPageEnd < totalPage)
            {
                if (totalPage - rs.SectionPageEnd <= 2)
                {
                    rs.SectionPageEnd = totalPage;
                }
                else
                {
                    rs.HasPageEnd = true;
                    // khoang cach cuoi
                    if (totalPage - rs.SectionPageEnd >= 2)
                    {
                        rs.HasDotEnd = true;
                    }
                }
            }

            return rs;
        }
    }

    public class MyPagedResult
    {
        public int PageCur { get; set; } = 0;
        public int SizePerPage { get; set; } = 0;
        public int TotalItem { get; set; } = 0;
        public int TotalPage { get; set; } = 0;
        public int ItemPageCurStartIndex { get; set; } = 0;
        public int ItemPageCurFinalIndex { get; set; } = 0;

        public int SectionPageStart { get; set; } = 0;
        public int SectionPageEnd { get; set; } = 0;
        public bool HasPageFirst { get; set; } = false;
        public bool HasDotFirst { get; set; } = false;
        public bool HasDotEnd { get; set; } = false;
        public bool HasPageEnd { get; set; } = false;
    }
}
