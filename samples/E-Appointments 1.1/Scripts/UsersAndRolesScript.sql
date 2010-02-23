USE [EAppointments]
GO

/**********************************************************************/
/* UsersAndRolesScript.SQL                                            */
/*                                                                    */
/**********************************************************************/

PRINT '-------------------------------------------------'
PRINT 'Starting execution of UsersAndRolesScript.SQL'
PRINT '-------------------------------------------------'
GO

PRINT '-------------------------------------------------'
PRINT 'ROLES                                            '
PRINT '-------------------------------------------------'
GO
/* BMSAdmin role*/
exec dbo.aspnet_Roles_CreateRole @ApplicationName=N'EAppointments',@RoleName=N'BMSAdmin'

/* Referrer  role*/
exec dbo.aspnet_Roles_CreateRole @ApplicationName=N'EAppointments',@RoleName=N'Referrer'

/* Patient role*/
exec dbo.aspnet_Roles_CreateRole @ApplicationName=N'EAppointments',@RoleName=N'Patient'

/* Provider role*/
exec dbo.aspnet_Roles_CreateRole @ApplicationName=N'EAppointments',@RoleName=N'Provider'

PRINT '-------------------------------------------------'
PRINT 'USERS                                            '
PRINT '-------------------------------------------------'
GO

DECLARE @currentUTCTime DATETIME,
        @newUserId UNIQUEIDENTIFIER

/* Administrator users*/
SET @currentUTCTime = GETUTCDATE()
SET @newUserId = NEWID()

exec dbo.aspnet_Membership_CreateUser 
	@ApplicationName=N'EAppointments',
	@UserName=N'bmsadmin',
	@Password=N'PYaov19jDb6bm/Y/hBEaa+uMsHs=',
	@PasswordSalt=N'/cAi+5MOrm4DF3ES5GxmeA==',
	@Email=N'admin@eappointments.net',
	@PasswordQuestion=N'What''s your pet''s name?',
	@PasswordAnswer=N'oifNTiwKED+2zwwFBWhQDG+fcZQ=',
	@IsApproved=1,
	@UniqueEmail=1,
	@PasswordFormat=1,
	@CurrentTimeUtc= @currentUTCTime,
	@UserId=@newUserId 

/* Add admin user to Administrator Role*/
exec dbo.aspnet_UsersInRoles_AddUsersToRoles 
  @ApplicationName=N'EAppointments',
  @RoleNames=N'BMSAdmin',
  @UserNames=N'bmsadmin',
  @CurrentTimeUtc=@currentUTCTime
  
/* Referrer user*/
SET @currentUTCTime = GETUTCDATE()
SET @newUserId = NEWID()

exec dbo.aspnet_Membership_CreateUser 
	@ApplicationName=N'EAppointments',
	@UserName=N'gsinha',
	@Password=N'CGaHQWhI1HHr12gm8MldymDgzAY=',
	@PasswordSalt=N'srBKs4FkjWDWMvlrvl3wAg==',
	@Email=N'gsinha@eappointments.net',
	@PasswordQuestion=N'What''s your cat''s name?',
	@PasswordAnswer=N'US7/p0QwbR3X/dVKBy0d9HZLBo4=',
	@IsApproved=1,
	@UniqueEmail=1,
	@PasswordFormat=1,
	@CurrentTimeUtc=@currentUTCTime,
	@UserId=@newUserId

exec dbo.aspnet_UsersInRoles_AddUsersToRoles 
  @ApplicationName=N'EAppointments',
  @RoleNames=N'Referrer',
  @UserNames=N'gsinha',
  @CurrentTimeUtc=@currentUTCTime

/* Referrer user*/
SET @currentUTCTime = GETUTCDATE()
SET @newUserId = NEWID()

exec dbo.aspnet_Membership_CreateUser 
	@ApplicationName=N'EAppointments',
	@UserName=N'sanjiv',
	@Password=N'CGaHQWhI1HHr12gm8MldymDgzAY=',
	@PasswordSalt=N'srBKs4FkjWDWMvlrvl3wAg==',
	@Email=N'sanjiv@eappointments.net',
	@PasswordQuestion=N'What''s your cat''s name?',
	@PasswordAnswer=N'US7/p0QwbR3X/dVKBy0d9HZLBo4=',
	@IsApproved=1,
	@UniqueEmail=1,
	@PasswordFormat=1,
	@CurrentTimeUtc=@currentUTCTime,
	@UserId=@newUserId

exec dbo.aspnet_UsersInRoles_AddUsersToRoles 
  @ApplicationName=N'EAppointments',
  @RoleNames=N'Referrer',
  @UserNames=N'sanjiv',
  @CurrentTimeUtc=@currentUTCTime
  
/* Patient user*/
SET @currentUTCTime = GETUTCDATE()
SET @newUserId = NEWID()

exec dbo.aspnet_Membership_CreateUser 
	@ApplicationName=N'EAppointments',
	@UserName=N'vikram',
	@Password=N'CGaHQWhI1HHr12gm8MldymDgzAY=',
	@PasswordSalt=N'srBKs4FkjWDWMvlrvl3wAg==',
	@Email=N'vikram@eappointments.net',
	@PasswordQuestion=N'What''s your cat''s name?',
	@PasswordAnswer=N'US7/p0QwbR3X/dVKBy0d9HZLBo4=',
	@IsApproved=1,
	@UniqueEmail=1,
	@PasswordFormat=1,
	@CurrentTimeUtc=@currentUTCTime,
	@UserId=@newUserId

exec dbo.aspnet_UsersInRoles_AddUsersToRoles 
  @ApplicationName=N'EAppointments',
  @RoleNames=N'Patient',
  @UserNames=N'vikram',
  @CurrentTimeUtc=@currentUTCTime
  
  
/* Provider user*/
SET @currentUTCTime = GETUTCDATE()
SET @newUserId = NEWID()

exec dbo.aspnet_Membership_CreateUser 
	@ApplicationName=N'EAppointments',
	@UserName=N'jehangir',
	@Password=N'CGaHQWhI1HHr12gm8MldymDgzAY=',
	@PasswordSalt=N'srBKs4FkjWDWMvlrvl3wAg==',
	@Email=N'jehangir@eappointments.net',
	@PasswordQuestion=N'What''s your cat''s name?',
	@PasswordAnswer=N'US7/p0QwbR3X/dVKBy0d9HZLBo4=',
	@IsApproved=1,
	@UniqueEmail=1,
	@PasswordFormat=1,
	@CurrentTimeUtc=@currentUTCTime,
	@UserId=@newUserId

exec dbo.aspnet_UsersInRoles_AddUsersToRoles 
  @ApplicationName=N'EAppointments',
  @RoleNames=N'Provider',
  @UserNames=N'jehangir',
  @CurrentTimeUtc=@currentUTCTime