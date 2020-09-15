# User API Dev Guide
[Get]	api/accounts - returns list of the all accounts
[Post]	api/accounts - accepts AccountCreationModel {int UserId,
													  string FirstName, 
													  string SecondName} 
						and allows to create new account for the 
						specified user if it is not created yet and returns created account
[Get]	api/users - returns list of the all users
[Get]	api/users/{email} - returns user by email
[Get]	api/users/{id} - returns user by its id
[Post]	api/users - accepts UserModel	{string Name,
										 string Email,
										 int MonthlySalary,
										 int MonthlyExpensis}  - validates model so the MonthlySalary is not less than 1000$
																and MonthlyExpensis > 0,and creates user is email doesn't exist
## Building
dotnet build;
cd TestProject.WebAPI;
dotnet run
## Testing
dotnet build && dotnet test --logger xunit --results-directory ./reports/
## Deploying

## Additional Information