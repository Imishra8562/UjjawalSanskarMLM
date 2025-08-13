Create PROCEDURE [dbo].[sp_Delete_User]
@User_Id INT

AS
BEGIN
BEGIN TRY     
 	
   UPDATE tbl_User SET Is_Active=0,Modified_On=GETDATE() WHERE User_Id=@User_Id
   SELECT @User_Id

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : DELETE USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END
