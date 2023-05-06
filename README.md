# Foodbuddy an application for food
![image](https://user-images.githubusercontent.com/94704550/219944630-50640882-aae6-4175-9560-724fe459dcb9.png)

# Setup

1. After git clone apply existing migrations in Package manager console.
```
Update-Database
```
2. Run project in IIS Express

## Account creation for login
1. In UserAuthenticationController uncomment Register() method
2. Run project
3. To complete registration visit: https://localhost:your_port/UserAuthentication/Register
4. If "You have registered successfully" comes up you can login with details from Register() method