CREATE PROCEDURE [dbo].[sp_Agent_Authenticate]
@User_Id NVARCHAR(200)=NULL,
@Password NVARCHAR(MAX)=NULL

AS
BEGIN
BEGIN TRY  
	   		 
   BEGIN
      SELECT * FROM tbl_Agent_Registration U
       WHERE U.User_Id=@User_Id AND U.Password=@Password 
   END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : AUTHENTICATE AGENT USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END