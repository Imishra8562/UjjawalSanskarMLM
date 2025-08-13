CREATE PROCEDURE [dbo].[sp_List_User_Role]
@User_Role_Id INT=NULL

AS   
SET NOCOUNT ON    
BEGIN    
BEGIN TRY   

   BEGIN  
 
      IF @User_Role_Id=0 SET @User_Role_Id=NULL
 
      SELECT * FROM tbl_User_Role WITH(NOLOCK)
      WHERE User_Role_Id=ISNULL(@User_Role_Id,User_Role_Id) AND Is_Active=1
      ORDER BY User_Role_Id

   END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST USER ROLE FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH    
END
