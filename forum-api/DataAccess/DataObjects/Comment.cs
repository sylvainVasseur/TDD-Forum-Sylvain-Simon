using System;
using System.Collections.Generic;

namespace forum_api.DataAccess.DataObjects
{
    public partial class Comment
    {
        public int IdComment { get; set; }
        public string Username { get; set; } = null!;
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Content { get; set; } = null!;
        public int TopicIdtopic { get; set; }
    }
}
