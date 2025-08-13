CREATE PROCEDURE [dbo].[sp_Delete_Status]
		@Status_Id INT

AS
BEGIN    
 BEGIN TRY  

	BEGIN
	UPDATE tbl_Status SET Is_Active=0 WHERE Status_Id=@Status_Id
	SELECT @Status_Id
	END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : DELETE Status FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END