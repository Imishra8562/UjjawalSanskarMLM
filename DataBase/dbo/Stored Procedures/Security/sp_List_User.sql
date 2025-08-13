CREATE PROCEDURE [dbo].[sp_List_User]
@User_Id INT=NULL,
@User_Role_Id INT=NULL,
@Email_Id NVARCHAR(200)=NULL,
@Mobile_No NVARCHAR(MAX)=NULL

AS   
SET NOCOUNT ON    
BEGIN    
BEGIN TRY   

   BEGIN   

      IF @User_Id=0 SET @User_Id=NULL
      IF @User_Role_Id=0 SET @User_Role_Id=NULL
      IF @Email_Id='' SET @Email_Id=NULL

      SELECT *, U.Is_Active AS User_Active FROM tbl_User U
      INNER JOIN tbl_User_Role UR WITH(NOLOCK) ON UR.User_Role_Id=U.FK_User_Role_Id
      WHERE U.User_Id=ISNULL(@User_Id,U.User_Id) AND 
      U.FK_User_Role_Id=ISNULL(@User_Role_Id,U.FK_User_Role_Id) AND 
      U.Email_Id=ISNULL(@Email_Id,U.Email_Id) AND  
      U.Mobile_No=ISNULL(@Mobile_No,U.Mobile_No) AND  
      U.Is_Active=1 
      ORDER BY U.User_Id

   END

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST USER FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH    
END
