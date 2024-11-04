CREATE DATABASE DBColProfessional;
GO


-- Switch context to the new database
USE DBColProfessional;
GO

-- Grant full control to the 'sa' user
CREATE LOGIN sa WITH PASSWORD = 'Passw0rd', CHECK_POLICY = OFF;
ALTER SERVER ROLE sysadmin ADD MEMBER sa;
GO