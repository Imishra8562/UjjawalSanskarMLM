CREATE PROCEDURE [dbo].[sp_Authenticate_User]
@User_Name NVARCHAR(200)=NULL,
@Password VARBINARY(MAX)=NULL

AS
BEGIN
BEGIN TRY  
	   		 
   BEGIN
      SELECT * FROM tbl_User U
      INNER JOIN tbl_User_Role UR ON UR.User_Role_Id=U.FK_User_Role_Id
       WHERE U.User_Name=@User_Name AND U.Password=@Password 
      --WHERE U.Email_Id=@Email_Id AND U.Password=@Password
   END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : AUTHENTICATE USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END