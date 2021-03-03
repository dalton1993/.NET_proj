using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Community
    {
        public int CommunityId {get;set;}
        public string Game {get;set;}
        public ICollection<Post> Posts {get;set;}
        public ICollection<UserCommunity> CommunityMember {get;set;}
    }
}