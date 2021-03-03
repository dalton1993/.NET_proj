
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class CommentReply
    {
        public int CommentReplyId {get;set;}

        [Required]
        [MaxLength(300)]
        public string reply {get;set;}

        public int PostCommentsId {get;set;}
        public PostComments Comment {get;set;}

        public int UserId {get;set;}
        public User User {get; set;}
    }
}