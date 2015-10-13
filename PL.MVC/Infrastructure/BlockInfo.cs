using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PL.MVC.Infrastructure
{
    public class BlockInfo<TSourse>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<TSourse> Items { get; set; }

        public int Skip
        {
            get { return PageSize*(PageNumber - 1); }
        }

        public int CurrentItems
        {
            get { return Skip + Items.Count(); }
        }
    }

    public class UserBlockInfo<TSourse> : BlockInfo<TSourse>
    {
        public Guid UserId { get; set; }
    }

    public class SearchBlockInfo<TSourse> : BlockInfo<TSourse>
    {
        public string SearchString { get; set; }
    }
}