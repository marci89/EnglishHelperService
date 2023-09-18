﻿namespace EnglishHelperService.Persistence.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<Word> Words { get; set; }

    }
}

