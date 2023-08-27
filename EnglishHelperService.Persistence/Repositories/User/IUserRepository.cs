﻿using EnglishHelperService.Persistence.Entities;

namespace EnglishHelperService.Persistence.Repositories
{
	public interface IUserRepository
	{
		Task<User> ReadById(long id);
		Task<IEnumerable<User>> List();
		void Create(User user);
		void Update(User user);
		void Delete(User user);
	}
}
