using System;

namespace PP23771.Exceptions;


public class UserNotFoundException : Exception
{
	public UserNotFoundException(string id) : base(String.Format("User with id {0} not found", id)) { }
}