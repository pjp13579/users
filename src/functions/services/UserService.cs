using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using PP23771.Entities;
using PP23771.UserDao;

namespace PP23771.UserService;

public interface IService
{
	List<User> getAllUsers();
	User getUserById(string userId);
	bool postUser(List<User> users);
	DeleteResult deleteUsers(List<string> ids);
}

public class Service : IService
{
	private IDAO userDao = new PP23771.UserDao.DAO();

	public List<User> getAllUsers()
	{
		return userDao.getAllUsers();
	}

	public User getUserById(string userId)
	{
		return userDao.getUserById(ObjectId.Parse(userId));
	}
	public bool postUser(List<User> users)
	{
		try
		{
			userDao.postUsers(users);
			return true;
		}
		catch (System.Exception)
		{
			throw;
		}

	}

	public DeleteResult deleteUsers(List<string> ids)
	{
		List<ObjectId> objIds = ids.Select(id => ObjectId.Parse(id)).ToList();
		return userDao.deleteUsers(objIds);
	}
}