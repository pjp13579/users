
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using PP23771.DTO;
using PP23771.Entities;

namespace PP23771.Converters;

public interface IConverter
{
	ViewUserDTO userToViewUserDTO(User user);
	List<ViewUserDTO> usersToViewUserDTO(List<User> user);
	ViewCompleteUserDTO userToViewCompleteDTO(User user);
	List<User> createUsersDTOToUsers(List<CreateUserDTO> createUsers);
}

public class Converter : IConverter
{

	public List<User> createUsersDTOToUsers(List<CreateUserDTO> createUsers)
	{
		List<User> users = new List<User>();

		createUsers.ForEach(createUser =>
		{
			User user = new User();

			user._id = new MongoDB.Bson.ObjectId();
			user.uid = createUser.uid;
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] passwordBytes = Encoding.UTF8.GetBytes(createUser.password);
				byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
				user.password = System.Convert.ToBase64String(hashedBytes);
			}
			user.first_name = createUser.first_name;
			user.last_name = createUser.last_name;
			user.username = createUser.username;
			user.email = createUser.email;
			user.avatar = createUser.avatar;
			user.gender = createUser.gender;
			user.phone_number = createUser.phone_number;
			user.social_insurance_number = createUser.social_insurance_number;
			user.date_of_birth = createUser.date_of_birth;

			user.employment = new Entities.User.Employment();
			user.employment.title = createUser.employment.title;
			user.employment.key_skill = createUser.employment.key_skill;

			user.address = new Entities.User.Address();
			user.address.city = createUser.address.city;
			user.address.street_name = createUser.address.street_name;
			user.address.street_address = createUser.address.street_address;
			user.address.zip_code = createUser.address.zip_code;
			user.address.state = createUser.address.state;
			user.address.country = createUser.address.country;

			user.address.coordinates = new Entities.User.Coordinates();
			user.address.coordinates.lat = createUser.address.coordinates.lat;
			user.address.coordinates.lng = createUser.address.coordinates.lng;

			user.credit_card = new Entities.User.CreditCard();
			user.credit_card.cc_number = createUser.credit_card.cc_number;

			user.subscription = new Entities.User.Subscription();
			user.subscription.plan = createUser.subscription.plan;
			user.subscription.status = createUser.subscription.status;
			user.subscription.payment_method = createUser.subscription.payment_method;
			user.subscription.term = createUser.subscription.term;
		
			users.Add(user);
		});

		return users;
	}

	public ViewCompleteUserDTO userToViewCompleteDTO(User user)
	{
		ViewCompleteUserDTO viewCompleteUserDTO = new ViewCompleteUserDTO();

		viewCompleteUserDTO._id = user._id;
		viewCompleteUserDTO.uid = user.uid;
		viewCompleteUserDTO.first_name = user.first_name;
		viewCompleteUserDTO.last_name = user.last_name;
		viewCompleteUserDTO.username = user.username;
		viewCompleteUserDTO.email = user.email;
		viewCompleteUserDTO.avatar = user.avatar;
		viewCompleteUserDTO.gender = user.gender;
		viewCompleteUserDTO.phone_number = user.phone_number;
		viewCompleteUserDTO.social_insurance_number = user.social_insurance_number;
		viewCompleteUserDTO.date_of_birth = user.date_of_birth;

		viewCompleteUserDTO.employment = new DTO.ViewCompleteUserDTO.Employment();
		viewCompleteUserDTO.employment.title = user.employment.title;
		viewCompleteUserDTO.employment.key_skill = user.employment.key_skill;

		viewCompleteUserDTO.address = new DTO.ViewCompleteUserDTO.Address();
		viewCompleteUserDTO.address.city = user.address.city;
		viewCompleteUserDTO.address.street_name = user.address.street_name;
		viewCompleteUserDTO.address.street_address = user.address.street_address;
		viewCompleteUserDTO.address.zip_code = user.address.zip_code;
		viewCompleteUserDTO.address.state = user.address.state;
		viewCompleteUserDTO.address.country = user.address.country;

		viewCompleteUserDTO.address.coordinates = new DTO.ViewCompleteUserDTO.Coordinates();
		viewCompleteUserDTO.address.coordinates.lat = user.address.coordinates.lat;
		viewCompleteUserDTO.address.coordinates.lng = user.address.coordinates.lng;

		viewCompleteUserDTO.credit_card = new DTO.ViewCompleteUserDTO.CreditCard();
		viewCompleteUserDTO.credit_card.cc_number = user.credit_card.cc_number;

		viewCompleteUserDTO.subscription = new DTO.ViewCompleteUserDTO.Subscription();
		viewCompleteUserDTO.subscription.plan = user.subscription.plan;
		viewCompleteUserDTO.subscription.status = user.subscription.status;
		viewCompleteUserDTO.subscription.payment_method = user.subscription.payment_method;
		viewCompleteUserDTO.subscription.term = user.subscription.term;

		return viewCompleteUserDTO;
	}

	public ViewUserDTO userToViewUserDTO(User user)
	{
		ViewUserDTO viewUserDTO = new ViewUserDTO();

		viewUserDTO._id = user._id;
		viewUserDTO.uid = user.uid;
		viewUserDTO.first_name = user.first_name;
		viewUserDTO.last_name = user.last_name;
		viewUserDTO.username = user.username;
		viewUserDTO.email = user.email;
		viewUserDTO.avatar = user.avatar;
		viewUserDTO.gender = user.gender;
		viewUserDTO.phone_number = user.phone_number;
		viewUserDTO.date_of_birth = user.date_of_birth;

		viewUserDTO.employment = new DTO.ViewUserDTO.Employment();
		viewUserDTO.employment.title = user.employment.title;
		viewUserDTO.employment.key_skill = user.employment.key_skill;

		viewUserDTO.address = new DTO.ViewUserDTO.Address();
		viewUserDTO.address.city = user.address.city;
		viewUserDTO.address.street_name = user.address.street_name;
		viewUserDTO.address.street_address = user.address.street_address;
		viewUserDTO.address.zip_code = user.address.zip_code;
		viewUserDTO.address.state = user.address.state;
		viewUserDTO.address.country = user.address.country;

		viewUserDTO.address.coordinates = new DTO.ViewUserDTO.Coordinates();
		viewUserDTO.address.coordinates.lat = user.address.coordinates.lat;
		viewUserDTO.address.coordinates.lng = user.address.coordinates.lng;

		viewUserDTO.subscription = new DTO.ViewUserDTO.Subscription();
		viewUserDTO.subscription.plan = user.subscription.plan;
		viewUserDTO.subscription.status = user.subscription.status;
		viewUserDTO.subscription.term = user.subscription.term;

		return viewUserDTO;
	}

	public List<ViewUserDTO> usersToViewUserDTO(List<User> users)
	{
		List<ViewUserDTO> viewUsersDTO = new List<ViewUserDTO>();
		users.ForEach(action: user =>
		{
			ViewUserDTO viewUserDTO = new ViewUserDTO();

			viewUserDTO._id = user._id;
			viewUserDTO.uid = user.uid;
			viewUserDTO.first_name = user.first_name;
			viewUserDTO.last_name = user.last_name;
			viewUserDTO.username = user.username;
			viewUserDTO.email = user.email;
			viewUserDTO.avatar = user.avatar;
			viewUserDTO.gender = user.gender;
			viewUserDTO.phone_number = user.phone_number;
			viewUserDTO.date_of_birth = user.date_of_birth;

			viewUserDTO.employment = new DTO.ViewUserDTO.Employment();
			viewUserDTO.employment.title = user.employment.title;
			viewUserDTO.employment.key_skill = user.employment.key_skill;

			viewUserDTO.address = new DTO.ViewUserDTO.Address();
			viewUserDTO.address.city = user.address.city;
			viewUserDTO.address.street_name = user.address.street_name;
			viewUserDTO.address.street_address = user.address.street_address;
			viewUserDTO.address.zip_code = user.address.zip_code;
			viewUserDTO.address.state = user.address.state;
			viewUserDTO.address.country = user.address.country;

			viewUserDTO.address.coordinates = new DTO.ViewUserDTO.Coordinates();
			viewUserDTO.address.coordinates.lat = user.address.coordinates.lat;
			viewUserDTO.address.coordinates.lng = user.address.coordinates.lng;

			viewUserDTO.subscription = new DTO.ViewUserDTO.Subscription();
			viewUserDTO.subscription.plan = user.subscription.plan;
			viewUserDTO.subscription.status = user.subscription.status;
			viewUserDTO.subscription.term = user.subscription.term;

			viewUsersDTO.Add(viewUserDTO);
		});

		return viewUsersDTO;
	}
}