using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using PP23771.Entities;
using PP23771.UserDao;
using PP23771.Converters;
using PP23771.DTO;
using PP23771.Exceptions;

namespace PP23771.UserService;

public interface IService
{
	List<ViewUserDTO> getAllUsers();
	ViewUserDTO getUserById(string userId);
	bool postUser(List<CreateUserDTO> createUsers);
	DeleteResult deleteUsers(List<string> ids);
}

public class Service : IService
{
	private IDAO userDao = new PP23771.UserDao.DAO();
	private IConverter converter = new Converter();

	public List<ViewUserDTO> getAllUsers()
	{
		List<User> users = userDao.getAllUsers();

		List<ViewUserDTO> viewUsers = converter.usersToViewUserDTO(users);

		return viewUsers;
	}

	public ViewUserDTO getUserById(string userId)
	{
		User user = userDao.getUserById(ObjectId.Parse(userId));
		if (user == null)
		{
			throw new UserNotFoundException(userId);
		}

		ViewUserDTO viewUser = converter.userToViewUserDTO(user);

		return viewUser;
	}
	public bool postUser(List<CreateUserDTO> createUsers)
	{
		List<User> users = converter.createUsersDTOToUsers(createUsers);
		userDao.postUsers(users);
		return true;
	}

	public DeleteResult deleteUsers(List<string> ids)
	{
		List<ObjectId> objIds = ids.Select(id => ObjectId.Parse(id)).ToList();
		return userDao.deleteUsers(objIds);
	}
}