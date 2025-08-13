CREATE PROCEDURE [dbo].[sp_Delete_Registration]

@Registration_Id INT

AS
BEGIN    
 BEGIN TRY  

	BEGIN
	UPDATE tbl_Registration SET Is_Active=0 WHERE Registration_Id=@Registration_Id
	SELECT @Registration_Id
	END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : DELETE REGISTRATION FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END