using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User
    {
        public int UserId {get; set;}

        [Required]
        [MaxLength(50)]
        public string FirstName {get; set;}

        [Required]
        [MaxLength(50)]
        public string LastName {get; set;}

        [Required]
        [MaxLength(50)]
        public string UserName {get; set;}

        public int Mobile {get; set;}

        [Required]
        public string Email {get; set;}

        [Required]
        [MaxLength(50)]
        public string Password {get; set;}

        [Timestamp]
        public byte[] CreatedAt {get;set;}

        public ICollection<Post> Post {get; set;}
        public ICollection<PostComments> Comments {get; set;}
        public ICollection<CommentReply> Replies {get; set;}
        public ICollection<UserToUser> Followers {get; set;}
        public ICollection<UserToUser> Following {get; set;}
        public ICollection<UserCommunity> CommunityMember {get;set;}
    }
}