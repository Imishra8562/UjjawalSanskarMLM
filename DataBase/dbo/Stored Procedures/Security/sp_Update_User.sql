CREATE PROCEDURE [dbo].[sp_Update_User]
@User_Id INT=NULL,
@User_Name NVARCHAR(MAX)=NULL,
@Mobile_No NVARCHAR(MAX)=NULL,
@Email_Id NVARCHAR(200)=NULL,
@Password VARBINARY(MAX)=NULL,
@FK_User_Role_Id INT=NULL,
@Is_Locked BIT=NULL,
@Last_Login DATETIME=NULL,

@Created_On DATETIME=NULL,
@Created_By INT=NULL,
@Modified_On DATETIME=NULL,
@Modified_By INT=NULL,
@Created_IP NVARCHAR(MAX)=NULL,
@Modified_IP NVARCHAR(MAX)=NULL,
@Is_Active BIT=NULL

AS
BEGIN
BEGIN TRY  
	   
   UPDATE tbl_User SET User_Name=@User_Name,
                       Mobile_No=@Mobile_No,
                       Email_Id=@Email_Id, 
                       Password=@Password, 
                       FK_User_Role_Id=@FK_User_Role_Id,
                       Is_Locked=@Is_Locked,
                       Last_Login=@Last_Login,

                       Modified_On=@Modified_On,
                       Modified_IP=@Modified_IP,
                       Modified_By=@Modified_By
                 WHERE User_Id=@User_Id
   SELECT @User_Id
                      
END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : UPDATE USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END
