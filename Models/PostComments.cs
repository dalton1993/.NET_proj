using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class PostComments
    {
        public int PostCommentsId {get; set;}

        [Required]
        [MaxLength(300)]
        public string Comment {get;set;}

        public int PostId {get;set;}
        public Post Post {get;set;}
        
        public int UserId {get;set;}
        public User User {get; set;}
        public ICollection<CommentReply> Replies {get;set;}

    }
}