using System;
using System.Collections.Generic;
using Models;

namespace DTO
{
    public class PostsDTO
    {
        public int PostId {get;set;}
        public string Message {get; set;}
        public byte[] CreatedAt {get; set;}
        public ICollection<PostComments> Comments {get;set;}
    }
}