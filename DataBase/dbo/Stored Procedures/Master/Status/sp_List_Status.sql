CREATE PROCEDURE [dbo].[sp_List_Status]
	@Status_Id INT=NULL

AS   
SET NOCOUNT ON    
BEGIN    
 BEGIN TRY   
   BEGIN   

    IF @Status_Id=0 SET @Status_Id=NULL

    SELECT * FROM tbl_Status WITH(NOLOCK)
    WHERE Status_Id=ISNULL(@Status_Id,Status_Id) 
    AND Is_Active=1 
    ORDER BY Status_Id

   END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST Status FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END