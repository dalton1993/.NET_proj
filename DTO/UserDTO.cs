using System;
using System.Collections.Generic;
using Models;

namespace DTO
{
    public class UserDTO
    {
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public ICollection<Post> Post {get; set;}
        public ICollection<UserToUser> Followers {get; set;}
        public ICollection<UserToUser> Following {get; set;}
    }
}