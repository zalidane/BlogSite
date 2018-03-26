using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Zalidane.com.Models;

namespace Zalidane.com.Helpers
{
    public static class TagHelper
    {
        public static List<string> GetTags(Post post)
        {
            List<string> tagList = new List<string>();

            foreach (Tag tag in post.Tags)
            {
                tagList.Add(tag.Name);
            }

            return tagList;
        }
    }
}