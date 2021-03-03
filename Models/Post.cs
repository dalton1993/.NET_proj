using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Post
    {
        public int PostId {get;set;}

        [Required]
        [MaxLength(300)]
        public string Message {get; set;}

        [Timestamp]
        public byte[] CreatedAt {get; set;}

        public int UserId {get; set;}
        public User User {get; set;}
        public int CommunityId {get;set;}
        public Community Community {get;set;}
        public ICollection<PostComments> Comments {get; set;}
    }
}