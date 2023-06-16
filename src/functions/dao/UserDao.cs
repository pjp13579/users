using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using PP23771.Entities;

namespace PP23771.UserDao;

public interface IDAO
{
	List<User> getAllUsers();
	User getUserById(ObjectId id);
	void postUsers(List<User> users);
	DeleteResult deleteUsers(List<ObjectId> ids);
}




public class DAO : IDAO
{
	private IMongoClient client;
	private IMongoDatabase database;
	private IMongoCollection<User> userCollection;
	private readonly string connectionUri = System.Environment.GetEnvironmentVariable("mongodbURI", EnvironmentVariableTarget.Process);
	private readonly string databaseName = System.Environment.GetEnvironmentVariable("databaseName", EnvironmentVariableTarget.Process);
	private readonly string userCollectionName = System.Environment.GetEnvironmentVariable("userCollectionName", EnvironmentVariableTarget.Process);


	public DAO()
	{
		client = new MongoClient(connectionUri);
		database = client.GetDatabase(databaseName);
		userCollection = database.GetCollection<User>(userCollectionName);
	}

	public List<User> getAllUsers()
	{
		FilterDefinition<User> userFilter = Builders<User>.Filter.Empty;

		return userCollection.Find(userFilter).ToList();
	}

	public User getUserById(ObjectId id)
	{
		FilterDefinition<User> userFilter = Builders<User>.Filter.Eq(user => user._id, value: id);

		return userCollection.Find<User>(userFilter).First();
	}

	public void postUsers(List<User> users)
	{
		userCollection.InsertMany(users);
	}

	public DeleteResult deleteUsers(List<ObjectId> ids)
	{
		FilterDefinition<User> deleteUserFilter = Builders<User>.Filter.In(user => user._id, ids);

		return userCollection.DeleteMany(deleteUserFilter);
	}
}

