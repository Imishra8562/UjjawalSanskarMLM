CREATE PROCEDURE [dbo].[sp_Add_Status]
		
@Status_Name NVARCHAR(MAX)=NULL,

@Created_On DATETIME=NULL,
@Created_By INT=NULL,
@Created_IP NVARCHAR(MAX)=NULL,
@Modified_On DATETIME=NULL,
@Modified_By INT=NULL,
@Modified_IP NVARCHAR(MAX)=NULL,
@Is_Active BIT=NULL

AS
BEGIN    
 BEGIN TRY   

   BEGIN

   DECLARE @Status_Id INT
   SELECT @Status_Id=Status_Id FROM tbl_Status WHERE Status_Name=@Status_Name AND Is_Active=1
  
   IF @Status_Id IS NULL 
   BEGIN

    INSERT INTO tbl_Status(Status_Name,        
                               Created_By,
                               Created_IP)
                        VALUES(TRIM(LTRIM(RTRIM(UPPER(@Status_Name)))),
                               @Created_By,
                               @Created_IP)
    SELECT SCOPE_IDENTITY()

   END

   END
    
END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : ADD Status FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END
