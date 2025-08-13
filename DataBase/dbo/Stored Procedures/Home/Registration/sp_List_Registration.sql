CREATE PROCEDURE [dbo].[sp_List_Registration]
@Registration_Id INT=NULL,
@User_Id INT=NULL,
@Status_Id INT=NULL,
@Token_Id NVARCHAR(MAX)= NULL,
@Sponcer_Id NVARCHAR(MAX)= NULL,
@Mobile NVARCHAR(MAX)= NULL,
@Email NVARCHAR(MAX)= NULL,
@Position NVARCHAR(MAX)= NULL,
@Gole_Completed BIT= NULL

AS
BEGIN    
 BEGIN TRY   

    IF @Registration_Id=0 SET @Registration_Id=NULL
    IF @User_Id=0 SET @User_Id=NULL
    IF @Status_Id=0 SET @Status_Id=NULL

	SELECT * FROM(
     SELECT RE.*,U.User_Name,S.Status_Name,R.User_Role_Name,dbo.fn_IsBinaryTreeComplete10(RE.Token_Id) AS IsGoleComplete 
	FROM tbl_Registration RE WITH(NOLOCK)
	INNER JOIN tbl_User U WITH(NOLOCK) ON U.User_Id=RE.FK_User_Id
	INNER JOIN tbl_Status S WITH(NOLOCK) ON S.Status_Id=RE.FK_Reg_Status_Id
	INNER JOIN tbl_User_Role R WITH(NOLOCK) ON R.User_Role_Id=U.FK_User_Role_Id
    --INNER JOIN tbl_Level_Comm_Chat LC ON RE.FK_Level_Comm_Chat_Id=LC.Level_Comm_Chat_Id	
	WHERE 
	RE.Registration_Id=ISNULL(@Registration_Id,RE.Registration_Id) AND
	RE.Token_Id=ISNULL(@Token_Id,RE.Token_Id) AND
	RE.FK_Reg_Status_Id=ISNULL(@Status_Id,RE.FK_Reg_Status_Id) AND
	RE.Sponsor_Id=ISNULL(@Sponcer_Id,RE.Sponsor_Id) AND
	RE.FK_User_Id=ISNULL(@User_Id,RE.FK_User_Id) AND
	RE.Email=ISNULL(@Email,RE.Email) AND
	RE.Position=ISNULL(@Position,RE.Position) AND
	RE.Mobile=ISNULL(@Mobile,RE.Mobile)
	AND RE.Is_Active=1 )A
	WHERE A.IsGoleComplete = ISNULL(@Gole_Completed,A.IsGoleComplete)
	ORDER BY A.Registration_Id

END TRY   
BEGIN CATCH    
   DECLARE @ErrorMessage VARCHAR(MAX);    
   SELECT @ErrorMessage ='SP ERROR : LIST REGISTRATION FAILED' + Char(13) + Char(10) + 'THE ERROR WAS : ' + Char(13) + Char(10) + ERROR_MESSAGE()   
   RAISERROR (@ErrorMessage, 16, 1)   
END CATCH
END