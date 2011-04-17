using System;
using System.Linq;
using Raven.Client.Indexes;
using RavenDbBlog.Core.Models;

namespace RavenDbBlog.Indexes
{
    public class Posts_ByMonthPublished_Count : AbstractIndexCreationTask<Post, PostCountByMonth>
    {
        public Posts_ByMonthPublished_Count()
        {
            Map = posts => from post in posts
                           select new {post.PublishAt.Year, post.PublishAt.Month, Count = 1};
            Reduce = results => from result in results
                                group result by new {result.Year, result.Month}
                                into g
                                select new {g.Key.Year, g.Key.Month, Count = g.Sum(x => x.Count)};
        }
    }

    public class PostCountByMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Count { get; set; }
    }
}