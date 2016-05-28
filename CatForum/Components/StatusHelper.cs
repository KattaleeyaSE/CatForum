using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatForum.Components
{
    public class StatusHelper
    {
        private string[] PostStatus = {
            "Available",
            "Delete"
        };
        private string[] CatStatus = {
            "Finding",
            "Found",
            "Search for Adoption",
            "Adopted",
            "Want to Adopt"
        };
        public string getPostStatus(int id)
        {
            if (id > 0 && id <= this.PostStatus.Length)
                return this.PostStatus[id - 1];
            else return "";
        }
        public string getCatStatus(int id)
        {
            if (id > 0 && id <= this.CatStatus.Length)
                return this.CatStatus[id - 1];
            else return "";
        }
    }
}
